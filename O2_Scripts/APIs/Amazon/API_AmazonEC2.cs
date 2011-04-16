// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods; 
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.XRules.Database.Utils;
using O2.External.SharpDevelop.ExtensionMethods;
using Amazon.EC2;
using Amazon.EC2.Model;

//O2Ref:AWSSDK.dll
//O2File:ascx_AskUserForLoginDetails.cs	
//O2File:API_GuiAutomation.cs
//O2File:API_RSA.cs

//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{	
	public class API_AmazonEC2
	{			
		public ICredential ApiKey { get; set; }
		public string DefaultRegion { get; set; }
		public API_RSA ApiRsa { get; set; }
		
		public int TimerCount = 60;
		public int TimerSleep  = 60 * 1000;

		
		public API_AmazonEC2() : this(null)
		{
			
		}		
		
		public API_AmazonEC2(ICredential apiKey) 
		{
			DefaultRegion = "us-west-1";//"eu-west-1";
			if (apiKey.isNull())
				apiKey = ascx_AskUserForLoginDetails.ask();
			ApiKey = apiKey;
		}
	}
	
	public static class API_AmazonEC2_ExtensionMethods
	{
		public static List<string> getEC2Regions(this API_AmazonEC2 amazonEC2)
		{
			var ec2Client = new AmazonEC2Client(amazonEC2.ApiKey.UserName, amazonEC2.ApiKey.Password); 
			return (from region in  ec2Client.DescribeRegions(new DescribeRegionsRequest())
		 		      			 			.DescribeRegionsResult.Region
					select region.RegionName).toList();
		}
		
		public static AmazonEC2Client getEC2Client(this API_AmazonEC2 amazonEC2, string region)
		{
			return new AmazonEC2Client(amazonEC2.ApiKey.UserName,
								 	   amazonEC2.ApiKey.Password, new AmazonEC2Config() 
								 	   							{ServiceURL = "http://{0}.ec2.amazonaws.com".format(region)});
		}
		
		
		public static List<Reservation> getReservationsInRegion(this API_AmazonEC2 amazonEC2, string region)
		{
		        "Gettting Reservations in region: {0}".info(region);
				var ec2ClientInRegion = amazonEC2.getEC2Client(region);
				var describesInstance = new DescribeInstancesRequest(); 				
				var reservations = ec2ClientInRegion.DescribeInstances(describesInstance)
													.DescribeInstancesResult
													.Reservation; 				
				return reservations;									  
		}
		
		public static Dictionary<string,List<RunningInstance>> getEC2Instances(this API_AmazonEC2 amazonEC2,bool onlyShowDefaultRegion)
		{		
			var instances = new Dictionary<string,List<RunningInstance>>();		
			
			var reservations = new List<Reservation>();
			if (onlyShowDefaultRegion)
				reservations.add(amazonEC2.getReservationsInRegion(amazonEC2.DefaultRegion));
			else
				foreach(var region in amazonEC2.getEC2Regions()) 
					reservations.add(amazonEC2.getReservationsInRegion(region));				        
						
			foreach(var reservation in reservations)
					foreach(var runningInstance in reservation.RunningInstance)
						instances.add(reservation.GroupName.Aggregate((a, b) => a + ',' + b),
									  runningInstance); 			
			return instances;			
		}
	}
	
	public static class API_AmazonEC2_ExtensionMethods_RunningInstance
	{
		public static RunningInstance startInstance(this API_AmazonEC2 amazonEC2, RunningInstance runningInstance)
	    {							
			"Starting instance with ID: {0}".info(runningInstance.InstanceId);							
			var ec2Client = amazonEC2.getEC2Client(runningInstance.Placement.AvailabilityZone.removeLastChar());
			var result = ec2Client.StartInstances(new StartInstancesRequest()
													.WithInstanceId(runningInstance.InstanceId));
			return runningInstance;
		}

		public static RunningInstance stopInstance(this API_AmazonEC2 amazonEC2, RunningInstance runningInstance)
	    {													
			"Stopping instance with ID: {0}".info(runningInstance.InstanceId);							
			var ec2Client = amazonEC2.getEC2Client(runningInstance.Placement.AvailabilityZone.removeLastChar());
			var result = ec2Client.StopInstances(new StopInstancesRequest() 
													.WithInstanceId(runningInstance.InstanceId)); 
	    	return runningInstance;
		}
	   
		public static RunningInstance showConsoleOut(this API_AmazonEC2 amazonEC2, RunningInstance runningInstance)
	    {							
			"Getting Console out instance with ID: {0}".info(runningInstance.InstanceId);			
			var ec2Client = amazonEC2.getEC2Client(runningInstance.Placement.AvailabilityZone.removeLastChar());
			var consoleOutResult = ec2Client.GetConsoleOutput(new GetConsoleOutputRequest()
																    .WithInstanceId(runningInstance.InstanceId));
			var consoleOut = consoleOutResult.GetConsoleOutputResult.ConsoleOutput.Output.base64Decode();	
			consoleOut.showInCodeViewer(".bat");
			return runningInstance;
		}		
	
		public static string getPassword(this API_AmazonEC2 amazonEC2, RunningInstance runningInstance)	
		{	
			"Tests on  instance with ID: {0}".info(runningInstance.InstanceId);										
			var ec2Client = amazonEC2.getEC2Client(runningInstance.Placement.AvailabilityZone.removeLastChar());
			var passwordResponse = ec2Client.GetPasswordData(new GetPasswordDataRequest().WithInstanceId(runningInstance.InstanceId));
			"raw password data : {0}".info(passwordResponse.GetPasswordDataResult.ToXML());						
			var decryptedPassword = amazonEC2.ApiRsa.decryptPasswordUsingPem(passwordResponse.GetPasswordDataResult.PasswordData.Data); 				
			"decrypted password: {0}".info(decryptedPassword);
			return decryptedPassword;			
	   	}
	   	
	   	public static string getRunningInstanceSignature(this API_AmazonEC2 amazonEC2, RunningInstance runningInstance)	
	   	{
			var signature = "{0}  -  {1}  -  {2}  -  {3}  -  {4} ".format(
		   						runningInstance.InstanceId, 
		   						runningInstance.InstanceType, 
		   						runningInstance.IpAddress,
		   						runningInstance.Placement.AvailabilityZone,
		   						runningInstance.InstanceState.Name);
			foreach(var tag in runningInstance.Tag)
				//signature = "{0}= {1}  -  {2}".format(tag.Key, tag.Value, signature); 
				signature = "{1}  -  {2}".format(tag.Key, tag.Value, signature); 
		   	return signature;
		}
	}
	
	public static class API_AmazonEC2_ExtensionMethods_GUIs
	{
		public static API_AmazonEC2 addStopInstanceGui(this API_AmazonEC2 amazonEC2, Panel targetPanel, TreeView treeViewWithInstances)
		{						
			Action startTimer = null;
			Action stopTimer = null;
			var instancesToStop = targetPanel.add_GroupBox("Stop Instance in {0} minutes".format((amazonEC2.TimerCount * amazonEC2.TimerCount / 60))) 
									         .add_TreeView();						
			var timerBar = instancesToStop.insert_Below(20).add_ProgressBar();
			instancesToStop.add_ContextMenu().add_MenuItem("Stop now",true, 
													()=>{
															"Stopping {0} instances now".debug(instancesToStop.nodes().size()); 
															foreach(var node in instancesToStop.nodes())
																amazonEC2.stopInstance((RunningInstance)node.get_Tag());
														})
											 .add_MenuItem("Clear list", ()=>instancesToStop.clear());
			var startTimerLink = instancesToStop.insert_Above(15).add_Link("Add instance to list",0,0, 
													()=>{
															var selectedNode = treeViewWithInstances.selected();
															if (selectedNode.notNull())
															{
																var tag = selectedNode.get_Tag();  
																if (tag is RunningInstance)
																{
																	var selectedInstance = (RunningInstance)tag;
																	var nodeText = "{0} - {1}".format(selectedInstance.InstanceId, selectedInstance.IpAddress);
																	instancesToStop.add_Node(nodeText, selectedInstance);
																}
															}
															//treeViewWithInstances.nodes().showInfo();
														})
											.append_Link("Start timer", ()=>startTimer());  
			var timerEnabled = false;								
			var	stopTimerLink = startTimerLink.append_Link("Stop timer", ()=>stopTimer()).enabled(false);  							
			startTimer = ()=>{											
									"starting timer".info();
									timerEnabled = true;												
									timerBar.maximum(amazonEC2.TimerCount);
									timerBar.value(0);
									startTimerLink.enabled(false);
									stopTimerLink.enabled(true);
									while(timerEnabled && timerBar.Value < amazonEC2.TimerCount)
									{
										"In StopInstances Timer [{0}/{1}], sleeping for {2} seconds".info(timerBar.Value, amazonEC2.TimerCount, amazonEC2.TimerSleep/1000);
										timerBar.sleep(amazonEC2.TimerSleep, false);
										timerBar.increment(1);																										
									}
									if (timerEnabled)
									{													
										"Timer is completed stopping {0} instances now".debug(instancesToStop.nodes().size());
										foreach(var node in instancesToStop.nodes())
											amazonEC2.stopInstance((RunningInstance)node.get_Tag());
									}
									else
										"Timer was stopped so nothing to do".debug();			
									startTimerLink.enabled(true);
									stopTimerLink.enabled(false);

							 };
			stopTimer = ()=>{
								
									"stopping timer".info();
									timerEnabled = false; 
									
									startTimerLink.enabled(true);
									stopTimerLink.enabled(false);
							 };
			targetPanel.onClosed(()=> 	timerEnabled=false);					 
			
			return amazonEC2;
		}
	}
	
}
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.XRules.Database.ExtensionMethods;
using Irony.Parsing;
using O2.Script;

//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_VisualizationAPI.cs

namespace O2.Script
{
	public class createTestGui
	{
	
		public static void createAndLaunch()
		{
			// Script with Irony parser (which is the current file	 
			var targetFile = @"C:\O2\_XRules_Local\H2Scripts\To Blog about\Irony Parser - CmdExe_Dir.cs";
			
			// create Gui
			var formPanel = O2Gui.open<Panel>("Irony Parser test - CmdExe_Dir", 800,400);
			var controls = formPanel.add_1x1("Script", "Dynamic Excution",false,300);  
			var sourceCodeEditor = controls[0].add_SourceCodeEditor();
			var script = controls[1].add_Script(false);
			var textBox = script.insert_Below<TextBox>(200).multiLine().wordWrap(false).scrollBars();
			var treeView = textBox.insert_Right<TreeView>(); 
								
			
			// inject objects into Script 
			script.InvocationParameters.add("textBox", textBox); 
			script.InvocationParameters.add("treeView", treeView); 
			
			// setup events
			textBox.onTextChange((text)=> script.execute());
			script.onCompilationOk = ()=> script.execute();
			
			//load test data
			sourceCodeEditor.open(targetFile);
			textBox.set_Text(CmdExe_Dir.getTestData());
			script.set_Command(CmdExe_Dir.getTestCommands().line().line() + "//O2File:".add(targetFile));		
		}
	}
	
	public class CmdExe_Dir
	{	 
		public string TextParsed { get; set; }
		public Parser Parser { get; set; }
		public ParseTree ParseTree { get; set;}
		public List<ParserMessage> ParserMessages { get; set;}
		 
		public static string getTestCommands()
		{
			return "var grammar = new CmdExe_Dir();".line() + 
				   "grammar.parseText(textBox.Text);".line() +
				   "grammar.showInTreeView(treeView);";
		}
		
		public static string getTestData()
		{
			return exec.cmdViaConsole("cmd",@"/c dir c:\O2");		
		}
		
		public void parseText(string textToParse)
		{
			TextParsed = textToParse;
			var grammar = new DirGrammar(); 
			Parser = new Parser(grammar); 
			ParseTree = Parser.Parse(TextParsed);  			 
			ParserMessages = ParseTree.ParserMessages;
		}
		
		public TreeView showInTreeView(TreeView treeView)
		{
			if (ParseTree != null)
			{
				treeView.clear(); 
				if (ParseTree.HasErrors())
				{
					var errorNode = treeView.add_Node("Parse errors!!").setTextColor(Color.Red);
					foreach(var message in ParserMessages)
					{
						var noteText  = "{0} [{1} : {2}]".format(message, message.Location.Line, message.Location.Column);
						errorNode.add_Node(noteText); 
						
					}
					treeView.expand();
				}
				else			
				{							 
					treeView.removeEventHandlers_BeforeExpand();   				 
					treeView.beforeExpand<ParseTreeNode>( 
						(parseTreeNode)=> 
							{
								"in before expand".debug();
								var currentNode = treeView.current();
								currentNode.clear(); 			
								foreach(var child in parseTreeNode.ChildNodes)
								{
									if (child.Token == null) 
										currentNode.add_Node(child.Term.Name ,child,true);	
									else
										currentNode.add_Node(child.Token.ValueString,child);			
								}				
							});
					if (ParseTree != null && ParseTree.Root != null)
					{
						treeView.add_Node("Raw ParseTree Data")
						        .add_Node(ParseTree.Root.Term.Name, ParseTree.Root, true);
						//treeView.expandAll(); 
						
						treeView.add_Node("DirResult Object", dirResultObject(), true);					
						treeView.autoExpandObjects<DirResult>();
						treeView.autoExpandObjects<DirResult.File>();
						treeView.autoExpandObjects<DirResult.Dir>();					
						treeView.selectFirst();					
						treeView.expand(); 
					}															
				}
			}
			return treeView;
		}
		
		public DirResult dirResultObject()
		{		
			var dirResult = new DirResult();
			if (ParseTree != null && ParseTree.HasErrors().isFalse())
			{
				foreach(var child in ParseTree.Root.ChildNodes)
				{
					switch(child.Term.Name)
					{
						case "dirDetails":
							dirResult.VolumeName = child.ChildNodes[0].Token.ValueString;
							dirResult.VolumeSerialNumber = child.ChildNodes[1].Token.ValueString;
							dirResult.DirectoryName = child.ChildNodes[2].Token.ValueString;
							break;
						case "items":
							foreach(var items in child.ChildNodes)
								foreach(var item in items.ChildNodes)
								{
									var date = item.ChildNodes[0].Token.ValueString;
									var time = item.ChildNodes[1].Token.ValueString;
									var amPm = item.ChildNodes[2].Token.ValueString;
									var size = item.ChildNodes[3].Token.ValueString;
									var name = item.ChildNodes[4].Token.ValueString;
									// hack due to parser definition prob
									if (size == "<DIR>")
										dirResult.Dirs.Add(new DirResult.Dir
											{
												Date = date,
												Time = time, 
												AmPm = amPm,											
												Name = name
											});
									else								
										dirResult.Files.Add(new DirResult.File
											{
												Date = date,
												Time = time, 
												AmPm = amPm,
												Size = size,
												Name = name
											});								
								}
									//item.Term.Name.str().info();
							break;
							
						case "dirStats":
							break;
					}
				}			
				var dir = new DirResult.Dir();
				dir.Name = "asd";
				dirResult.Dirs.Add(dir);
			}
			return dirResult;
		}
	}
	
	public class DirResult
	{
		public string VolumeName { get; set; }
		public string VolumeSerialNumber { get; set; }
		public string DirectoryName { get; set; }
		public string FilesCount { get; set; }
		public string FilesSize { get; set; }
		public string DirsCount { get; set; }
		public string DiskFreeSpace { get; set; }
		public List<File> Files { get;set;}
		public List<Dir> Dirs { get;set;}
		
		public class File
		{
			public string Date { get; set; }
			public string Time { get; set; }
			public string AmPm { get; set; }
			public string Size { get; set; }
			public string Name { get; set; }
		}
		
		public class Dir
		{
			public string Date { get; set; }
			public string Time { get; set; }
			public string AmPm { get; set; }		
			public string Name { get; set; }
		}
		
		public DirResult()
		{
			Files = new List<File>();
			Dirs = new List<Dir>();
		}		
	}
	
	[Language("DirGrammar", "0.1", "Using Irony to parse the result of cmd.exe dir")]
	public class DirGrammar : Grammar
	{	
		
		public  DirGrammar()
		{
			// Terminals	 	
		 	var driveName = new IdentifierTerminal("identifierValue", "" , Strings.DecimalDigits);	 		 
		 	var stringValue = new DsvLiteral("stringValue", TypeCode.String,null);	 	
			var date = new IdentifierTerminal("date", "/", Strings.DecimalDigits);
		 	var time = new DsvLiteral("time", TypeCode.String," ");
		 	var amPm = new DsvLiteral("AmPM", TypeCode.String," ");
		 	var size = new DsvLiteral("size", TypeCode.String," ");
		 	var name = new DsvLiteral("name", TypeCode.String,null);
			var numberWithCommas = new DsvLiteral("number", TypeCode.String," ");
		 	var number = new NumberLiteral("number");
		 	number.DecimalSeparator = ',';  // this doesn't work for numbers with format ###,###,###
		 	
		 	// Non-terminals
		 	var program = new NonTerminal("program");		
		 	var expression = new NonTerminal("expression");		
		 	var drive = new NonTerminal("drive");		 
			var serial = new NonTerminal("serial");		
			var directory = new NonTerminal("directory");		
		 	var directoryDetail = new NonTerminal("directoryDetail");		
		 	var fileDetail = new NonTerminal("fileDetail");		
		 	var item = new NonTerminal("item");	 	
		 	var items = new NonTerminal("items");		
		 	var dirDetails = new NonTerminal("dirDetails");	
		 	var dirStats = new NonTerminal("dirStats");	
		 	
		 	// BNF rules
		 	program.Rule = dirDetails + NewLineStar + items + NewLineStar +  dirStats + NewLineStar; // +  items; 
		 	dirDetails.Rule = "Volume in drive" + driveName + "has no label." + NewLine + 
		 					  "Volume Serial Number is" +  stringValue  + NewLine + NewLine +
		 					  "Directory of" + stringValue + NewLine;
	
			// another problem happens here where the directoryDetail doesn't get picket up (i.e. we always get an fileDetail)	 	
			// until I can figure out a way to support ###,###,### numbers it looks like the DsvLiteral used for size will take precedece to  ToTerm("<DIR>")
		 	directoryDetail.Rule = date + time + amPm + ToTerm("<DIR>") + name + NewLine;
		 	fileDetail.Rule = date + time + amPm + size + name + NewLine;
		 	item.Rule =  directoryDetail | fileDetail;	 	
		 	items.Rule = MakeStarRule(items, item);
		 	
		 	// this is a weird one, if I use the numberWithCommas in the dirStats.Rule, it starts to conflict with the items BnfTerm
		 	dirStats.Rule = number + "File(s)" + numberWithCommas + "bytes" +  NewLine + 
		 					  number + "Dir(s)" + numberWithCommas + "bytes free" +  NewLine;// + number ;
		 	
		 	// config	 	
		 	this.RegisterPunctuation("Volume in drive", "has no label.", "Volume Serial Number is",  "Directory of",
		 							 "File(s)" , "bytes" , "Dir(s)", "bytes free");
		 	this.Root = program;			
		 	this.LanguageFlags = LanguageFlags.CreateAst ;
		 	// this flag doesn't seem to have any effect since 
		 	// ParseTree.Root.AstNode is null
		}	
	}
}
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.ExtensionMethods;
using Irony.Parsing;
using O2.Script;

//O2File:C:\O2\_XRules_Local\ExtensionMethods\extra_VisualizationAPI.cs

// based on the code in http://www.codeproject.com/KB/recipes/YourFirstDSL.aspx
// with the code updated using snipptets from http://blogs.msdn.com/kirillosenkov/archive/2009/10/31/irony.aspx

public class IronyTest
{	
	public string TextParsed { get; set; }
	public Parser Parser { get; set; }
	public ParseTree ParseTree { get; set;}
	public List<ParserMessage> ParserMessages { get; set;}
	 
	public static string getTestCommands()
	{
		return "var ironyTest = new IronyTest();".line() + 
			   "ironyTest.parseText(textBox.Text);".line() +
				"ironyTest.showInTreeView(treeView);";
	}
	
	public static string getTestData()
	{
		return "set camera size: 400 by 300 pixels.".line() + 
			   "set camera position: 100, 100.".line() + 
			   "move 200 pixels right.".line() + 	
		 	   "move 100 pixels up.".line();
	}
	
	public void parseText(string textToParse)
	{
		TextParsed = textToParse;
		var grammar = new CameraControlGrammar(); 
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
					errorNode.add_Node(message); 
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
				treeView.add_Node(ParseTree.Root.Term.Name, ParseTree.Root, true);
				treeView.expandAll(); 
				treeView.selectFirst();
			}
		}
		return treeView;
	}
}

[Language("CameraControlGrammar", "0.1", "First Test of using Irony (using the sample CameraControlGrammar)")]
public class CameraControlGrammar : Grammar
{
	public CameraControlGrammar()
	{
	 	// Terminals
	 	var number = new NumberLiteral("number");
	 	
	 	// Non-terminals
		var program = new NonTerminal("program");
		var cameraSize = new NonTerminal("cameraSize");
		var cameraPosition = new NonTerminal("cameraPosition");
		var commandList = new NonTerminal("commandList");
		var command = new NonTerminal("command");
		var direction = new NonTerminal("direction");
		
		// BNF rules	
		program.Rule = cameraSize + cameraPosition + commandList;
		cameraSize.Rule = ToTerm("set") + "camera" + "size" + ":" + number + "by" + number + "pixels" + ".";
		cameraPosition.Rule = ToTerm("set") + "camera" + "position" + ":" + number + "," + number + ".";
		commandList.Rule = MakePlusRule(commandList, null, command);
		command.Rule = ToTerm("move") + number + "pixels" + direction + ".";
		direction.Rule = ToTerm("up") | "down" | "left" | "right";
		
		// Pontuaction
		this.MarkPunctuation("set", "camera", "size", ":", "by", "pixels", ".", "position", ",", "move");
		 
		// global options
		this.Root = program;				
	}
}
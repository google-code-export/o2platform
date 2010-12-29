// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.Windows;
using O2.DotNetWrappers.ExtensionMethods;
using O2.Views.ASCX.classes.MainGUI;
using O2.Views.ASCX.ExtensionMethods;
using Irony.Parsing;
//O2Ref:O2_Misc_Microsoft_MPL_Libs.dll
//O2File:O2MediaWikiAPI.cs
namespace O2.XRules.Database.APIs
{
	//These are massive hacks that should be rewritten once the MediaWiki Gramar is bettert
    public class WikiText_HeadersAndTemplates
    {    
		public List<string> TextParsed { get; set; }
		public ParserMessageList  ParserMessages { get; set; }
    	public Dictionary<string,List<string>> Templates { get; set; }
    	public bool UseContentLocalCache { get; set; }
		
		public WikiText_HeadersAndTemplates()
    	{
    		TextParsed = new List<string>();
    		Templates = new Dictionary<string,List<string>>();  
    		UseContentLocalCache = true;
    	}
    	
    	public WikiText_HeadersAndTemplates parse(O2MediaWikiAPI wikiApi, string page)
    	{
    		return parse(wikiApi.raw(page,UseContentLocalCache));	
    	}
    	
    	public WikiText_HeadersAndTemplates parse(string wikiText)
    	{
    		TextParsed.Add(wikiText);
    		if (wikiText.valid().isFalse())
    			return this;
    		
    		var grammar = new MediaWiki_Grammar();   
			var Parser = new Parser(grammar);    
			var ParseTree = Parser.Parse(wikiText);   
			ParserMessages = ParseTree.ParserMessages;
			var currentTemplate = "";
			foreach(var child in ParseTree.Root.ChildNodes)
			{
				var nodeText = child.Token.ValueString.trim();
				var nodeType = child.Term.Name; 
				if (nodeType == "h2")
					currentTemplate = nodeText;
				if (currentTemplate.valid())
					if (nodeType == "template")
						Templates.add(currentTemplate, nodeText);
			}
			return this;
		}	
			
    	//   public 
    }
    
   
   
   
    
    [Language("MediaWiki", "1.0", "MediaWiki markup grammar.")]
    public class MediaWiki_Grammar : Grammar
    {
    	public MediaWiki_Grammar()
    	{
	     	// Terminals  	     	
	     	var text = new WikiTextTerminal("text");  
	     	//var templateText = new DsvLiteral("stringValue", TypeCode.String," ");  
	     	
	     	//Headings
		    var h1 = new WikiBlockTerminal("h1", WikiBlockType.EscapedText, "=", "\n", "h1"); 
		    var h2 = new WikiBlockTerminal("h2", WikiBlockType.EscapedText, "==", "==", "h2"); 
		    var h3 = new WikiBlockTerminal("h3", WikiBlockType.EscapedText, "===", "\n", "h3"); 
		    var h4 = new WikiBlockTerminal("h4", WikiBlockType.EscapedText, "====", "\n", "h4"); 
		    var h5 = new WikiBlockTerminal("h5", WikiBlockType.EscapedText, "=====", "\n", "h5"); 
		    var h6 = new WikiBlockTerminal("h6", WikiBlockType.EscapedText, "======", "\n", "h6"); 
		      
		    //Block tags
     		var template = new WikiBlockTerminal("template", WikiBlockType.CodeBlock, "{{", "}}", "pre"); 
	     	
	     	
	     	// Non-terminals
	     	var wikiElement = new NonTerminal("wikiElement");
      		var wikiText = new NonTerminal("wikiText"); 
      		
      		
	     	// BNF rules
	     	
	     	wikiElement.Rule = 	//template |
	     						text	|
	     						h1 | h2 | h3 | h4 | h5 | h6 |
	     						template |
	     					   	NewLine
	     					   	;
	     	wikiText.Rule = MakeStarRule(wikiText, wikiElement); 
	     	
	  		// config               
			this.Root =  wikiText; 
     	 	this.WhitespaceChars = string.Empty;
      		MarkTransient(wikiElement); 
			NewLine.SetFlag(TermFlags.IsPunctuation, false); 
      		this.LanguageFlags |= LanguageFlags.DisableScannerParserLink | LanguageFlags.NewLineBeforeEOF | LanguageFlags.CanRunSample
      							| LanguageFlags.CreateAst ;
;
		}
		
		public TreeView showInTreeView(TreeView treeView, ParseTree ParseTree)
                {
                        if (ParseTree != null)
                        {
                                treeView.clear(); 
                                if (ParseTree.HasErrors())
                                {
                                        var errorNode = treeView.add_Node("Parse errors!!").setTextColor(Color.Red);
                                        foreach(var message in ParseTree.ParserMessages)
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
                                                                                currentNode.add_Node(child.Term.Name + " : " + child.Token.ValueString,child);                    
                                                                }                               
                                                        });
                                        if (ParseTree != null && ParseTree.Root != null)
                                        {
                                                treeView//.add_Node("Raw ParseTree Data")
                                                        .add_Node(ParseTree.Root.Term.Name, ParseTree.Root, true);
                                                //treeView.expandAll(); 
                                                
                                                //treeView.add_Node("DirResult Object", dirResultObject(), true);                                 
                                                //treeView.autoExpandObjects<DirResult>();
                                                //treeView.autoExpandObjects<DirResult.File>();
                                                //treeView.autoExpandObjects<DirResult.Dir>();                                    
                                                treeView.selectFirst();                                 
                                                treeView.expand(); 
                                        }                                                                                                                       
                                }
                        }
                        return treeView;
                }

    }

}

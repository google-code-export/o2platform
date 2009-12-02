// $ANTLR 3.0.1 C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g 2008-01-14 11:06:20

using System.Text;
using System.Globalization;
using System.Collections.Generic;
using Evaluant.NLinq.Expressions;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;




using Antlr.Runtime.Tree;

public class NLinqParser : Parser 
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"INTEGER", 
		"FLOAT", 
		"STRING", 
		"ID", 
		"LETTER", 
		"DIGIT", 
		"EscapeSequence", 
		"NULL", 
		"UnicodeEscape", 
		"HexDigit", 
		"WS", 
		"'['", 
		"']'", 
		"'?'", 
		"':'", 
		"'||'", 
		"'&&'", 
		"'=='", 
		"'!='", 
		"'<'", 
		"'<='", 
		"'>'", 
		"'>='", 
		"'+'", 
		"'-'", 
		"'*'", 
		"'/'", 
		"'%'", 
		"'!'", 
		"'.'", 
		"'('", 
		"')'", 
		"','", 
		"'=>'", 
		"'new'", 
		"'{'", 
		"'='", 
		"'}'", 
		"'from'", 
		"'in'", 
		"'into'", 
		"'where'", 
		"'let'", 
		"'join'", 
		"'on'", 
		"'equals'", 
		"'orderby'", 
		"'ascending'", 
		"'descending'", 
		"'select'", 
		"'group'", 
		"'by'", 
		"'DateTime'", 
		"'true'", 
		"'false'"
    };

    public const int INTEGER = 4;
    public const int WS = 14;
    public const int LETTER = 8;
    public const int UnicodeEscape = 12;
    public const int NULL = 11;
    public const int FLOAT = 5;
    public const int DIGIT = 9;
    public const int ID = 7;
    public const int EOF = -1;
    public const int HexDigit = 13;
    public const int EscapeSequence = 10;
    public const int STRING = 6;
    
    
        public NLinqParser(ITokenStream input) 
    		: base(input)
    	{
    		InitializeCyclicDFAs();
        }
        
    protected ITreeAdaptor adaptor = new CommonTreeAdaptor();
    
    public ITreeAdaptor TreeAdaptor
    {
        get { return this.adaptor; }
        set { this.adaptor = value; }
    }

    override public string[] TokenNames
	{
		get { return tokenNames; }
	}

    override public string GrammarFileName
	{
		get { return "C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g"; }
	}

    
    private const char BS = '\\';
    private static NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
    
    private string extractString(string text) {
        
        StringBuilder sb = new StringBuilder(text);
        int startIndex = 1; // Skip initial quote
        int slashIndex = -1;
    
        while ((slashIndex = sb.ToString().IndexOf(BS, startIndex)) != -1)
        {
            char escapeType = sb[slashIndex + 1];
            switch (escapeType)
            {
                case 'u':
                  string hcode = String.Concat(sb[slashIndex+4], sb[slashIndex+5]);
                  string lcode = String.Concat(sb[slashIndex+2], sb[slashIndex+3]);
                  char unicodeChar = Encoding.Unicode.GetChars(new byte[] { System.Convert.ToByte(hcode, 16), System.Convert.ToByte(lcode, 16)} )[0];
                  sb.Remove(slashIndex, 6).Insert(slashIndex, unicodeChar); 
                  break;
                case 'n': sb.Remove(slashIndex, 2).Insert(slashIndex, '\n'); break;
                case 'r': sb.Remove(slashIndex, 2).Insert(slashIndex, '\r'); break;
                case 't': sb.Remove(slashIndex, 2).Insert(slashIndex, '\t'); break;
                case '\'': sb.Remove(slashIndex, 2).Insert(slashIndex, '\''); break;
                case '\\': sb.Remove(slashIndex, 2).Insert(slashIndex, '\\'); break;
                default: throw new RecognitionException("Unvalid escape sequence: \\" + escapeType);
            }
    
            startIndex = slashIndex + 1;
    
        }
    
        sb.Remove(0, 1);
        sb.Remove(sb.Length - 1, 1);
    
        return sb.ToString();
    }


    public class linqExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start linqExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:61:1: linqExpression returns [Expression value] : expression EOF ;
    public linqExpression_return linqExpression() // throws RecognitionException [1]
    {   
        linqExpression_return retval = new linqExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken EOF2 = null;
        expression_return expression1 = null;
        
        
        CommonTree EOF2_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:62:2: ( expression EOF )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:62:4: expression EOF
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_expression_in_linqExpression56);
            	expression1 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression1.Tree);
            	EOF2 = (IToken)input.LT(1);
            	Match(input,EOF,FOLLOW_EOF_in_linqExpression58); 
            	retval.value =  expression1.value; 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end linqExpression

    public class expression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start expression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:65:1: expression returns [Expression value] : expressionItem ( '[' expression ']' )? ;
    public expression_return expression() // throws RecognitionException [1]
    {   
        expression_return retval = new expression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal4 = null;
        IToken char_literal6 = null;
        expressionItem_return expressionItem3 = null;

        expression_return expression5 = null;
        
        
        CommonTree char_literal4_tree=null;
        CommonTree char_literal6_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:66:2: ( expressionItem ( '[' expression ']' )? )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:66:4: expressionItem ( '[' expression ']' )?
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_expressionItem_in_expression76);
            	expressionItem3 = expressionItem();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expressionItem3.Tree);
            	retval.value =  expressionItem3.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:66:54: ( '[' expression ']' )?
            	int alt1 = 2;
            	int LA1_0 = input.LA(1);
            	
            	if ( (LA1_0 == 15) )
            	{
            	    alt1 = 1;
            	}
            	switch (alt1) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:66:55: '[' expression ']'
            	        {
            	        	char_literal4 = (IToken)input.LT(1);
            	        	Match(input,15,FOLLOW_15_in_expression81); 
            	        	char_literal4_tree = (CommonTree)adaptor.Create(char_literal4);
            	        	adaptor.AddChild(root_0, char_literal4_tree);

            	        	PushFollow(FOLLOW_expression_in_expression83);
            	        	expression5 = expression();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, expression5.Tree);
            	        	char_literal6 = (IToken)input.LT(1);
            	        	Match(input,16,FOLLOW_16_in_expression85); 
            	        	char_literal6_tree = (CommonTree)adaptor.Create(char_literal6);
            	        	adaptor.AddChild(root_0, char_literal6_tree);

            	        	 retval.value =  new Indexer(expressionItem3.value, retval.value); 
            	        
            	        }
            	        break;
            	
            	}

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end expression

    public class expressionItem_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start expressionItem
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:69:1: expressionItem returns [Expression value] : left= conditionalExpression ( '?' middle= conditionalExpression ':' right= conditionalExpression )? ;
    public expressionItem_return expressionItem() // throws RecognitionException [1]
    {   
        expressionItem_return retval = new expressionItem_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal7 = null;
        IToken char_literal8 = null;
        conditionalExpression_return left = null;

        conditionalExpression_return middle = null;

        conditionalExpression_return right = null;
        
        
        CommonTree char_literal7_tree=null;
        CommonTree char_literal8_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:70:2: (left= conditionalExpression ( '?' middle= conditionalExpression ':' right= conditionalExpression )? )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:70:4: left= conditionalExpression ( '?' middle= conditionalExpression ':' right= conditionalExpression )?
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_conditionalExpression_in_expressionItem108);
            	left = conditionalExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:70:57: ( '?' middle= conditionalExpression ':' right= conditionalExpression )?
            	int alt2 = 2;
            	int LA2_0 = input.LA(1);
            	
            	if ( (LA2_0 == 17) )
            	{
            	    alt2 = 1;
            	}
            	switch (alt2) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:70:59: '?' middle= conditionalExpression ':' right= conditionalExpression
            	        {
            	        	char_literal7 = (IToken)input.LT(1);
            	        	Match(input,17,FOLLOW_17_in_expressionItem114); 
            	        	char_literal7_tree = (CommonTree)adaptor.Create(char_literal7);
            	        	adaptor.AddChild(root_0, char_literal7_tree);

            	        	PushFollow(FOLLOW_conditionalExpression_in_expressionItem118);
            	        	middle = conditionalExpression();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, middle.Tree);
            	        	char_literal8 = (IToken)input.LT(1);
            	        	Match(input,18,FOLLOW_18_in_expressionItem120); 
            	        	char_literal8_tree = (CommonTree)adaptor.Create(char_literal8);
            	        	adaptor.AddChild(root_0, char_literal8_tree);

            	        	PushFollow(FOLLOW_conditionalExpression_in_expressionItem124);
            	        	right = conditionalExpression();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, right.Tree);
            	        	 retval.value =  new TernaryExpression(left.value, middle.value, right.value); 
            	        
            	        }
            	        break;
            	
            	}

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end expressionItem

    public class conditionalExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start conditionalExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:73:1: conditionalExpression returns [Expression value] : left= booleanAndExpression ( '||' right= booleanAndExpression )* ;
    public conditionalExpression_return conditionalExpression() // throws RecognitionException [1]
    {   
        conditionalExpression_return retval = new conditionalExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal9 = null;
        booleanAndExpression_return left = null;

        booleanAndExpression_return right = null;
        
        
        CommonTree string_literal9_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:77:2: (left= booleanAndExpression ( '||' right= booleanAndExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:77:4: left= booleanAndExpression ( '||' right= booleanAndExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_booleanAndExpression_in_conditionalExpression151);
            	left = booleanAndExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:77:56: ( '||' right= booleanAndExpression )*
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);
            	    
            	    if ( (LA3_0 == 19) )
            	    {
            	        alt3 = 1;
            	    }
            	    
            	
            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:78:4: '||' right= booleanAndExpression
            			    {
            			    	string_literal9 = (IToken)input.LT(1);
            			    	Match(input,19,FOLLOW_19_in_conditionalExpression160); 
            			    	string_literal9_tree = (CommonTree)adaptor.Create(string_literal9);
            			    	adaptor.AddChild(root_0, string_literal9_tree);

            			    	 type = BinaryExpressionType.Or; 
            			    	PushFollow(FOLLOW_booleanAndExpression_in_conditionalExpression170);
            			    	right = booleanAndExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, retval.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop3;
            	    }
            	} while (true);
            	
            	loop3:
            		;	// Stops C# compiler whinging that label 'loop3' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end conditionalExpression

    public class booleanAndExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start booleanAndExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:83:1: booleanAndExpression returns [Expression value] : left= equalityExpression ( '&&' right= equalityExpression )* ;
    public booleanAndExpression_return booleanAndExpression() // throws RecognitionException [1]
    {   
        booleanAndExpression_return retval = new booleanAndExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal10 = null;
        equalityExpression_return left = null;

        equalityExpression_return right = null;
        
        
        CommonTree string_literal10_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:87:2: (left= equalityExpression ( '&&' right= equalityExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:87:4: left= equalityExpression ( '&&' right= equalityExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_equalityExpression_in_booleanAndExpression204);
            	left = equalityExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:87:54: ( '&&' right= equalityExpression )*
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);
            	    
            	    if ( (LA4_0 == 20) )
            	    {
            	        alt4 = 1;
            	    }
            	    
            	
            	    switch (alt4) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:88:4: '&&' right= equalityExpression
            			    {
            			    	string_literal10 = (IToken)input.LT(1);
            			    	Match(input,20,FOLLOW_20_in_booleanAndExpression213); 
            			    	string_literal10_tree = (CommonTree)adaptor.Create(string_literal10);
            			    	adaptor.AddChild(root_0, string_literal10_tree);

            			    	 type = BinaryExpressionType.And; 
            			    	PushFollow(FOLLOW_equalityExpression_in_booleanAndExpression223);
            			    	right = equalityExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, retval.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop4;
            	    }
            	} while (true);
            	
            	loop4:
            		;	// Stops C# compiler whinging that label 'loop4' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end booleanAndExpression

    public class equalityExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start equalityExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:93:1: equalityExpression returns [Expression value] : left= relationalExpression ( ( '==' | '!=' ) right= relationalExpression )* ;
    public equalityExpression_return equalityExpression() // throws RecognitionException [1]
    {   
        equalityExpression_return retval = new equalityExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal11 = null;
        IToken string_literal12 = null;
        relationalExpression_return left = null;

        relationalExpression_return right = null;
        
        
        CommonTree string_literal11_tree=null;
        CommonTree string_literal12_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:97:2: (left= relationalExpression ( ( '==' | '!=' ) right= relationalExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:97:4: left= relationalExpression ( ( '==' | '!=' ) right= relationalExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_relationalExpression_in_equalityExpression255);
            	left = relationalExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:97:56: ( ( '==' | '!=' ) right= relationalExpression )*
            	do 
            	{
            	    int alt6 = 2;
            	    int LA6_0 = input.LA(1);
            	    
            	    if ( (LA6_0 == 21) )
            	    {
            	        alt6 = 1;
            	    }
            	    else if ( (LA6_0 == 22) )
            	    {
            	        alt6 = 1;
            	    }
            	    
            	
            	    switch (alt6) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:98:4: ( '==' | '!=' ) right= relationalExpression
            			    {
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:98:4: ( '==' | '!=' )
            			    	int alt5 = 2;
            			    	int LA5_0 = input.LA(1);
            			    	
            			    	if ( (LA5_0 == 21) )
            			    	{
            			    	    alt5 = 1;
            			    	}
            			    	else if ( (LA5_0 == 22) )
            			    	{
            			    	    alt5 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    NoViableAltException nvae_d5s0 =
            			    	        new NoViableAltException("98:4: ( '==' | '!=' )", 5, 0, input);
            			    	
            			    	    throw nvae_d5s0;
            			    	}
            			    	switch (alt5) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:98:6: '=='
            			    	        {
            			    	        	string_literal11 = (IToken)input.LT(1);
            			    	        	Match(input,21,FOLLOW_21_in_equalityExpression266); 
            			    	        	string_literal11_tree = (CommonTree)adaptor.Create(string_literal11);
            			    	        	adaptor.AddChild(root_0, string_literal11_tree);

            			    	        	 type = BinaryExpressionType.Equal; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:99:6: '!='
            			    	        {
            			    	        	string_literal12 = (IToken)input.LT(1);
            			    	        	Match(input,22,FOLLOW_22_in_equalityExpression276); 
            			    	        	string_literal12_tree = (CommonTree)adaptor.Create(string_literal12);
            			    	        	adaptor.AddChild(root_0, string_literal12_tree);

            			    	        	 type = BinaryExpressionType.NotEqual; 
            			    	        
            			    	        }
            			    	        break;
            			    	
            			    	}

            			    	PushFollow(FOLLOW_relationalExpression_in_equalityExpression288);
            			    	right = relationalExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, retval.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop6;
            	    }
            	} while (true);
            	
            	loop6:
            		;	// Stops C# compiler whinging that label 'loop6' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end equalityExpression

    public class relationalExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start relationalExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:104:1: relationalExpression returns [Expression value] : left= additiveExpression ( ( '<' | '<=' | '>' | '>=' ) right= additiveExpression )* ;
    public relationalExpression_return relationalExpression() // throws RecognitionException [1]
    {   
        relationalExpression_return retval = new relationalExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal13 = null;
        IToken string_literal14 = null;
        IToken char_literal15 = null;
        IToken string_literal16 = null;
        additiveExpression_return left = null;

        additiveExpression_return right = null;
        
        
        CommonTree char_literal13_tree=null;
        CommonTree string_literal14_tree=null;
        CommonTree char_literal15_tree=null;
        CommonTree string_literal16_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:108:2: (left= additiveExpression ( ( '<' | '<=' | '>' | '>=' ) right= additiveExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:108:4: left= additiveExpression ( ( '<' | '<=' | '>' | '>=' ) right= additiveExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_additiveExpression_in_relationalExpression321);
            	left = additiveExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:108:54: ( ( '<' | '<=' | '>' | '>=' ) right= additiveExpression )*
            	do 
            	{
            	    int alt8 = 2;
            	    switch ( input.LA(1) ) 
            	    {
            	    case 23:
            	    	{
            	        alt8 = 1;
            	        }
            	        break;
            	    case 24:
            	    	{
            	        alt8 = 1;
            	        }
            	        break;
            	    case 25:
            	    	{
            	        alt8 = 1;
            	        }
            	        break;
            	    case 26:
            	    	{
            	        alt8 = 1;
            	        }
            	        break;
            	    
            	    }
            	
            	    switch (alt8) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:109:4: ( '<' | '<=' | '>' | '>=' ) right= additiveExpression
            			    {
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:109:4: ( '<' | '<=' | '>' | '>=' )
            			    	int alt7 = 4;
            			    	switch ( input.LA(1) ) 
            			    	{
            			    	case 23:
            			    		{
            			    	    alt7 = 1;
            			    	    }
            			    	    break;
            			    	case 24:
            			    		{
            			    	    alt7 = 2;
            			    	    }
            			    	    break;
            			    	case 25:
            			    		{
            			    	    alt7 = 3;
            			    	    }
            			    	    break;
            			    	case 26:
            			    		{
            			    	    alt7 = 4;
            			    	    }
            			    	    break;
            			    		default:
            			    		    NoViableAltException nvae_d7s0 =
            			    		        new NoViableAltException("109:4: ( '<' | '<=' | '>' | '>=' )", 7, 0, input);
            			    	
            			    		    throw nvae_d7s0;
            			    	}
            			    	
            			    	switch (alt7) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:109:6: '<'
            			    	        {
            			    	        	char_literal13 = (IToken)input.LT(1);
            			    	        	Match(input,23,FOLLOW_23_in_relationalExpression332); 
            			    	        	char_literal13_tree = (CommonTree)adaptor.Create(char_literal13);
            			    	        	adaptor.AddChild(root_0, char_literal13_tree);

            			    	        	 type = BinaryExpressionType.Lesser; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:110:6: '<='
            			    	        {
            			    	        	string_literal14 = (IToken)input.LT(1);
            			    	        	Match(input,24,FOLLOW_24_in_relationalExpression342); 
            			    	        	string_literal14_tree = (CommonTree)adaptor.Create(string_literal14);
            			    	        	adaptor.AddChild(root_0, string_literal14_tree);

            			    	        	 type = BinaryExpressionType.LesserOrEqual; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 3 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:111:6: '>'
            			    	        {
            			    	        	char_literal15 = (IToken)input.LT(1);
            			    	        	Match(input,25,FOLLOW_25_in_relationalExpression353); 
            			    	        	char_literal15_tree = (CommonTree)adaptor.Create(char_literal15);
            			    	        	adaptor.AddChild(root_0, char_literal15_tree);

            			    	        	 type = BinaryExpressionType.Greater; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 4 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:112:6: '>='
            			    	        {
            			    	        	string_literal16 = (IToken)input.LT(1);
            			    	        	Match(input,26,FOLLOW_26_in_relationalExpression363); 
            			    	        	string_literal16_tree = (CommonTree)adaptor.Create(string_literal16);
            			    	        	adaptor.AddChild(root_0, string_literal16_tree);

            			    	        	 type = BinaryExpressionType.GreaterOrEqual; 
            			    	        
            			    	        }
            			    	        break;
            			    	
            			    	}

            			    	PushFollow(FOLLOW_additiveExpression_in_relationalExpression375);
            			    	right = additiveExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, retval.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop8;
            	    }
            	} while (true);
            	
            	loop8:
            		;	// Stops C# compiler whinging that label 'loop8' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end relationalExpression

    public class additiveExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start additiveExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:117:1: additiveExpression returns [Expression value] : left= multiplicativeExpression ( ( '+' | '-' ) right= multiplicativeExpression )* ;
    public additiveExpression_return additiveExpression() // throws RecognitionException [1]
    {   
        additiveExpression_return retval = new additiveExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal17 = null;
        IToken char_literal18 = null;
        multiplicativeExpression_return left = null;

        multiplicativeExpression_return right = null;
        
        
        CommonTree char_literal17_tree=null;
        CommonTree char_literal18_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:121:2: (left= multiplicativeExpression ( ( '+' | '-' ) right= multiplicativeExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:121:4: left= multiplicativeExpression ( ( '+' | '-' ) right= multiplicativeExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_multiplicativeExpression_in_additiveExpression407);
            	left = multiplicativeExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:121:60: ( ( '+' | '-' ) right= multiplicativeExpression )*
            	do 
            	{
            	    int alt10 = 2;
            	    int LA10_0 = input.LA(1);
            	    
            	    if ( (LA10_0 == 27) )
            	    {
            	        alt10 = 1;
            	    }
            	    else if ( (LA10_0 == 28) )
            	    {
            	        alt10 = 1;
            	    }
            	    
            	
            	    switch (alt10) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:122:4: ( '+' | '-' ) right= multiplicativeExpression
            			    {
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:122:4: ( '+' | '-' )
            			    	int alt9 = 2;
            			    	int LA9_0 = input.LA(1);
            			    	
            			    	if ( (LA9_0 == 27) )
            			    	{
            			    	    alt9 = 1;
            			    	}
            			    	else if ( (LA9_0 == 28) )
            			    	{
            			    	    alt9 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    NoViableAltException nvae_d9s0 =
            			    	        new NoViableAltException("122:4: ( '+' | '-' )", 9, 0, input);
            			    	
            			    	    throw nvae_d9s0;
            			    	}
            			    	switch (alt9) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:122:6: '+'
            			    	        {
            			    	        	char_literal17 = (IToken)input.LT(1);
            			    	        	Match(input,27,FOLLOW_27_in_additiveExpression418); 
            			    	        	char_literal17_tree = (CommonTree)adaptor.Create(char_literal17);
            			    	        	adaptor.AddChild(root_0, char_literal17_tree);

            			    	        	 type = BinaryExpressionType.Plus; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:123:6: '-'
            			    	        {
            			    	        	char_literal18 = (IToken)input.LT(1);
            			    	        	Match(input,28,FOLLOW_28_in_additiveExpression428); 
            			    	        	char_literal18_tree = (CommonTree)adaptor.Create(char_literal18);
            			    	        	adaptor.AddChild(root_0, char_literal18_tree);

            			    	        	 type = BinaryExpressionType.Minus; 
            			    	        
            			    	        }
            			    	        break;
            			    	
            			    	}

            			    	PushFollow(FOLLOW_multiplicativeExpression_in_additiveExpression440);
            			    	right = multiplicativeExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, retval.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop10;
            	    }
            	} while (true);
            	
            	loop10:
            		;	// Stops C# compiler whinging that label 'loop10' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end additiveExpression

    public class multiplicativeExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start multiplicativeExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:128:1: multiplicativeExpression returns [Expression value] : left= unaryExpression ( ( '*' | '/' | '%' ) right= unaryExpression )* ;
    public multiplicativeExpression_return multiplicativeExpression() // throws RecognitionException [1]
    {   
        multiplicativeExpression_return retval = new multiplicativeExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal19 = null;
        IToken char_literal20 = null;
        IToken char_literal21 = null;
        unaryExpression_return left = null;

        unaryExpression_return right = null;
        
        
        CommonTree char_literal19_tree=null;
        CommonTree char_literal20_tree=null;
        CommonTree char_literal21_tree=null;
    
        
        BinaryExpressionType type = BinaryExpressionType.Unknown;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:132:2: (left= unaryExpression ( ( '*' | '/' | '%' ) right= unaryExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:132:4: left= unaryExpression ( ( '*' | '/' | '%' ) right= unaryExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_unaryExpression_in_multiplicativeExpression472);
            	left = unaryExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, left.Tree);
            	 retval.value =  left.value; 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:132:51: ( ( '*' | '/' | '%' ) right= unaryExpression )*
            	do 
            	{
            	    int alt12 = 2;
            	    switch ( input.LA(1) ) 
            	    {
            	    case 29:
            	    	{
            	        alt12 = 1;
            	        }
            	        break;
            	    case 30:
            	    	{
            	        alt12 = 1;
            	        }
            	        break;
            	    case 31:
            	    	{
            	        alt12 = 1;
            	        }
            	        break;
            	    
            	    }
            	
            	    switch (alt12) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:133:4: ( '*' | '/' | '%' ) right= unaryExpression
            			    {
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:133:4: ( '*' | '/' | '%' )
            			    	int alt11 = 3;
            			    	switch ( input.LA(1) ) 
            			    	{
            			    	case 29:
            			    		{
            			    	    alt11 = 1;
            			    	    }
            			    	    break;
            			    	case 30:
            			    		{
            			    	    alt11 = 2;
            			    	    }
            			    	    break;
            			    	case 31:
            			    		{
            			    	    alt11 = 3;
            			    	    }
            			    	    break;
            			    		default:
            			    		    NoViableAltException nvae_d11s0 =
            			    		        new NoViableAltException("133:4: ( '*' | '/' | '%' )", 11, 0, input);
            			    	
            			    		    throw nvae_d11s0;
            			    	}
            			    	
            			    	switch (alt11) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:133:6: '*'
            			    	        {
            			    	        	char_literal19 = (IToken)input.LT(1);
            			    	        	Match(input,29,FOLLOW_29_in_multiplicativeExpression483); 
            			    	        	char_literal19_tree = (CommonTree)adaptor.Create(char_literal19);
            			    	        	adaptor.AddChild(root_0, char_literal19_tree);

            			    	        	 type = BinaryExpressionType.Times; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:134:6: '/'
            			    	        {
            			    	        	char_literal20 = (IToken)input.LT(1);
            			    	        	Match(input,30,FOLLOW_30_in_multiplicativeExpression493); 
            			    	        	char_literal20_tree = (CommonTree)adaptor.Create(char_literal20);
            			    	        	adaptor.AddChild(root_0, char_literal20_tree);

            			    	        	 type = BinaryExpressionType.Div; 
            			    	        
            			    	        }
            			    	        break;
            			    	    case 3 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:135:6: '%'
            			    	        {
            			    	        	char_literal21 = (IToken)input.LT(1);
            			    	        	Match(input,31,FOLLOW_31_in_multiplicativeExpression503); 
            			    	        	char_literal21_tree = (CommonTree)adaptor.Create(char_literal21);
            			    	        	adaptor.AddChild(root_0, char_literal21_tree);

            			    	        	 type = BinaryExpressionType.Modulo; 
            			    	        
            			    	        }
            			    	        break;
            			    	
            			    	}

            			    	PushFollow(FOLLOW_unaryExpression_in_multiplicativeExpression515);
            			    	right = unaryExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, right.Tree);
            			    	 retval.value =  new BinaryExpression(type, left.value, right.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop12;
            	    }
            	} while (true);
            	
            	loop12:
            		;	// Stops C# compiler whinging that label 'loop12' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end multiplicativeExpression

    public class unaryExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start unaryExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:141:1: unaryExpression returns [Expression value] : ( statement | '!' statement | '-' statement );
    public unaryExpression_return unaryExpression() // throws RecognitionException [1]
    {   
        unaryExpression_return retval = new unaryExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal23 = null;
        IToken char_literal25 = null;
        statement_return statement22 = null;

        statement_return statement24 = null;

        statement_return statement26 = null;
        
        
        CommonTree char_literal23_tree=null;
        CommonTree char_literal25_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:142:2: ( statement | '!' statement | '-' statement )
            int alt13 = 3;
            switch ( input.LA(1) ) 
            {
            case INTEGER:
            case FLOAT:
            case STRING:
            case ID:
            case 34:
            case 38:
            case 42:
            case 57:
            case 58:
            	{
                alt13 = 1;
                }
                break;
            case 32:
            	{
                alt13 = 2;
                }
                break;
            case 28:
            	{
                alt13 = 3;
                }
                break;
            	default:
            	    NoViableAltException nvae_d13s0 =
            	        new NoViableAltException("141:1: unaryExpression returns [Expression value] : ( statement | '!' statement | '-' statement );", 13, 0, input);
            
            	    throw nvae_d13s0;
            }
            
            switch (alt13) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:142:4: statement
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_statement_in_unaryExpression542);
                    	statement22 = statement();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, statement22.Tree);
                    	 retval.value =  statement22.value; 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:143:8: '!' statement
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	char_literal23 = (IToken)input.LT(1);
                    	Match(input,32,FOLLOW_32_in_unaryExpression553); 
                    	char_literal23_tree = (CommonTree)adaptor.Create(char_literal23);
                    	adaptor.AddChild(root_0, char_literal23_tree);

                    	PushFollow(FOLLOW_statement_in_unaryExpression555);
                    	statement24 = statement();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, statement24.Tree);
                    	 retval.value =  new UnaryExpression(UnaryExpressionType.Not, statement24.value); 
                    
                    }
                    break;
                case 3 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:144:8: '-' statement
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	char_literal25 = (IToken)input.LT(1);
                    	Match(input,28,FOLLOW_28_in_unaryExpression566); 
                    	char_literal25_tree = (CommonTree)adaptor.Create(char_literal25);
                    	adaptor.AddChild(root_0, char_literal25_tree);

                    	PushFollow(FOLLOW_statement_in_unaryExpression568);
                    	statement26 = statement();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, statement26.Tree);
                    	 retval.value =  new UnaryExpression(UnaryExpressionType.Negate, statement26.value); 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end unaryExpression

    public class statement_return : ParserRuleReturnScope 
    {
        public Statement value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start statement
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:147:1: statement returns [Statement value] : first= primaryExpression ( '.' follow= primaryExpression )* ;
    public statement_return statement() // throws RecognitionException [1]
    {   
        statement_return retval = new statement_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal27 = null;
        primaryExpression_return first = null;

        primaryExpression_return follow = null;
        
        
        CommonTree char_literal27_tree=null;
    
        
        List<Expression> expressions = new List<Expression>();
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:151:2: (first= primaryExpression ( '.' follow= primaryExpression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:151:4: first= primaryExpression ( '.' follow= primaryExpression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_primaryExpression_in_statement594);
            	first = primaryExpression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, first.Tree);
            	 expressions.Add(first.value); 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:151:63: ( '.' follow= primaryExpression )*
            	do 
            	{
            	    int alt14 = 2;
            	    int LA14_0 = input.LA(1);
            	    
            	    if ( (LA14_0 == 33) )
            	    {
            	        alt14 = 1;
            	    }
            	    
            	
            	    switch (alt14) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:151:65: '.' follow= primaryExpression
            			    {
            			    	char_literal27 = (IToken)input.LT(1);
            			    	Match(input,33,FOLLOW_33_in_statement600); 
            			    	char_literal27_tree = (CommonTree)adaptor.Create(char_literal27);
            			    	adaptor.AddChild(root_0, char_literal27_tree);

            			    	PushFollow(FOLLOW_primaryExpression_in_statement604);
            			    	follow = primaryExpression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, follow.Tree);
            			    	 expressions.Add(follow.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop14;
            	    }
            	} while (true);
            	
            	loop14:
            		;	// Stops C# compiler whinging that label 'loop14' has no statements

            	 retval.value =  new Statement(expressions.ToArray()); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end statement

    public class primaryExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start primaryExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );
    public primaryExpression_return primaryExpression() // throws RecognitionException [1]
    {   
        primaryExpression_return retval = new primaryExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal28 = null;
        IToken char_literal30 = null;
        value_return expr = null;

        expression_return expression29 = null;

        newExpression_return newExpression31 = null;

        identifier_return identifier32 = null;

        arguments_return arguments33 = null;

        methodCall_return methodCall34 = null;

        queryExpression_return queryExpression35 = null;
        
        
        CommonTree char_literal28_tree=null;
        CommonTree char_literal30_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:157:2: ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression )
            int alt16 = 6;
            switch ( input.LA(1) ) 
            {
            case 34:
            	{
                alt16 = 1;
                }
                break;
            case INTEGER:
            case FLOAT:
            case STRING:
            case 57:
            case 58:
            	{
                alt16 = 2;
                }
                break;
            case 38:
            	{
                int LA16_3 = input.LA(2);
                
                if ( (LA16_3 == 56) )
                {
                    alt16 = 2;
                }
                else if ( (LA16_3 == ID || LA16_3 == 39) )
                {
                    alt16 = 3;
                }
                else 
                {
                    NoViableAltException nvae_d16s3 =
                        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 3, input);
                
                    throw nvae_d16s3;
                }
                }
                break;
            case ID:
            	{
                int LA16_4 = input.LA(2);
                
                if ( (LA16_4 == 34) )
                {
                    switch ( input.LA(3) ) 
                    {
                    case 34:
                    	{
                        switch ( input.LA(4) ) 
                        {
                        case INTEGER:
                        case FLOAT:
                        case STRING:
                        case 28:
                        case 32:
                        case 34:
                        case 38:
                        case 42:
                        case 57:
                        case 58:
                        	{
                            alt16 = 4;
                            }
                            break;
                        case ID:
                        	{
                            switch ( input.LA(5) ) 
                            {
                            case 15:
                            case 17:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                            case 33:
                            case 34:
                            	{
                                alt16 = 4;
                                }
                                break;
                            case 36:
                            	{
                                alt16 = 5;
                                }
                                break;
                            case 35:
                            	{
                                int LA16_14 = input.LA(6);
                                
                                if ( (LA16_14 == 37) )
                                {
                                    alt16 = 5;
                                }
                                else if ( (LA16_14 == 15 || LA16_14 == 17 || (LA16_14 >= 19 && LA16_14 <= 31) || LA16_14 == 33 || (LA16_14 >= 35 && LA16_14 <= 36)) )
                                {
                                    alt16 = 4;
                                }
                                else 
                                {
                                    NoViableAltException nvae_d16s14 =
                                        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 14, input);
                                
                                    throw nvae_d16s14;
                                }
                                }
                                break;
                            	default:
                            	    NoViableAltException nvae_d16s12 =
                            	        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 12, input);
                            
                            	    throw nvae_d16s12;
                            }
                        
                            }
                            break;
                        case 35:
                        	{
                            alt16 = 5;
                            }
                            break;
                        	default:
                        	    NoViableAltException nvae_d16s9 =
                        	        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 9, input);
                        
                        	    throw nvae_d16s9;
                        }
                    
                        }
                        break;
                    case INTEGER:
                    case FLOAT:
                    case STRING:
                    case 28:
                    case 32:
                    case 35:
                    case 38:
                    case 42:
                    case 57:
                    case 58:
                    	{
                        alt16 = 4;
                        }
                        break;
                    case ID:
                    	{
                        switch ( input.LA(4) ) 
                        {
                        case 15:
                        case 17:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 33:
                        case 34:
                        case 35:
                        	{
                            alt16 = 4;
                            }
                            break;
                        case 36:
                        	{
                            int LA16_13 = input.LA(5);
                            
                            if ( (LA16_13 == ID) )
                            {
                                int LA16_15 = input.LA(6);
                                
                                if ( (LA16_15 == 15 || LA16_15 == 17 || (LA16_15 >= 19 && LA16_15 <= 31) || (LA16_15 >= 33 && LA16_15 <= 36)) )
                                {
                                    alt16 = 4;
                                }
                                else if ( (LA16_15 == 37) )
                                {
                                    alt16 = 5;
                                }
                                else 
                                {
                                    NoViableAltException nvae_d16s15 =
                                        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 15, input);
                                
                                    throw nvae_d16s15;
                                }
                            }
                            else if ( ((LA16_13 >= INTEGER && LA16_13 <= STRING) || LA16_13 == 28 || LA16_13 == 32 || LA16_13 == 34 || LA16_13 == 38 || LA16_13 == 42 || (LA16_13 >= 57 && LA16_13 <= 58)) )
                            {
                                alt16 = 4;
                            }
                            else 
                            {
                                NoViableAltException nvae_d16s13 =
                                    new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 13, input);
                            
                                throw nvae_d16s13;
                            }
                            }
                            break;
                        case 37:
                        	{
                            alt16 = 5;
                            }
                            break;
                        	default:
                        	    NoViableAltException nvae_d16s10 =
                        	        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 10, input);
                        
                        	    throw nvae_d16s10;
                        }
                    
                        }
                        break;
                    case 37:
                    	{
                        alt16 = 5;
                        }
                        break;
                    	default:
                    	    NoViableAltException nvae_d16s7 =
                    	        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 7, input);
                    
                    	    throw nvae_d16s7;
                    }
                
                }
                else if ( (LA16_4 == EOF || (LA16_4 >= 15 && LA16_4 <= 31) || LA16_4 == 33 || (LA16_4 >= 35 && LA16_4 <= 36) || (LA16_4 >= 41 && LA16_4 <= 42) || (LA16_4 >= 44 && LA16_4 <= 54)) )
                {
                    alt16 = 4;
                }
                else 
                {
                    NoViableAltException nvae_d16s4 =
                        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 4, input);
                
                    throw nvae_d16s4;
                }
                }
                break;
            case 42:
            	{
                alt16 = 6;
                }
                break;
            	default:
            	    NoViableAltException nvae_d16s0 =
            	        new NoViableAltException("156:1: primaryExpression returns [Expression value] : ( '(' expression ')' | expr= value | newExpression | identifier ( arguments )? | methodCall | queryExpression );", 16, 0, input);
            
            	    throw nvae_d16s0;
            }
            
            switch (alt16) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:157:4: '(' expression ')'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	char_literal28 = (IToken)input.LT(1);
                    	Match(input,34,FOLLOW_34_in_primaryExpression632); 
                    	char_literal28_tree = (CommonTree)adaptor.Create(char_literal28);
                    	adaptor.AddChild(root_0, char_literal28_tree);

                    	PushFollow(FOLLOW_expression_in_primaryExpression634);
                    	expression29 = expression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, expression29.Tree);
                    	char_literal30 = (IToken)input.LT(1);
                    	Match(input,35,FOLLOW_35_in_primaryExpression636); 
                    	char_literal30_tree = (CommonTree)adaptor.Create(char_literal30);
                    	adaptor.AddChild(root_0, char_literal30_tree);

                    	 retval.value =  expression29.value; 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:158:4: expr= value
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_value_in_primaryExpression646);
                    	expr = value();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, expr.Tree);
                    	 retval.value =  expr.value; 
                    
                    }
                    break;
                case 3 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:159:4: newExpression
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_newExpression_in_primaryExpression654);
                    	newExpression31 = newExpression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, newExpression31.Tree);
                    	 retval.value =  newExpression31.value; 
                    
                    }
                    break;
                case 4 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:160:4: identifier ( arguments )?
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_identifier_in_primaryExpression662);
                    	identifier32 = identifier();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, identifier32.Tree);
                    	retval.value =  input.ToString(identifier32.start,identifier32.stop) == "null" ? new ValueExpression(null, TypeCode.Object) : (Expression)identifier32.value; 
                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:160:120: ( arguments )?
                    	int alt15 = 2;
                    	int LA15_0 = input.LA(1);
                    	
                    	if ( (LA15_0 == 34) )
                    	{
                    	    alt15 = 1;
                    	}
                    	switch (alt15) 
                    	{
                    	    case 1 :
                    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:160:121: arguments
                    	        {
                    	        	PushFollow(FOLLOW_arguments_in_primaryExpression667);
                    	        	arguments33 = arguments();
                    	        	followingStackPointer_--;
                    	        	
                    	        	adaptor.AddChild(root_0, arguments33.Tree);
                    	        	retval.value =  new MethodCall(identifier32.value, (arguments33.value).ToArray()); 
                    	        
                    	        }
                    	        break;
                    	
                    	}

                    
                    }
                    break;
                case 5 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:161:4: methodCall
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_methodCall_in_primaryExpression676);
                    	methodCall34 = methodCall();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, methodCall34.Tree);
                    	 retval.value =  methodCall34.value; 
                    
                    }
                    break;
                case 6 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:162:5: queryExpression
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_queryExpression_in_primaryExpression685);
                    	queryExpression35 = queryExpression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, queryExpression35.Tree);
                    	 retval.value =  queryExpression35.value; 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end primaryExpression

    public class value_return : ParserRuleReturnScope 
    {
        public ValueExpression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start value
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:165:1: value returns [ValueExpression value] : ( INTEGER | FLOAT | STRING | datetime | boolean );
    public value_return value() // throws RecognitionException [1]
    {   
        value_return retval = new value_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken INTEGER36 = null;
        IToken FLOAT37 = null;
        IToken STRING38 = null;
        datetime_return datetime39 = null;

        boolean_return boolean40 = null;
        
        
        CommonTree INTEGER36_tree=null;
        CommonTree FLOAT37_tree=null;
        CommonTree STRING38_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:166:2: ( INTEGER | FLOAT | STRING | datetime | boolean )
            int alt17 = 5;
            switch ( input.LA(1) ) 
            {
            case INTEGER:
            	{
                alt17 = 1;
                }
                break;
            case FLOAT:
            	{
                alt17 = 2;
                }
                break;
            case STRING:
            	{
                alt17 = 3;
                }
                break;
            case 38:
            	{
                alt17 = 4;
                }
                break;
            case 57:
            case 58:
            	{
                alt17 = 5;
                }
                break;
            	default:
            	    NoViableAltException nvae_d17s0 =
            	        new NoViableAltException("165:1: value returns [ValueExpression value] : ( INTEGER | FLOAT | STRING | datetime | boolean );", 17, 0, input);
            
            	    throw nvae_d17s0;
            }
            
            switch (alt17) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:166:5: INTEGER
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	INTEGER36 = (IToken)input.LT(1);
                    	Match(input,INTEGER,FOLLOW_INTEGER_in_value704); 
                    	INTEGER36_tree = (CommonTree)adaptor.Create(INTEGER36);
                    	adaptor.AddChild(root_0, INTEGER36_tree);

                    	 retval.value =  new ValueExpression(int.Parse(INTEGER36.Text), TypeCode.Int32); 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:167:4: FLOAT
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	FLOAT37 = (IToken)input.LT(1);
                    	Match(input,FLOAT,FOLLOW_FLOAT_in_value712); 
                    	FLOAT37_tree = (CommonTree)adaptor.Create(FLOAT37);
                    	adaptor.AddChild(root_0, FLOAT37_tree);

                    	 retval.value =  new ValueExpression(float.Parse(FLOAT37.Text, numberFormatInfo), TypeCode.Single); 
                    
                    }
                    break;
                case 3 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:168:4: STRING
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	STRING38 = (IToken)input.LT(1);
                    	Match(input,STRING,FOLLOW_STRING_in_value720); 
                    	STRING38_tree = (CommonTree)adaptor.Create(STRING38);
                    	adaptor.AddChild(root_0, STRING38_tree);

                    	 retval.value =  new ValueExpression(extractString(STRING38.Text), TypeCode.String); 
                    
                    }
                    break;
                case 4 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:169:5: datetime
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_datetime_in_value729);
                    	datetime39 = datetime();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, datetime39.Tree);
                    	 retval.value =  new ValueExpression(datetime39.value, TypeCode.DateTime); 
                    
                    }
                    break;
                case 5 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:170:4: boolean
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_boolean_in_value736);
                    	boolean40 = boolean();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, boolean40.Tree);
                    	 retval.value =  new ValueExpression(boolean40.value, TypeCode.Boolean); 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end value

    public class methodCall_return : ParserRuleReturnScope 
    {
        public MethodCall value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start methodCall
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:173:1: methodCall returns [MethodCall value] : (n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')' | n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')' );
    public methodCall_return methodCall() // throws RecognitionException [1]
    {   
        methodCall_return retval = new methodCall_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal41 = null;
        IToken char_literal42 = null;
        IToken string_literal43 = null;
        IToken char_literal44 = null;
        IToken char_literal45 = null;
        IToken char_literal46 = null;
        IToken char_literal47 = null;
        IToken char_literal48 = null;
        IToken string_literal49 = null;
        IToken char_literal50 = null;
        identifier_return n2 = null;

        identifier_return anon1 = null;

        identifier_return indexer1 = null;

        expression_return lambda1 = null;

        identifier_return n3 = null;

        identifier_return anon2 = null;

        identifier_return indexer2 = null;

        expression_return lambda2 = null;
        
        
        CommonTree char_literal41_tree=null;
        CommonTree char_literal42_tree=null;
        CommonTree string_literal43_tree=null;
        CommonTree char_literal44_tree=null;
        CommonTree char_literal45_tree=null;
        CommonTree char_literal46_tree=null;
        CommonTree char_literal47_tree=null;
        CommonTree char_literal48_tree=null;
        CommonTree string_literal49_tree=null;
        CommonTree char_literal50_tree=null;
    
        
        List<Expression> parameters = new List<Expression>();
        Identifier name = null;
        Identifier anonIdentifier = null;
        Identifier indexIdentifier = null;
        Expression lambdaExpression = null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:181:2: (n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')' | n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')' )
            int alt22 = 2;
            int LA22_0 = input.LA(1);
            
            if ( (LA22_0 == ID) )
            {
                int LA22_1 = input.LA(2);
                
                if ( (LA22_1 == 34) )
                {
                    int LA22_2 = input.LA(3);
                    
                    if ( (LA22_2 == 34) )
                    {
                        alt22 = 2;
                    }
                    else if ( (LA22_2 == ID || LA22_2 == 37) )
                    {
                        alt22 = 1;
                    }
                    else 
                    {
                        NoViableAltException nvae_d22s2 =
                            new NoViableAltException("173:1: methodCall returns [MethodCall value] : (n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')' | n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')' );", 22, 2, input);
                    
                        throw nvae_d22s2;
                    }
                }
                else 
                {
                    NoViableAltException nvae_d22s1 =
                        new NoViableAltException("173:1: methodCall returns [MethodCall value] : (n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')' | n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')' );", 22, 1, input);
                
                    throw nvae_d22s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d22s0 =
                    new NoViableAltException("173:1: methodCall returns [MethodCall value] : (n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')' | n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')' );", 22, 0, input);
            
                throw nvae_d22s0;
            }
            switch (alt22) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:182:2: n2= identifier '(' (anon1= identifier ( ',' indexer1= identifier )? )? '=>' lambda1= expression ')'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_identifier_in_methodCall763);
                    	n2 = identifier();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, n2.Tree);
                    	 name = n2.value; 
                    	char_literal41 = (IToken)input.LT(1);
                    	Match(input,34,FOLLOW_34_in_methodCall767); 
                    	char_literal41_tree = (CommonTree)adaptor.Create(char_literal41);
                    	adaptor.AddChild(root_0, char_literal41_tree);

                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:182:43: (anon1= identifier ( ',' indexer1= identifier )? )?
                    	int alt19 = 2;
                    	int LA19_0 = input.LA(1);
                    	
                    	if ( (LA19_0 == ID) )
                    	{
                    	    alt19 = 1;
                    	}
                    	switch (alt19) 
                    	{
                    	    case 1 :
                    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:182:44: anon1= identifier ( ',' indexer1= identifier )?
                    	        {
                    	        	PushFollow(FOLLOW_identifier_in_methodCall773);
                    	        	anon1 = identifier();
                    	        	followingStackPointer_--;
                    	        	
                    	        	adaptor.AddChild(root_0, anon1.Tree);
                    	        	 anonIdentifier = anon1.value; 
                    	        	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:182:96: ( ',' indexer1= identifier )?
                    	        	int alt18 = 2;
                    	        	int LA18_0 = input.LA(1);
                    	        	
                    	        	if ( (LA18_0 == 36) )
                    	        	{
                    	        	    alt18 = 1;
                    	        	}
                    	        	switch (alt18) 
                    	        	{
                    	        	    case 1 :
                    	        	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:182:97: ',' indexer1= identifier
                    	        	        {
                    	        	        	char_literal42 = (IToken)input.LT(1);
                    	        	        	Match(input,36,FOLLOW_36_in_methodCall778); 
                    	        	        	char_literal42_tree = (CommonTree)adaptor.Create(char_literal42);
                    	        	        	adaptor.AddChild(root_0, char_literal42_tree);

                    	        	        	PushFollow(FOLLOW_identifier_in_methodCall782);
                    	        	        	indexer1 = identifier();
                    	        	        	followingStackPointer_--;
                    	        	        	
                    	        	        	adaptor.AddChild(root_0, indexer1.Tree);
                    	        	        	 indexIdentifier = indexer1.value; 
                    	        	        
                    	        	        }
                    	        	        break;
                    	        	
                    	        	}

                    	        
                    	        }
                    	        break;
                    	
                    	}

                    	string_literal43 = (IToken)input.LT(1);
                    	Match(input,37,FOLLOW_37_in_methodCall793); 
                    	string_literal43_tree = (CommonTree)adaptor.Create(string_literal43);
                    	adaptor.AddChild(root_0, string_literal43_tree);

                    	PushFollow(FOLLOW_expression_in_methodCall797);
                    	lambda1 = expression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, lambda1.Tree);
                    	 lambdaExpression = lambda1.value; 
                    	char_literal44 = (IToken)input.LT(1);
                    	Match(input,35,FOLLOW_35_in_methodCall801); 
                    	char_literal44_tree = (CommonTree)adaptor.Create(char_literal44);
                    	adaptor.AddChild(root_0, char_literal44_tree);

                    	 retval.value =  new MethodCall(name, new Expression[0], anonIdentifier, indexIdentifier, lambdaExpression ); 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:184:4: n3= identifier '(' '(' (anon2= identifier ( ',' indexer2= identifier )? )? ')' '=>' lambda2= expression ')'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_identifier_in_methodCall811);
                    	n3 = identifier();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, n3.Tree);
                    	 name = n3.value; 
                    	char_literal45 = (IToken)input.LT(1);
                    	Match(input,34,FOLLOW_34_in_methodCall815); 
                    	char_literal45_tree = (CommonTree)adaptor.Create(char_literal45);
                    	adaptor.AddChild(root_0, char_literal45_tree);

                    	char_literal46 = (IToken)input.LT(1);
                    	Match(input,34,FOLLOW_34_in_methodCall817); 
                    	char_literal46_tree = (CommonTree)adaptor.Create(char_literal46);
                    	adaptor.AddChild(root_0, char_literal46_tree);

                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:184:48: (anon2= identifier ( ',' indexer2= identifier )? )?
                    	int alt21 = 2;
                    	int LA21_0 = input.LA(1);
                    	
                    	if ( (LA21_0 == ID) )
                    	{
                    	    alt21 = 1;
                    	}
                    	switch (alt21) 
                    	{
                    	    case 1 :
                    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:184:49: anon2= identifier ( ',' indexer2= identifier )?
                    	        {
                    	        	PushFollow(FOLLOW_identifier_in_methodCall822);
                    	        	anon2 = identifier();
                    	        	followingStackPointer_--;
                    	        	
                    	        	adaptor.AddChild(root_0, anon2.Tree);
                    	        	 anonIdentifier = anon2.value; 
                    	        	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:184:101: ( ',' indexer2= identifier )?
                    	        	int alt20 = 2;
                    	        	int LA20_0 = input.LA(1);
                    	        	
                    	        	if ( (LA20_0 == 36) )
                    	        	{
                    	        	    alt20 = 1;
                    	        	}
                    	        	switch (alt20) 
                    	        	{
                    	        	    case 1 :
                    	        	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:184:102: ',' indexer2= identifier
                    	        	        {
                    	        	        	char_literal47 = (IToken)input.LT(1);
                    	        	        	Match(input,36,FOLLOW_36_in_methodCall827); 
                    	        	        	char_literal47_tree = (CommonTree)adaptor.Create(char_literal47);
                    	        	        	adaptor.AddChild(root_0, char_literal47_tree);

                    	        	        	PushFollow(FOLLOW_identifier_in_methodCall831);
                    	        	        	indexer2 = identifier();
                    	        	        	followingStackPointer_--;
                    	        	        	
                    	        	        	adaptor.AddChild(root_0, indexer2.Tree);
                    	        	        	 indexIdentifier = indexer2.value; 
                    	        	        
                    	        	        }
                    	        	        break;
                    	        	
                    	        	}

                    	        
                    	        }
                    	        break;
                    	
                    	}

                    	char_literal48 = (IToken)input.LT(1);
                    	Match(input,35,FOLLOW_35_in_methodCall841); 
                    	char_literal48_tree = (CommonTree)adaptor.Create(char_literal48);
                    	adaptor.AddChild(root_0, char_literal48_tree);

                    	string_literal49 = (IToken)input.LT(1);
                    	Match(input,37,FOLLOW_37_in_methodCall843); 
                    	string_literal49_tree = (CommonTree)adaptor.Create(string_literal49);
                    	adaptor.AddChild(root_0, string_literal49_tree);

                    	PushFollow(FOLLOW_expression_in_methodCall847);
                    	lambda2 = expression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, lambda2.Tree);
                    	 lambdaExpression = lambda2.value; 
                    	char_literal50 = (IToken)input.LT(1);
                    	Match(input,35,FOLLOW_35_in_methodCall851); 
                    	char_literal50_tree = (CommonTree)adaptor.Create(char_literal50);
                    	adaptor.AddChild(root_0, char_literal50_tree);

                    	 retval.value =  new MethodCall(name, new Expression[0], anonIdentifier, indexIdentifier, lambdaExpression); 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end methodCall

    public class identifier_return : ParserRuleReturnScope 
    {
        public Identifier value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start identifier
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:188:1: identifier returns [Identifier value] : ID ;
    public identifier_return identifier() // throws RecognitionException [1]
    {   
        identifier_return retval = new identifier_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken ID51 = null;
        
        CommonTree ID51_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:189:2: ( ID )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:189:5: ID
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	ID51 = (IToken)input.LT(1);
            	Match(input,ID,FOLLOW_ID_in_identifier869); 
            	ID51_tree = (CommonTree)adaptor.Create(ID51);
            	adaptor.AddChild(root_0, ID51_tree);

            	 retval.value =  new Identifier(ID51.Text); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end identifier

    public class newExpression_return : ParserRuleReturnScope 
    {
        public Expression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start newExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:192:1: newExpression returns [Expression value] : ( 'new' ( type )? '{' (firstid= identifier '=' )? firstexp= expression ( ',' (followid= identifier '=' )? followexp= expression )* '}' | 'new' type arguments );
    public newExpression_return newExpression() // throws RecognitionException [1]
    {   
        newExpression_return retval = new newExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal52 = null;
        IToken char_literal54 = null;
        IToken char_literal55 = null;
        IToken char_literal56 = null;
        IToken char_literal57 = null;
        IToken char_literal58 = null;
        IToken string_literal59 = null;
        identifier_return firstid = null;

        expression_return firstexp = null;

        identifier_return followid = null;

        expression_return followexp = null;

        type_return type53 = null;

        type_return type60 = null;

        arguments_return arguments61 = null;
        
        
        CommonTree string_literal52_tree=null;
        CommonTree char_literal54_tree=null;
        CommonTree char_literal55_tree=null;
        CommonTree char_literal56_tree=null;
        CommonTree char_literal57_tree=null;
        CommonTree char_literal58_tree=null;
        CommonTree string_literal59_tree=null;
    
        
        string typ = null;
        Identifier id = null;
        List<AnonymousParameter> parameters = new List<AnonymousParameter>();
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:2: ( 'new' ( type )? '{' (firstid= identifier '=' )? firstexp= expression ( ',' (followid= identifier '=' )? followexp= expression )* '}' | 'new' type arguments )
            int alt27 = 2;
            alt27 = dfa27.Predict(input);
            switch (alt27) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:4: 'new' ( type )? '{' (firstid= identifier '=' )? firstexp= expression ( ',' (followid= identifier '=' )? followexp= expression )* '}'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	string_literal52 = (IToken)input.LT(1);
                    	Match(input,38,FOLLOW_38_in_newExpression890); 
                    	string_literal52_tree = (CommonTree)adaptor.Create(string_literal52);
                    	adaptor.AddChild(root_0, string_literal52_tree);

                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:10: ( type )?
                    	int alt23 = 2;
                    	int LA23_0 = input.LA(1);
                    	
                    	if ( (LA23_0 == ID) )
                    	{
                    	    alt23 = 1;
                    	}
                    	switch (alt23) 
                    	{
                    	    case 1 :
                    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:11: type
                    	        {
                    	        	PushFollow(FOLLOW_type_in_newExpression893);
                    	        	type53 = type();
                    	        	followingStackPointer_--;
                    	        	
                    	        	adaptor.AddChild(root_0, type53.Tree);
                    	        	 typ = input.ToString(type53.start,type53.stop); id = null; 
                    	        
                    	        }
                    	        break;
                    	
                    	}

                    	char_literal54 = (IToken)input.LT(1);
                    	Match(input,39,FOLLOW_39_in_newExpression900); 
                    	char_literal54_tree = (CommonTree)adaptor.Create(char_literal54);
                    	adaptor.AddChild(root_0, char_literal54_tree);

                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:56: (firstid= identifier '=' )?
                    	int alt24 = 2;
                    	int LA24_0 = input.LA(1);
                    	
                    	if ( (LA24_0 == ID) )
                    	{
                    	    int LA24_1 = input.LA(2);
                    	    
                    	    if ( (LA24_1 == 40) )
                    	    {
                    	        alt24 = 1;
                    	    }
                    	}
                    	switch (alt24) 
                    	{
                    	    case 1 :
                    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:57: firstid= identifier '='
                    	        {
                    	        	PushFollow(FOLLOW_identifier_in_newExpression905);
                    	        	firstid = identifier();
                    	        	followingStackPointer_--;
                    	        	
                    	        	adaptor.AddChild(root_0, firstid.Tree);
                    	        	char_literal55 = (IToken)input.LT(1);
                    	        	Match(input,40,FOLLOW_40_in_newExpression907); 
                    	        	char_literal55_tree = (CommonTree)adaptor.Create(char_literal55);
                    	        	adaptor.AddChild(root_0, char_literal55_tree);

                    	        	 id = firstid.value; 
                    	        
                    	        }
                    	        break;
                    	
                    	}

                    	PushFollow(FOLLOW_expression_in_newExpression916);
                    	firstexp = expression();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, firstexp.Tree);
                    	 parameters.Add(new AnonymousParameter(id, firstexp.value)); 
                    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:193: ( ',' (followid= identifier '=' )? followexp= expression )*
                    	do 
                    	{
                    	    int alt26 = 2;
                    	    int LA26_0 = input.LA(1);
                    	    
                    	    if ( (LA26_0 == 36) )
                    	    {
                    	        alt26 = 1;
                    	    }
                    	    
                    	
                    	    switch (alt26) 
                    		{
                    			case 1 :
                    			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:194: ',' (followid= identifier '=' )? followexp= expression
                    			    {
                    			    	char_literal56 = (IToken)input.LT(1);
                    			    	Match(input,36,FOLLOW_36_in_newExpression921); 
                    			    	char_literal56_tree = (CommonTree)adaptor.Create(char_literal56);
                    			    	adaptor.AddChild(root_0, char_literal56_tree);

                    			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:198: (followid= identifier '=' )?
                    			    	int alt25 = 2;
                    			    	int LA25_0 = input.LA(1);
                    			    	
                    			    	if ( (LA25_0 == ID) )
                    			    	{
                    			    	    int LA25_1 = input.LA(2);
                    			    	    
                    			    	    if ( (LA25_1 == 40) )
                    			    	    {
                    			    	        alt25 = 1;
                    			    	    }
                    			    	}
                    			    	switch (alt25) 
                    			    	{
                    			    	    case 1 :
                    			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:198:199: followid= identifier '='
                    			    	        {
                    			    	        	PushFollow(FOLLOW_identifier_in_newExpression926);
                    			    	        	followid = identifier();
                    			    	        	followingStackPointer_--;
                    			    	        	
                    			    	        	adaptor.AddChild(root_0, followid.Tree);
                    			    	        	char_literal57 = (IToken)input.LT(1);
                    			    	        	Match(input,40,FOLLOW_40_in_newExpression928); 
                    			    	        	char_literal57_tree = (CommonTree)adaptor.Create(char_literal57);
                    			    	        	adaptor.AddChild(root_0, char_literal57_tree);

                    			    	        	 id = followid.value; 
                    			    	        
                    			    	        }
                    			    	        break;
                    			    	
                    			    	}

                    			    	PushFollow(FOLLOW_expression_in_newExpression936);
                    			    	followexp = expression();
                    			    	followingStackPointer_--;
                    			    	
                    			    	adaptor.AddChild(root_0, followexp.Tree);
                    			    	 parameters.Add(new AnonymousParameter(id, followexp.value)); 
                    			    
                    			    }
                    			    break;
                    	
                    			default:
                    			    goto loop26;
                    	    }
                    	} while (true);
                    	
                    	loop26:
                    		;	// Stops C# compiler whinging that label 'loop26' has no statements

                    	char_literal58 = (IToken)input.LT(1);
                    	Match(input,41,FOLLOW_41_in_newExpression942); 
                    	char_literal58_tree = (CommonTree)adaptor.Create(char_literal58);
                    	adaptor.AddChild(root_0, char_literal58_tree);

                    	retval.value =  new AnonymousNew(typ, parameters.ToArray()); 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:200:5: 'new' type arguments
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	string_literal59 = (IToken)input.LT(1);
                    	Match(input,38,FOLLOW_38_in_newExpression953); 
                    	string_literal59_tree = (CommonTree)adaptor.Create(string_literal59);
                    	adaptor.AddChild(root_0, string_literal59_tree);

                    	PushFollow(FOLLOW_type_in_newExpression955);
                    	type60 = type();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, type60.Tree);
                    	PushFollow(FOLLOW_arguments_in_newExpression957);
                    	arguments61 = arguments();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, arguments61.Tree);
                    	retval.value =  new TypedNew(input.ToString(type60.start,type60.stop), (arguments61.value).ToArray()); 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end newExpression

    public class queryExpression_return : ParserRuleReturnScope 
    {
        public QueryExpression value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start queryExpression
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:204:1: queryExpression returns [QueryExpression value] : fromClause queryBody ;
    public queryExpression_return queryExpression() // throws RecognitionException [1]
    {   
        queryExpression_return retval = new queryExpression_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        fromClause_return fromClause62 = null;

        queryBody_return queryBody63 = null;
        
        
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:205:2: ( fromClause queryBody )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:205:5: fromClause queryBody
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_fromClause_in_queryExpression984);
            	fromClause62 = fromClause();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, fromClause62.Tree);
            	PushFollow(FOLLOW_queryBody_in_queryExpression986);
            	queryBody63 = queryBody();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, queryBody63.Tree);
            	 retval.value =  new QueryExpression(fromClause62.value, queryBody63.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end queryExpression

    public class fromClause_return : ParserRuleReturnScope 
    {
        public FromClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start fromClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:208:1: fromClause returns [FromClause value] : 'from' ( type )? identifier 'in' expression ;
    public fromClause_return fromClause() // throws RecognitionException [1]
    {   
        fromClause_return retval = new fromClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal64 = null;
        IToken string_literal67 = null;
        type_return type65 = null;

        identifier_return identifier66 = null;

        expression_return expression68 = null;
        
        
        CommonTree string_literal64_tree=null;
        CommonTree string_literal67_tree=null;
    
        
        string t = null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:212:2: ( 'from' ( type )? identifier 'in' expression )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:212:4: 'from' ( type )? identifier 'in' expression
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal64 = (IToken)input.LT(1);
            	Match(input,42,FOLLOW_42_in_fromClause1009); 
            	string_literal64_tree = (CommonTree)adaptor.Create(string_literal64);
            	adaptor.AddChild(root_0, string_literal64_tree);

            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:212:11: ( type )?
            	int alt28 = 2;
            	int LA28_0 = input.LA(1);
            	
            	if ( (LA28_0 == ID) )
            	{
            	    int LA28_1 = input.LA(2);
            	    
            	    if ( (LA28_1 == ID || LA28_1 == 33) )
            	    {
            	        alt28 = 1;
            	    }
            	}
            	switch (alt28) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:212:12: type
            	        {
            	        	PushFollow(FOLLOW_type_in_fromClause1012);
            	        	type65 = type();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, type65.Tree);
            	        	 t = input.ToString(type65.start,type65.stop); 
            	        
            	        }
            	        break;
            	
            	}

            	PushFollow(FOLLOW_identifier_in_fromClause1019);
            	identifier66 = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, identifier66.Tree);
            	string_literal67 = (IToken)input.LT(1);
            	Match(input,43,FOLLOW_43_in_fromClause1021); 
            	string_literal67_tree = (CommonTree)adaptor.Create(string_literal67);
            	adaptor.AddChild(root_0, string_literal67_tree);

            	PushFollow(FOLLOW_expression_in_fromClause1023);
            	expression68 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression68.Tree);
            	 retval.value =  new FromClause(t, identifier66.value, expression68.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end fromClause

    public class queryBody_return : ParserRuleReturnScope 
    {
        public QueryBody value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start queryBody
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:215:1: queryBody returns [QueryBody value] : ( queryBodyClause )* ( selectClause | groupClause ) ( queryContinuation )? ;
    public queryBody_return queryBody() // throws RecognitionException [1]
    {   
        queryBody_return retval = new queryBody_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        queryBodyClause_return queryBodyClause69 = null;

        selectClause_return selectClause70 = null;

        groupClause_return groupClause71 = null;

        queryContinuation_return queryContinuation72 = null;
        
        
    
        
        List<QueryBodyClause> clauses = new List<QueryBodyClause>();
        SelectOrGroupClause selOrGroup = null;
        QueryContinuation qc = null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:2: ( ( queryBodyClause )* ( selectClause | groupClause ) ( queryContinuation )? )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:4: ( queryBodyClause )* ( selectClause | groupClause ) ( queryContinuation )?
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:4: ( queryBodyClause )*
            	do 
            	{
            	    int alt29 = 2;
            	    int LA29_0 = input.LA(1);
            	    
            	    if ( (LA29_0 == 42 || (LA29_0 >= 45 && LA29_0 <= 47) || LA29_0 == 50) )
            	    {
            	        alt29 = 1;
            	    }
            	    
            	
            	    switch (alt29) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:5: queryBodyClause
            			    {
            			    	PushFollow(FOLLOW_queryBodyClause_in_queryBody1048);
            			    	queryBodyClause69 = queryBodyClause();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, queryBodyClause69.Tree);
            			    	 clauses.Add(queryBodyClause69.value); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop29;
            	    }
            	} while (true);
            	
            	loop29:
            		;	// Stops C# compiler whinging that label 'loop29' has no statements

            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:65: ( selectClause | groupClause )
            	int alt30 = 2;
            	int LA30_0 = input.LA(1);
            	
            	if ( (LA30_0 == 53) )
            	{
            	    alt30 = 1;
            	}
            	else if ( (LA30_0 == 54) )
            	{
            	    alt30 = 2;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d30s0 =
            	        new NoViableAltException("221:65: ( selectClause | groupClause )", 30, 0, input);
            	
            	    throw nvae_d30s0;
            	}
            	switch (alt30) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:66: selectClause
            	        {
            	        	PushFollow(FOLLOW_selectClause_in_queryBody1056);
            	        	selectClause70 = selectClause();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, selectClause70.Tree);
            	        	 selOrGroup = selectClause70.value; 
            	        
            	        }
            	        break;
            	    case 2 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:119: groupClause
            	        {
            	        	PushFollow(FOLLOW_groupClause_in_queryBody1062);
            	        	groupClause71 = groupClause();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, groupClause71.Tree);
            	        	 selOrGroup = groupClause71.value; 
            	        
            	        }
            	        break;
            	
            	}

            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:169: ( queryContinuation )?
            	int alt31 = 2;
            	int LA31_0 = input.LA(1);
            	
            	if ( (LA31_0 == 44) )
            	{
            	    alt31 = 1;
            	}
            	switch (alt31) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:221:170: queryContinuation
            	        {
            	        	PushFollow(FOLLOW_queryContinuation_in_queryBody1068);
            	        	queryContinuation72 = queryContinuation();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, queryContinuation72.Tree);
            	        	 qc = queryContinuation72.value; 
            	        
            	        }
            	        break;
            	
            	}

            	 retval.value =  new QueryBody(clauses.ToArray(), selOrGroup, qc); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end queryBody

    public class queryBodyClause_return : ParserRuleReturnScope 
    {
        public QueryBodyClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start queryBodyClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:225:1: queryBodyClause returns [QueryBodyClause value] : ( fromClause | letClause | whereClause | joinClause | orderByClause );
    public queryBodyClause_return queryBodyClause() // throws RecognitionException [1]
    {   
        queryBodyClause_return retval = new queryBodyClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        fromClause_return fromClause73 = null;

        letClause_return letClause74 = null;

        whereClause_return whereClause75 = null;

        joinClause_return joinClause76 = null;

        orderByClause_return orderByClause77 = null;
        
        
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:226:2: ( fromClause | letClause | whereClause | joinClause | orderByClause )
            int alt32 = 5;
            switch ( input.LA(1) ) 
            {
            case 42:
            	{
                alt32 = 1;
                }
                break;
            case 46:
            	{
                alt32 = 2;
                }
                break;
            case 45:
            	{
                alt32 = 3;
                }
                break;
            case 47:
            	{
                alt32 = 4;
                }
                break;
            case 50:
            	{
                alt32 = 5;
                }
                break;
            	default:
            	    NoViableAltException nvae_d32s0 =
            	        new NoViableAltException("225:1: queryBodyClause returns [QueryBodyClause value] : ( fromClause | letClause | whereClause | joinClause | orderByClause );", 32, 0, input);
            
            	    throw nvae_d32s0;
            }
            
            switch (alt32) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:226:4: fromClause
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_fromClause_in_queryBodyClause1092);
                    	fromClause73 = fromClause();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, fromClause73.Tree);
                    	 retval.value =  fromClause73.value; 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:227:5: letClause
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_letClause_in_queryBodyClause1101);
                    	letClause74 = letClause();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, letClause74.Tree);
                    	 retval.value =  letClause74.value; 
                    
                    }
                    break;
                case 3 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:228:5: whereClause
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_whereClause_in_queryBodyClause1110);
                    	whereClause75 = whereClause();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, whereClause75.Tree);
                    	 retval.value =  whereClause75.value; 
                    
                    }
                    break;
                case 4 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:229:5: joinClause
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_joinClause_in_queryBodyClause1119);
                    	joinClause76 = joinClause();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, joinClause76.Tree);
                    	 retval.value =  joinClause76.value; 
                    
                    }
                    break;
                case 5 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:230:5: orderByClause
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	PushFollow(FOLLOW_orderByClause_in_queryBodyClause1128);
                    	orderByClause77 = orderByClause();
                    	followingStackPointer_--;
                    	
                    	adaptor.AddChild(root_0, orderByClause77.Tree);
                    	 retval.value =  orderByClause77.value; 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end queryBodyClause

    public class queryContinuation_return : ParserRuleReturnScope 
    {
        public QueryContinuation value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start queryContinuation
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:233:1: queryContinuation returns [QueryContinuation value] : 'into' identifier queryBody ;
    public queryContinuation_return queryContinuation() // throws RecognitionException [1]
    {   
        queryContinuation_return retval = new queryContinuation_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal78 = null;
        identifier_return identifier79 = null;

        queryBody_return queryBody80 = null;
        
        
        CommonTree string_literal78_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:234:2: ( 'into' identifier queryBody )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:234:5: 'into' identifier queryBody
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal78 = (IToken)input.LT(1);
            	Match(input,44,FOLLOW_44_in_queryContinuation1145); 
            	string_literal78_tree = (CommonTree)adaptor.Create(string_literal78);
            	adaptor.AddChild(root_0, string_literal78_tree);

            	PushFollow(FOLLOW_identifier_in_queryContinuation1147);
            	identifier79 = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, identifier79.Tree);
            	PushFollow(FOLLOW_queryBody_in_queryContinuation1149);
            	queryBody80 = queryBody();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, queryBody80.Tree);
            	 retval.value =  new QueryContinuation(identifier79.value, queryBody80.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end queryContinuation

    public class whereClause_return : ParserRuleReturnScope 
    {
        public WhereClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start whereClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:237:1: whereClause returns [WhereClause value] : 'where' expression ;
    public whereClause_return whereClause() // throws RecognitionException [1]
    {   
        whereClause_return retval = new whereClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal81 = null;
        expression_return expression82 = null;
        
        
        CommonTree string_literal81_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:238:2: ( 'where' expression )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:238:4: 'where' expression
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal81 = (IToken)input.LT(1);
            	Match(input,45,FOLLOW_45_in_whereClause1167); 
            	string_literal81_tree = (CommonTree)adaptor.Create(string_literal81);
            	adaptor.AddChild(root_0, string_literal81_tree);

            	PushFollow(FOLLOW_expression_in_whereClause1169);
            	expression82 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression82.Tree);
            	 retval.value =  new WhereClause(expression82.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end whereClause

    public class letClause_return : ParserRuleReturnScope 
    {
        public LetClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start letClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:240:1: letClause returns [LetClause value] : 'let' identifier '=' expression ;
    public letClause_return letClause() // throws RecognitionException [1]
    {   
        letClause_return retval = new letClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal83 = null;
        IToken char_literal85 = null;
        identifier_return identifier84 = null;

        expression_return expression86 = null;
        
        
        CommonTree string_literal83_tree=null;
        CommonTree char_literal85_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:241:2: ( 'let' identifier '=' expression )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:241:4: 'let' identifier '=' expression
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal83 = (IToken)input.LT(1);
            	Match(input,46,FOLLOW_46_in_letClause1184); 
            	string_literal83_tree = (CommonTree)adaptor.Create(string_literal83);
            	adaptor.AddChild(root_0, string_literal83_tree);

            	PushFollow(FOLLOW_identifier_in_letClause1186);
            	identifier84 = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, identifier84.Tree);
            	char_literal85 = (IToken)input.LT(1);
            	Match(input,40,FOLLOW_40_in_letClause1188); 
            	char_literal85_tree = (CommonTree)adaptor.Create(char_literal85);
            	adaptor.AddChild(root_0, char_literal85_tree);

            	PushFollow(FOLLOW_expression_in_letClause1190);
            	expression86 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression86.Tree);
            	 retval.value =  new LetClause(identifier84.value, expression86.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end letClause

    public class joinClause_return : ParserRuleReturnScope 
    {
        public JoinClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start joinClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:244:1: joinClause returns [JoinClause value] : 'join' ( type )? join= identifier 'in' inid= expression 'on' on= expression 'equals' equals= expression ( 'into' into= identifier )? ;
    public joinClause_return joinClause() // throws RecognitionException [1]
    {   
        joinClause_return retval = new joinClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal87 = null;
        IToken string_literal89 = null;
        IToken string_literal90 = null;
        IToken string_literal91 = null;
        IToken string_literal92 = null;
        identifier_return join = null;

        expression_return inid = null;

        expression_return on = null;

        expression_return equals = null;

        identifier_return into = null;

        type_return type88 = null;
        
        
        CommonTree string_literal87_tree=null;
        CommonTree string_literal89_tree=null;
        CommonTree string_literal90_tree=null;
        CommonTree string_literal91_tree=null;
        CommonTree string_literal92_tree=null;
    
        
        string t = null;
        Identifier i = null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:2: ( 'join' ( type )? join= identifier 'in' inid= expression 'on' on= expression 'equals' equals= expression ( 'into' into= identifier )? )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:4: 'join' ( type )? join= identifier 'in' inid= expression 'on' on= expression 'equals' equals= expression ( 'into' into= identifier )?
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal87 = (IToken)input.LT(1);
            	Match(input,47,FOLLOW_47_in_joinClause1211); 
            	string_literal87_tree = (CommonTree)adaptor.Create(string_literal87);
            	adaptor.AddChild(root_0, string_literal87_tree);

            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:11: ( type )?
            	int alt33 = 2;
            	int LA33_0 = input.LA(1);
            	
            	if ( (LA33_0 == ID) )
            	{
            	    int LA33_1 = input.LA(2);
            	    
            	    if ( (LA33_1 == ID || LA33_1 == 33) )
            	    {
            	        alt33 = 1;
            	    }
            	}
            	switch (alt33) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:12: type
            	        {
            	        	PushFollow(FOLLOW_type_in_joinClause1214);
            	        	type88 = type();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, type88.Tree);
            	        	 t = input.ToString(type88.start,type88.stop); 
            	        
            	        }
            	        break;
            	
            	}

            	PushFollow(FOLLOW_identifier_in_joinClause1223);
            	join = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, join.Tree);
            	string_literal89 = (IToken)input.LT(1);
            	Match(input,43,FOLLOW_43_in_joinClause1225); 
            	string_literal89_tree = (CommonTree)adaptor.Create(string_literal89);
            	adaptor.AddChild(root_0, string_literal89_tree);

            	PushFollow(FOLLOW_expression_in_joinClause1229);
            	inid = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, inid.Tree);
            	string_literal90 = (IToken)input.LT(1);
            	Match(input,48,FOLLOW_48_in_joinClause1231); 
            	string_literal90_tree = (CommonTree)adaptor.Create(string_literal90);
            	adaptor.AddChild(root_0, string_literal90_tree);

            	PushFollow(FOLLOW_expression_in_joinClause1235);
            	on = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, on.Tree);
            	string_literal91 = (IToken)input.LT(1);
            	Match(input,49,FOLLOW_49_in_joinClause1237); 
            	string_literal91_tree = (CommonTree)adaptor.Create(string_literal91);
            	adaptor.AddChild(root_0, string_literal91_tree);

            	PushFollow(FOLLOW_expression_in_joinClause1241);
            	equals = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, equals.Tree);
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:123: ( 'into' into= identifier )?
            	int alt34 = 2;
            	int LA34_0 = input.LA(1);
            	
            	if ( (LA34_0 == 44) )
            	{
            	    alt34 = 1;
            	}
            	switch (alt34) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:249:124: 'into' into= identifier
            	        {
            	        	string_literal92 = (IToken)input.LT(1);
            	        	Match(input,44,FOLLOW_44_in_joinClause1244); 
            	        	string_literal92_tree = (CommonTree)adaptor.Create(string_literal92);
            	        	adaptor.AddChild(root_0, string_literal92_tree);

            	        	PushFollow(FOLLOW_identifier_in_joinClause1248);
            	        	into = identifier();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, into.Tree);
            	        	 i = into.value; 
            	        
            	        }
            	        break;
            	
            	}

            	 retval.value =  new JoinClause(t, join.value, inid.value, on.value, equals.value, i); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end joinClause

    public class orderByClause_return : ParserRuleReturnScope 
    {
        public OrderByClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start orderByClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:253:1: orderByClause returns [OrderByClause value] : 'orderby' fis= expression (fio= ( 'ascending' | 'descending' ) )? ( ',' fos= expression (foo= ( 'ascending' | 'descending' ) )? )* ;
    public orderByClause_return orderByClause() // throws RecognitionException [1]
    {   
        orderByClause_return retval = new orderByClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken fio = null;
        IToken foo = null;
        IToken string_literal93 = null;
        IToken char_literal94 = null;
        expression_return fis = null;

        expression_return fos = null;
        
        
        CommonTree fio_tree=null;
        CommonTree foo_tree=null;
        CommonTree string_literal93_tree=null;
        CommonTree char_literal94_tree=null;
    
        
        List<OrderByCriteria> criterias = new List<OrderByCriteria>();
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:2: ( 'orderby' fis= expression (fio= ( 'ascending' | 'descending' ) )? ( ',' fos= expression (foo= ( 'ascending' | 'descending' ) )? )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:4: 'orderby' fis= expression (fio= ( 'ascending' | 'descending' ) )? ( ',' fos= expression (foo= ( 'ascending' | 'descending' ) )? )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal93 = (IToken)input.LT(1);
            	Match(input,50,FOLLOW_50_in_orderByClause1277); 
            	string_literal93_tree = (CommonTree)adaptor.Create(string_literal93);
            	adaptor.AddChild(root_0, string_literal93_tree);

            	PushFollow(FOLLOW_expression_in_orderByClause1281);
            	fis = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, fis.Tree);
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:32: (fio= ( 'ascending' | 'descending' ) )?
            	int alt35 = 2;
            	int LA35_0 = input.LA(1);
            	
            	if ( ((LA35_0 >= 51 && LA35_0 <= 52)) )
            	{
            	    alt35 = 1;
            	}
            	switch (alt35) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:32: fio= ( 'ascending' | 'descending' )
            	        {
            	        	fio = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= 51 && input.LA(1) <= 52) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, adaptor.Create(fio));
            	        	    errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse =
            	        	        new MismatchedSetException(null,input);
            	        	    RecoverFromMismatchedSet(input,mse,FOLLOW_set_in_orderByClause1285);    throw mse;
            	        	}

            	        
            	        }
            	        break;
            	
            	}

            	 criterias.Add(new OrderByCriteria(fis.value, fio == null || fio.Text == "ascending"));
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:157: ( ',' fos= expression (foo= ( 'ascending' | 'descending' ) )? )*
            	do 
            	{
            	    int alt37 = 2;
            	    int LA37_0 = input.LA(1);
            	    
            	    if ( (LA37_0 == 36) )
            	    {
            	        alt37 = 1;
            	    }
            	    
            	
            	    switch (alt37) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:158: ',' fos= expression (foo= ( 'ascending' | 'descending' ) )?
            			    {
            			    	char_literal94 = (IToken)input.LT(1);
            			    	Match(input,36,FOLLOW_36_in_orderByClause1298); 
            			    	char_literal94_tree = (CommonTree)adaptor.Create(char_literal94);
            			    	adaptor.AddChild(root_0, char_literal94_tree);

            			    	PushFollow(FOLLOW_expression_in_orderByClause1302);
            			    	fos = expression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, fos.Tree);
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:180: (foo= ( 'ascending' | 'descending' ) )?
            			    	int alt36 = 2;
            			    	int LA36_0 = input.LA(1);
            			    	
            			    	if ( ((LA36_0 >= 51 && LA36_0 <= 52)) )
            			    	{
            			    	    alt36 = 1;
            			    	}
            			    	switch (alt36) 
            			    	{
            			    	    case 1 :
            			    	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:257:180: foo= ( 'ascending' | 'descending' )
            			    	        {
            			    	        	foo = (IToken)input.LT(1);
            			    	        	if ( (input.LA(1) >= 51 && input.LA(1) <= 52) ) 
            			    	        	{
            			    	        	    input.Consume();
            			    	        	    adaptor.AddChild(root_0, adaptor.Create(foo));
            			    	        	    errorRecovery = false;
            			    	        	}
            			    	        	else 
            			    	        	{
            			    	        	    MismatchedSetException mse =
            			    	        	        new MismatchedSetException(null,input);
            			    	        	    RecoverFromMismatchedSet(input,mse,FOLLOW_set_in_orderByClause1306);    throw mse;
            			    	        	}

            			    	        
            			    	        }
            			    	        break;
            			    	
            			    	}

            			    	 criterias.Add(new OrderByCriteria(fos.value, foo == null || foo.Text == "ascending"));
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop37;
            	    }
            	} while (true);
            	
            	loop37:
            		;	// Stops C# compiler whinging that label 'loop37' has no statements

            	retval.value =  new OrderByClause(criterias.ToArray()); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end orderByClause

    public class selectClause_return : ParserRuleReturnScope 
    {
        public SelectClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start selectClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:261:1: selectClause returns [SelectClause value] : 'select' expression ;
    public selectClause_return selectClause() // throws RecognitionException [1]
    {   
        selectClause_return retval = new selectClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal95 = null;
        expression_return expression96 = null;
        
        
        CommonTree string_literal95_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:262:2: ( 'select' expression )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:262:4: 'select' expression
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal95 = (IToken)input.LT(1);
            	Match(input,53,FOLLOW_53_in_selectClause1336); 
            	string_literal95_tree = (CommonTree)adaptor.Create(string_literal95);
            	adaptor.AddChild(root_0, string_literal95_tree);

            	PushFollow(FOLLOW_expression_in_selectClause1338);
            	expression96 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression96.Tree);
            	 retval.value =  new SelectClause(expression96.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end selectClause

    public class groupClause_return : ParserRuleReturnScope 
    {
        public GroupClause value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start groupClause
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:266:1: groupClause returns [GroupClause value] : 'group' identifier 'by' expression ;
    public groupClause_return groupClause() // throws RecognitionException [1]
    {   
        groupClause_return retval = new groupClause_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal97 = null;
        IToken string_literal99 = null;
        identifier_return identifier98 = null;

        expression_return expression100 = null;
        
        
        CommonTree string_literal97_tree=null;
        CommonTree string_literal99_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:267:2: ( 'group' identifier 'by' expression )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:267:4: 'group' identifier 'by' expression
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal97 = (IToken)input.LT(1);
            	Match(input,54,FOLLOW_54_in_groupClause1356); 
            	string_literal97_tree = (CommonTree)adaptor.Create(string_literal97);
            	adaptor.AddChild(root_0, string_literal97_tree);

            	PushFollow(FOLLOW_identifier_in_groupClause1358);
            	identifier98 = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, identifier98.Tree);
            	string_literal99 = (IToken)input.LT(1);
            	Match(input,55,FOLLOW_55_in_groupClause1360); 
            	string_literal99_tree = (CommonTree)adaptor.Create(string_literal99);
            	adaptor.AddChild(root_0, string_literal99_tree);

            	PushFollow(FOLLOW_expression_in_groupClause1362);
            	expression100 = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, expression100.Tree);
            	 retval.value =  new GroupClause(identifier98.value, expression100.value); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end groupClause

    public class type_return : ParserRuleReturnScope 
    {
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start type
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:270:1: type : identifier ( '.' identifier )* ;
    public type_return type() // throws RecognitionException [1]
    {   
        type_return retval = new type_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal102 = null;
        identifier_return identifier101 = null;

        identifier_return identifier103 = null;
        
        
        CommonTree char_literal102_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:271:2: ( identifier ( '.' identifier )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:271:4: identifier ( '.' identifier )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_identifier_in_type1376);
            	identifier101 = identifier();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, identifier101.Tree);
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:271:15: ( '.' identifier )*
            	do 
            	{
            	    int alt38 = 2;
            	    int LA38_0 = input.LA(1);
            	    
            	    if ( (LA38_0 == 33) )
            	    {
            	        alt38 = 1;
            	    }
            	    
            	
            	    switch (alt38) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:271:17: '.' identifier
            			    {
            			    	char_literal102 = (IToken)input.LT(1);
            			    	Match(input,33,FOLLOW_33_in_type1380); 
            			    	char_literal102_tree = (CommonTree)adaptor.Create(char_literal102);
            			    	adaptor.AddChild(root_0, char_literal102_tree);

            			    	PushFollow(FOLLOW_identifier_in_type1382);
            			    	identifier103 = identifier();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, identifier103.Tree);
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop38;
            	    }
            	} while (true);
            	
            	loop38:
            		;	// Stops C# compiler whinging that label 'loop38' has no statements

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end type

    public class expressionList_return : ParserRuleReturnScope 
    {
        public List<Expression> value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start expressionList
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:274:1: expressionList returns [List<Expression> value] : first= expression ( ',' follow= expression )* ;
    public expressionList_return expressionList() // throws RecognitionException [1]
    {   
        expressionList_return retval = new expressionList_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal104 = null;
        expression_return first = null;

        expression_return follow = null;
        
        
        CommonTree char_literal104_tree=null;
    
        
        List<Expression> expressions = new List<Expression>();
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:278:2: (first= expression ( ',' follow= expression )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:278:4: first= expression ( ',' follow= expression )*
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	PushFollow(FOLLOW_expression_in_expressionList1407);
            	first = expression();
            	followingStackPointer_--;
            	
            	adaptor.AddChild(root_0, first.Tree);
            	expressions.Add(first.value);
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:278:55: ( ',' follow= expression )*
            	do 
            	{
            	    int alt39 = 2;
            	    int LA39_0 = input.LA(1);
            	    
            	    if ( (LA39_0 == 36) )
            	    {
            	        alt39 = 1;
            	    }
            	    
            	
            	    switch (alt39) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:278:57: ',' follow= expression
            			    {
            			    	char_literal104 = (IToken)input.LT(1);
            			    	Match(input,36,FOLLOW_36_in_expressionList1414); 
            			    	char_literal104_tree = (CommonTree)adaptor.Create(char_literal104);
            			    	adaptor.AddChild(root_0, char_literal104_tree);

            			    	PushFollow(FOLLOW_expression_in_expressionList1418);
            			    	follow = expression();
            			    	followingStackPointer_--;
            			    	
            			    	adaptor.AddChild(root_0, follow.Tree);
            			    	expressions.Add(follow.value);
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop39;
            	    }
            	} while (true);
            	
            	loop39:
            		;	// Stops C# compiler whinging that label 'loop39' has no statements

            	 retval.value =  expressions; 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end expressionList

    public class arguments_return : ParserRuleReturnScope 
    {
        public List<Expression> value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start arguments
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:282:1: arguments returns [List<Expression> value] : '(' ( expressionList )? ')' ;
    public arguments_return arguments() // throws RecognitionException [1]
    {   
        arguments_return retval = new arguments_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken char_literal105 = null;
        IToken char_literal107 = null;
        expressionList_return expressionList106 = null;
        
        
        CommonTree char_literal105_tree=null;
        CommonTree char_literal107_tree=null;
    
        
        retval.value =  new List<Expression>();
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:286:2: ( '(' ( expressionList )? ')' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:286:4: '(' ( expressionList )? ')'
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	char_literal105 = (IToken)input.LT(1);
            	Match(input,34,FOLLOW_34_in_arguments1447); 
            	char_literal105_tree = (CommonTree)adaptor.Create(char_literal105);
            	adaptor.AddChild(root_0, char_literal105_tree);

            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:286:8: ( expressionList )?
            	int alt40 = 2;
            	int LA40_0 = input.LA(1);
            	
            	if ( ((LA40_0 >= INTEGER && LA40_0 <= ID) || LA40_0 == 28 || LA40_0 == 32 || LA40_0 == 34 || LA40_0 == 38 || LA40_0 == 42 || (LA40_0 >= 57 && LA40_0 <= 58)) )
            	{
            	    alt40 = 1;
            	}
            	switch (alt40) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:286:10: expressionList
            	        {
            	        	PushFollow(FOLLOW_expressionList_in_arguments1451);
            	        	expressionList106 = expressionList();
            	        	followingStackPointer_--;
            	        	
            	        	adaptor.AddChild(root_0, expressionList106.Tree);
            	        	retval.value =  expressionList106.value;
            	        
            	        }
            	        break;
            	
            	}

            	char_literal107 = (IToken)input.LT(1);
            	Match(input,35,FOLLOW_35_in_arguments1458); 
            	char_literal107_tree = (CommonTree)adaptor.Create(char_literal107);
            	adaptor.AddChild(root_0, char_literal107_tree);

            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end arguments

    public class datetime_return : ParserRuleReturnScope 
    {
        public DateTime value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start datetime
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:308:1: datetime returns [DateTime value] : 'new' 'DateTime' '(' year= INTEGER ',' month= INTEGER ',' day= INTEGER ')' ;
    public datetime_return datetime() // throws RecognitionException [1]
    {   
        datetime_return retval = new datetime_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken year = null;
        IToken month = null;
        IToken day = null;
        IToken string_literal108 = null;
        IToken string_literal109 = null;
        IToken char_literal110 = null;
        IToken char_literal111 = null;
        IToken char_literal112 = null;
        IToken char_literal113 = null;
        
        CommonTree year_tree=null;
        CommonTree month_tree=null;
        CommonTree day_tree=null;
        CommonTree string_literal108_tree=null;
        CommonTree string_literal109_tree=null;
        CommonTree char_literal110_tree=null;
        CommonTree char_literal111_tree=null;
        CommonTree char_literal112_tree=null;
        CommonTree char_literal113_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:309:3: ( 'new' 'DateTime' '(' year= INTEGER ',' month= INTEGER ',' day= INTEGER ')' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:309:5: 'new' 'DateTime' '(' year= INTEGER ',' month= INTEGER ',' day= INTEGER ')'
            {
            	root_0 = (CommonTree)adaptor.GetNilNode();
            
            	string_literal108 = (IToken)input.LT(1);
            	Match(input,38,FOLLOW_38_in_datetime1610); 
            	string_literal109 = (IToken)input.LT(1);
            	Match(input,56,FOLLOW_56_in_datetime1613); 
            	string_literal109_tree = (CommonTree)adaptor.Create(string_literal109);
            	adaptor.AddChild(root_0, string_literal109_tree);

            	char_literal110 = (IToken)input.LT(1);
            	Match(input,34,FOLLOW_34_in_datetime1615); 
            	char_literal110_tree = (CommonTree)adaptor.Create(char_literal110);
            	adaptor.AddChild(root_0, char_literal110_tree);

            	year = (IToken)input.LT(1);
            	Match(input,INTEGER,FOLLOW_INTEGER_in_datetime1619); 
            	year_tree = (CommonTree)adaptor.Create(year);
            	adaptor.AddChild(root_0, year_tree);

            	char_literal111 = (IToken)input.LT(1);
            	Match(input,36,FOLLOW_36_in_datetime1621); 
            	char_literal111_tree = (CommonTree)adaptor.Create(char_literal111);
            	adaptor.AddChild(root_0, char_literal111_tree);

            	month = (IToken)input.LT(1);
            	Match(input,INTEGER,FOLLOW_INTEGER_in_datetime1625); 
            	month_tree = (CommonTree)adaptor.Create(month);
            	adaptor.AddChild(root_0, month_tree);

            	char_literal112 = (IToken)input.LT(1);
            	Match(input,36,FOLLOW_36_in_datetime1627); 
            	char_literal112_tree = (CommonTree)adaptor.Create(char_literal112);
            	adaptor.AddChild(root_0, char_literal112_tree);

            	day = (IToken)input.LT(1);
            	Match(input,INTEGER,FOLLOW_INTEGER_in_datetime1631); 
            	day_tree = (CommonTree)adaptor.Create(day);
            	adaptor.AddChild(root_0, day_tree);

            	char_literal113 = (IToken)input.LT(1);
            	Match(input,35,FOLLOW_35_in_datetime1633); 
            	char_literal113_tree = (CommonTree)adaptor.Create(char_literal113);
            	adaptor.AddChild(root_0, char_literal113_tree);

            	 retval.value =  new DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text)); 
            
            }
    
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end datetime

    public class boolean_return : ParserRuleReturnScope 
    {
        public bool value;
        internal CommonTree tree;
        override public object Tree
        {
        	get { return tree; }
        }
    };
    
    // $ANTLR start boolean
    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:312:1: boolean returns [bool value] : ( 'true' | 'false' );
    public boolean_return boolean() // throws RecognitionException [1]
    {   
        boolean_return retval = new boolean_return();
        retval.start = input.LT(1);
        
        CommonTree root_0 = null;
    
        IToken string_literal114 = null;
        IToken string_literal115 = null;
        
        CommonTree string_literal114_tree=null;
        CommonTree string_literal115_tree=null;
    
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:313:2: ( 'true' | 'false' )
            int alt41 = 2;
            int LA41_0 = input.LA(1);
            
            if ( (LA41_0 == 57) )
            {
                alt41 = 1;
            }
            else if ( (LA41_0 == 58) )
            {
                alt41 = 2;
            }
            else 
            {
                NoViableAltException nvae_d41s0 =
                    new NoViableAltException("312:1: boolean returns [bool value] : ( 'true' | 'false' );", 41, 0, input);
            
                throw nvae_d41s0;
            }
            switch (alt41) 
            {
                case 1 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:313:4: 'true'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	string_literal114 = (IToken)input.LT(1);
                    	Match(input,57,FOLLOW_57_in_boolean1657); 
                    	string_literal114_tree = (CommonTree)adaptor.Create(string_literal114);
                    	adaptor.AddChild(root_0, string_literal114_tree);

                    	 retval.value =  true; 
                    
                    }
                    break;
                case 2 :
                    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:314:4: 'false'
                    {
                    	root_0 = (CommonTree)adaptor.GetNilNode();
                    
                    	string_literal115 = (IToken)input.LT(1);
                    	Match(input,58,FOLLOW_58_in_boolean1664); 
                    	string_literal115_tree = (CommonTree)adaptor.Create(string_literal115);
                    	adaptor.AddChild(root_0, string_literal115_tree);

                    	 retval.value =  false; 
                    
                    }
                    break;
            
            }
            retval.stop = input.LT(-1);
            
            	retval.tree = (CommonTree)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, retval.start, retval.stop);
    
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end boolean


   	protected DFA27 dfa27;
	private void InitializeCyclicDFAs()
	{
    	this.dfa27 = new DFA27(this);
	}

    static readonly short[] DFA27_eot = {
        -1, -1, -1, -1, -1, -1, -1
        };
    static readonly short[] DFA27_eof = {
        -1, -1, -1, -1, -1, -1, -1
        };
    static readonly int[] DFA27_min = {
        38, 7, 33, 0, 7, 0, 33
        };
    static readonly int[] DFA27_max = {
        38, 39, 39, 0, 7, 0, 39
        };
    static readonly short[] DFA27_accept = {
        -1, -1, -1, 1, -1, 2, -1
        };
    static readonly short[] DFA27_special = {
        -1, -1, -1, -1, -1, -1, -1
        };
    
    static readonly short[] dfa27_transition_null = null;

    static readonly short[] dfa27_transition0 = {
    	4, 5, -1, -1, -1, -1, 3
    	};
    static readonly short[] dfa27_transition1 = {
    	2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
    	    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 3
    	};
    static readonly short[] dfa27_transition2 = {
    	6
    	};
    static readonly short[] dfa27_transition3 = {
    	1
    	};
    
    static readonly short[][] DFA27_transition = {
    	dfa27_transition3,
    	dfa27_transition1,
    	dfa27_transition0,
    	dfa27_transition_null,
    	dfa27_transition2,
    	dfa27_transition_null,
    	dfa27_transition0
        };
    
    protected class DFA27 : DFA
    {
        public DFA27(BaseRecognizer recognizer) 
        {
            this.recognizer = recognizer;
            this.decisionNumber = 27;
            this.eot = DFA27_eot;
            this.eof = DFA27_eof;
            this.min = DFA27_min;
            this.max = DFA27_max;
            this.accept     = DFA27_accept;
            this.special    = DFA27_special;
            this.transition = DFA27_transition;
        }
    
        override public string Description
        {
            get { return "192:1: newExpression returns [Expression value] : ( 'new' ( type )? '{' (firstid= identifier '=' )? firstexp= expression ( ',' (followid= identifier '=' )? followexp= expression )* '}' | 'new' type arguments );"; }
        }
    
    }
    
 

    public static readonly BitSet FOLLOW_expression_in_linqExpression56 = new BitSet(new ulong[]{0x0000000000000000UL});
    public static readonly BitSet FOLLOW_EOF_in_linqExpression58 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expressionItem_in_expression76 = new BitSet(new ulong[]{0x0000000000008002UL});
    public static readonly BitSet FOLLOW_15_in_expression81 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_expression83 = new BitSet(new ulong[]{0x0000000000010000UL});
    public static readonly BitSet FOLLOW_16_in_expression85 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_conditionalExpression_in_expressionItem108 = new BitSet(new ulong[]{0x0000000000020002UL});
    public static readonly BitSet FOLLOW_17_in_expressionItem114 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_conditionalExpression_in_expressionItem118 = new BitSet(new ulong[]{0x0000000000040000UL});
    public static readonly BitSet FOLLOW_18_in_expressionItem120 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_conditionalExpression_in_expressionItem124 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_booleanAndExpression_in_conditionalExpression151 = new BitSet(new ulong[]{0x0000000000080002UL});
    public static readonly BitSet FOLLOW_19_in_conditionalExpression160 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_booleanAndExpression_in_conditionalExpression170 = new BitSet(new ulong[]{0x0000000000080002UL});
    public static readonly BitSet FOLLOW_equalityExpression_in_booleanAndExpression204 = new BitSet(new ulong[]{0x0000000000100002UL});
    public static readonly BitSet FOLLOW_20_in_booleanAndExpression213 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_equalityExpression_in_booleanAndExpression223 = new BitSet(new ulong[]{0x0000000000100002UL});
    public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression255 = new BitSet(new ulong[]{0x0000000000600002UL});
    public static readonly BitSet FOLLOW_21_in_equalityExpression266 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_22_in_equalityExpression276 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression288 = new BitSet(new ulong[]{0x0000000000600002UL});
    public static readonly BitSet FOLLOW_additiveExpression_in_relationalExpression321 = new BitSet(new ulong[]{0x0000000007800002UL});
    public static readonly BitSet FOLLOW_23_in_relationalExpression332 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_24_in_relationalExpression342 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_25_in_relationalExpression353 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_26_in_relationalExpression363 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_additiveExpression_in_relationalExpression375 = new BitSet(new ulong[]{0x0000000007800002UL});
    public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression407 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_27_in_additiveExpression418 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_28_in_additiveExpression428 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression440 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression472 = new BitSet(new ulong[]{0x00000000E0000002UL});
    public static readonly BitSet FOLLOW_29_in_multiplicativeExpression483 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_30_in_multiplicativeExpression493 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_31_in_multiplicativeExpression503 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression515 = new BitSet(new ulong[]{0x00000000E0000002UL});
    public static readonly BitSet FOLLOW_statement_in_unaryExpression542 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_unaryExpression553 = new BitSet(new ulong[]{0x06000444000000F0UL});
    public static readonly BitSet FOLLOW_statement_in_unaryExpression555 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_28_in_unaryExpression566 = new BitSet(new ulong[]{0x06000444000000F0UL});
    public static readonly BitSet FOLLOW_statement_in_unaryExpression568 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_primaryExpression_in_statement594 = new BitSet(new ulong[]{0x0000000200000002UL});
    public static readonly BitSet FOLLOW_33_in_statement600 = new BitSet(new ulong[]{0x06000444000000F0UL});
    public static readonly BitSet FOLLOW_primaryExpression_in_statement604 = new BitSet(new ulong[]{0x0000000200000002UL});
    public static readonly BitSet FOLLOW_34_in_primaryExpression632 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_primaryExpression634 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_primaryExpression636 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_value_in_primaryExpression646 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_newExpression_in_primaryExpression654 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_identifier_in_primaryExpression662 = new BitSet(new ulong[]{0x0000000400000002UL});
    public static readonly BitSet FOLLOW_arguments_in_primaryExpression667 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_methodCall_in_primaryExpression676 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_queryExpression_in_primaryExpression685 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGER_in_value704 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FLOAT_in_value712 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_STRING_in_value720 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_datetime_in_value729 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_boolean_in_value736 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall763 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_34_in_methodCall767 = new BitSet(new ulong[]{0x0000002000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall773 = new BitSet(new ulong[]{0x0000003000000000UL});
    public static readonly BitSet FOLLOW_36_in_methodCall778 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall782 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_37_in_methodCall793 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_methodCall797 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_methodCall801 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall811 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_34_in_methodCall815 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_34_in_methodCall817 = new BitSet(new ulong[]{0x0000000800000080UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall822 = new BitSet(new ulong[]{0x0000001800000000UL});
    public static readonly BitSet FOLLOW_36_in_methodCall827 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_methodCall831 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_methodCall841 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_37_in_methodCall843 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_methodCall847 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_methodCall851 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ID_in_identifier869 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_newExpression890 = new BitSet(new ulong[]{0x0000008000000080UL});
    public static readonly BitSet FOLLOW_type_in_newExpression893 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_39_in_newExpression900 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_identifier_in_newExpression905 = new BitSet(new ulong[]{0x0000010000000000UL});
    public static readonly BitSet FOLLOW_40_in_newExpression907 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_newExpression916 = new BitSet(new ulong[]{0x0000021000000000UL});
    public static readonly BitSet FOLLOW_36_in_newExpression921 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_identifier_in_newExpression926 = new BitSet(new ulong[]{0x0000010000000000UL});
    public static readonly BitSet FOLLOW_40_in_newExpression928 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_newExpression936 = new BitSet(new ulong[]{0x0000021000000000UL});
    public static readonly BitSet FOLLOW_41_in_newExpression942 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_newExpression953 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_type_in_newExpression955 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_arguments_in_newExpression957 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_fromClause_in_queryExpression984 = new BitSet(new ulong[]{0x0064E40000000000UL});
    public static readonly BitSet FOLLOW_queryBody_in_queryExpression986 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_42_in_fromClause1009 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_type_in_fromClause1012 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_fromClause1019 = new BitSet(new ulong[]{0x0000080000000000UL});
    public static readonly BitSet FOLLOW_43_in_fromClause1021 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_fromClause1023 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_queryBodyClause_in_queryBody1048 = new BitSet(new ulong[]{0x0064E40000000000UL});
    public static readonly BitSet FOLLOW_selectClause_in_queryBody1056 = new BitSet(new ulong[]{0x0000100000000002UL});
    public static readonly BitSet FOLLOW_groupClause_in_queryBody1062 = new BitSet(new ulong[]{0x0000100000000002UL});
    public static readonly BitSet FOLLOW_queryContinuation_in_queryBody1068 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_fromClause_in_queryBodyClause1092 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_letClause_in_queryBodyClause1101 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_whereClause_in_queryBodyClause1110 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_joinClause_in_queryBodyClause1119 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_orderByClause_in_queryBodyClause1128 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_44_in_queryContinuation1145 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_queryContinuation1147 = new BitSet(new ulong[]{0x0064E40000000000UL});
    public static readonly BitSet FOLLOW_queryBody_in_queryContinuation1149 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_45_in_whereClause1167 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_whereClause1169 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_46_in_letClause1184 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_letClause1186 = new BitSet(new ulong[]{0x0000010000000000UL});
    public static readonly BitSet FOLLOW_40_in_letClause1188 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_letClause1190 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_47_in_joinClause1211 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_type_in_joinClause1214 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_joinClause1223 = new BitSet(new ulong[]{0x0000080000000000UL});
    public static readonly BitSet FOLLOW_43_in_joinClause1225 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_joinClause1229 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_48_in_joinClause1231 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_joinClause1235 = new BitSet(new ulong[]{0x0002000000000000UL});
    public static readonly BitSet FOLLOW_49_in_joinClause1237 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_joinClause1241 = new BitSet(new ulong[]{0x0000100000000002UL});
    public static readonly BitSet FOLLOW_44_in_joinClause1244 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_joinClause1248 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_50_in_orderByClause1277 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_orderByClause1281 = new BitSet(new ulong[]{0x0018001000000002UL});
    public static readonly BitSet FOLLOW_set_in_orderByClause1285 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_36_in_orderByClause1298 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_orderByClause1302 = new BitSet(new ulong[]{0x0018001000000002UL});
    public static readonly BitSet FOLLOW_set_in_orderByClause1306 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_53_in_selectClause1336 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_selectClause1338 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_54_in_groupClause1356 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_groupClause1358 = new BitSet(new ulong[]{0x0080000000000000UL});
    public static readonly BitSet FOLLOW_55_in_groupClause1360 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_groupClause1362 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_identifier_in_type1376 = new BitSet(new ulong[]{0x0000000200000002UL});
    public static readonly BitSet FOLLOW_33_in_type1380 = new BitSet(new ulong[]{0x0000000000000080UL});
    public static readonly BitSet FOLLOW_identifier_in_type1382 = new BitSet(new ulong[]{0x0000000200000002UL});
    public static readonly BitSet FOLLOW_expression_in_expressionList1407 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_36_in_expressionList1414 = new BitSet(new ulong[]{0x06000445100000F0UL});
    public static readonly BitSet FOLLOW_expression_in_expressionList1418 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_34_in_arguments1447 = new BitSet(new ulong[]{0x0600044D100000F0UL});
    public static readonly BitSet FOLLOW_expressionList_in_arguments1451 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_arguments1458 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_datetime1610 = new BitSet(new ulong[]{0x0100000000000000UL});
    public static readonly BitSet FOLLOW_56_in_datetime1613 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_34_in_datetime1615 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_INTEGER_in_datetime1619 = new BitSet(new ulong[]{0x0000001000000000UL});
    public static readonly BitSet FOLLOW_36_in_datetime1621 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_INTEGER_in_datetime1625 = new BitSet(new ulong[]{0x0000001000000000UL});
    public static readonly BitSet FOLLOW_36_in_datetime1627 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_INTEGER_in_datetime1631 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_datetime1633 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_57_in_boolean1657 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_58_in_boolean1664 = new BitSet(new ulong[]{0x0000000000000002UL});

}

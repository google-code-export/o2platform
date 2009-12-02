// $ANTLR 3.0.1 C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g 2008-01-14 11:06:21

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;



public class NLinqLexer : Lexer 
{
    public const int LETTER = 8;
    public const int FLOAT = 5;
    public const int T29 = 29;
    public const int T28 = 28;
    public const int T27 = 27;
    public const int T26 = 26;
    public const int ID = 7;
    public const int T25 = 25;
    public const int EOF = -1;
    public const int T24 = 24;
    public const int HexDigit = 13;
    public const int T23 = 23;
    public const int T22 = 22;
    public const int T21 = 21;
    public const int T20 = 20;
    public const int T38 = 38;
    public const int T37 = 37;
    public const int DIGIT = 9;
    public const int T39 = 39;
    public const int T34 = 34;
    public const int T33 = 33;
    public const int T36 = 36;
    public const int T35 = 35;
    public const int T30 = 30;
    public const int T32 = 32;
    public const int T31 = 31;
    public const int INTEGER = 4;
    public const int NULL = 11;
    public const int T49 = 49;
    public const int T48 = 48;
    public const int T43 = 43;
    public const int Tokens = 59;
    public const int T42 = 42;
    public const int T41 = 41;
    public const int T40 = 40;
    public const int T47 = 47;
    public const int T46 = 46;
    public const int T45 = 45;
    public const int T44 = 44;
    public const int WS = 14;
    public const int T50 = 50;
    public const int UnicodeEscape = 12;
    public const int T52 = 52;
    public const int T15 = 15;
    public const int T51 = 51;
    public const int T16 = 16;
    public const int T54 = 54;
    public const int T17 = 17;
    public const int T53 = 53;
    public const int T18 = 18;
    public const int EscapeSequence = 10;
    public const int T56 = 56;
    public const int T19 = 19;
    public const int T55 = 55;
    public const int T58 = 58;
    public const int STRING = 6;
    public const int T57 = 57;

    public NLinqLexer() 
    {
		InitializeCyclicDFAs();
    }
    public NLinqLexer(ICharStream input) 
		: base(input)
	{
		InitializeCyclicDFAs();
    }
    
    override public string GrammarFileName
    {
    	get { return "C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g";} 
    }

    // $ANTLR start T15 
    public void mT15() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T15;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:7:5: ( '[' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:7:7: '['
            {
            	Match('['); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T15

    // $ANTLR start T16 
    public void mT16() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T16;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:8:5: ( ']' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:8:7: ']'
            {
            	Match(']'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T16

    // $ANTLR start T17 
    public void mT17() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T17;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:9:5: ( '?' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:9:7: '?'
            {
            	Match('?'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T17

    // $ANTLR start T18 
    public void mT18() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T18;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:10:5: ( ':' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:10:7: ':'
            {
            	Match(':'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T18

    // $ANTLR start T19 
    public void mT19() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T19;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:11:5: ( '||' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:11:7: '||'
            {
            	Match("||"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T19

    // $ANTLR start T20 
    public void mT20() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T20;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:12:5: ( '&&' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:12:7: '&&'
            {
            	Match("&&"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T20

    // $ANTLR start T21 
    public void mT21() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T21;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:13:5: ( '==' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:13:7: '=='
            {
            	Match("=="); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T21

    // $ANTLR start T22 
    public void mT22() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T22;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:14:5: ( '!=' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:14:7: '!='
            {
            	Match("!="); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T22

    // $ANTLR start T23 
    public void mT23() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T23;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:15:5: ( '<' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:15:7: '<'
            {
            	Match('<'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T23

    // $ANTLR start T24 
    public void mT24() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T24;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:16:5: ( '<=' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:16:7: '<='
            {
            	Match("<="); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T24

    // $ANTLR start T25 
    public void mT25() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T25;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:17:5: ( '>' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:17:7: '>'
            {
            	Match('>'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T25

    // $ANTLR start T26 
    public void mT26() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T26;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:18:5: ( '>=' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:18:7: '>='
            {
            	Match(">="); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T26

    // $ANTLR start T27 
    public void mT27() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T27;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:19:5: ( '+' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:19:7: '+'
            {
            	Match('+'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T27

    // $ANTLR start T28 
    public void mT28() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T28;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:20:5: ( '-' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:20:7: '-'
            {
            	Match('-'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T28

    // $ANTLR start T29 
    public void mT29() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T29;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:21:5: ( '*' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:21:7: '*'
            {
            	Match('*'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T29

    // $ANTLR start T30 
    public void mT30() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T30;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:22:5: ( '/' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:22:7: '/'
            {
            	Match('/'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T30

    // $ANTLR start T31 
    public void mT31() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T31;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:23:5: ( '%' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:23:7: '%'
            {
            	Match('%'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T31

    // $ANTLR start T32 
    public void mT32() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T32;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:24:5: ( '!' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:24:7: '!'
            {
            	Match('!'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T32

    // $ANTLR start T33 
    public void mT33() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T33;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:25:5: ( '.' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:25:7: '.'
            {
            	Match('.'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T33

    // $ANTLR start T34 
    public void mT34() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T34;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:26:5: ( '(' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:26:7: '('
            {
            	Match('('); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T34

    // $ANTLR start T35 
    public void mT35() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T35;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:27:5: ( ')' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:27:7: ')'
            {
            	Match(')'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T35

    // $ANTLR start T36 
    public void mT36() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T36;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:28:5: ( ',' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:28:7: ','
            {
            	Match(','); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T36

    // $ANTLR start T37 
    public void mT37() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T37;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:29:5: ( '=>' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:29:7: '=>'
            {
            	Match("=>"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T37

    // $ANTLR start T38 
    public void mT38() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T38;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:30:5: ( 'new' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:30:7: 'new'
            {
            	Match("new"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T38

    // $ANTLR start T39 
    public void mT39() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T39;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:31:5: ( '{' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:31:7: '{'
            {
            	Match('{'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T39

    // $ANTLR start T40 
    public void mT40() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T40;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:32:5: ( '=' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:32:7: '='
            {
            	Match('='); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T40

    // $ANTLR start T41 
    public void mT41() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T41;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:33:5: ( '}' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:33:7: '}'
            {
            	Match('}'); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T41

    // $ANTLR start T42 
    public void mT42() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T42;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:34:5: ( 'from' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:34:7: 'from'
            {
            	Match("from"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T42

    // $ANTLR start T43 
    public void mT43() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T43;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:35:5: ( 'in' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:35:7: 'in'
            {
            	Match("in"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T43

    // $ANTLR start T44 
    public void mT44() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T44;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:36:5: ( 'into' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:36:7: 'into'
            {
            	Match("into"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T44

    // $ANTLR start T45 
    public void mT45() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T45;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:37:5: ( 'where' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:37:7: 'where'
            {
            	Match("where"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T45

    // $ANTLR start T46 
    public void mT46() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T46;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:38:5: ( 'let' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:38:7: 'let'
            {
            	Match("let"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T46

    // $ANTLR start T47 
    public void mT47() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T47;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:39:5: ( 'join' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:39:7: 'join'
            {
            	Match("join"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T47

    // $ANTLR start T48 
    public void mT48() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T48;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:40:5: ( 'on' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:40:7: 'on'
            {
            	Match("on"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T48

    // $ANTLR start T49 
    public void mT49() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T49;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:41:5: ( 'equals' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:41:7: 'equals'
            {
            	Match("equals"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T49

    // $ANTLR start T50 
    public void mT50() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T50;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:42:5: ( 'orderby' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:42:7: 'orderby'
            {
            	Match("orderby"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T50

    // $ANTLR start T51 
    public void mT51() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T51;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:43:5: ( 'ascending' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:43:7: 'ascending'
            {
            	Match("ascending"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T51

    // $ANTLR start T52 
    public void mT52() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T52;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:44:5: ( 'descending' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:44:7: 'descending'
            {
            	Match("descending"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T52

    // $ANTLR start T53 
    public void mT53() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T53;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:45:5: ( 'select' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:45:7: 'select'
            {
            	Match("select"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T53

    // $ANTLR start T54 
    public void mT54() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T54;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:46:5: ( 'group' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:46:7: 'group'
            {
            	Match("group"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T54

    // $ANTLR start T55 
    public void mT55() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T55;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:47:5: ( 'by' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:47:7: 'by'
            {
            	Match("by"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T55

    // $ANTLR start T56 
    public void mT56() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T56;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:48:5: ( 'DateTime' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:48:7: 'DateTime'
            {
            	Match("DateTime"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T56

    // $ANTLR start T57 
    public void mT57() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T57;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:49:5: ( 'true' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:49:7: 'true'
            {
            	Match("true"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T57

    // $ANTLR start T58 
    public void mT58() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = T58;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:50:5: ( 'false' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:50:7: 'false'
            {
            	Match("false"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end T58

    // $ANTLR start ID 
    public void mID() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = ID;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:289:2: ( LETTER ( LETTER | DIGIT )* )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:289:5: LETTER ( LETTER | DIGIT )*
            {
            	mLETTER(); 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:289:12: ( LETTER | DIGIT )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);
            	    
            	    if ( ((LA1_0 >= '0' && LA1_0 <= '9') || (LA1_0 >= 'A' && LA1_0 <= 'Z') || LA1_0 == '_' || (LA1_0 >= 'a' && LA1_0 <= 'z')) )
            	    {
            	        alt1 = 1;
            	    }
            	    
            	
            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();
            			    	
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse =
            			    	        new MismatchedSetException(null,input);
            			    	    Recover(mse);    throw mse;
            			    	}

            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop1;
            	    }
            	} while (true);
            	
            	loop1:
            		;	// Stops C# compiler whinging that label 'loop1' has no statements

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end ID

    // $ANTLR start INTEGER 
    public void mINTEGER() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = INTEGER;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:293:2: ( ( DIGIT )+ )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:293:4: ( DIGIT )+
            {
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:293:4: ( DIGIT )+
            	int cnt2 = 0;
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);
            	    
            	    if ( ((LA2_0 >= '0' && LA2_0 <= '9')) )
            	    {
            	        alt2 = 1;
            	    }
            	    
            	
            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:293:4: DIGIT
            			    {
            			    	mDIGIT(); 
            			    
            			    }
            			    break;
            	
            			default:
            			    if ( cnt2 >= 1 ) goto loop2;
            		            EarlyExitException eee =
            		                new EarlyExitException(2, input);
            		            throw eee;
            	    }
            	    cnt2++;
            	} while (true);
            	
            	loop2:
            		;	// Stops C# compiler whinging that label 'loop2' has no statements

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end INTEGER

    // $ANTLR start FLOAT 
    public void mFLOAT() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = FLOAT;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:2: ( ( DIGIT )* '.' ( DIGIT )+ )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:4: ( DIGIT )* '.' ( DIGIT )+
            {
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:4: ( DIGIT )*
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);
            	    
            	    if ( ((LA3_0 >= '0' && LA3_0 <= '9')) )
            	    {
            	        alt3 = 1;
            	    }
            	    
            	
            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:4: DIGIT
            			    {
            			    	mDIGIT(); 
            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop3;
            	    }
            	} while (true);
            	
            	loop3:
            		;	// Stops C# compiler whinging that label 'loop3' has no statements

            	Match('.'); 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:15: ( DIGIT )+
            	int cnt4 = 0;
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);
            	    
            	    if ( ((LA4_0 >= '0' && LA4_0 <= '9')) )
            	    {
            	        alt4 = 1;
            	    }
            	    
            	
            	    switch (alt4) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:297:15: DIGIT
            			    {
            			    	mDIGIT(); 
            			    
            			    }
            			    break;
            	
            			default:
            			    if ( cnt4 >= 1 ) goto loop4;
            		            EarlyExitException eee =
            		                new EarlyExitException(4, input);
            		            throw eee;
            	    }
            	    cnt4++;
            	} while (true);
            	
            	loop4:
            		;	// Stops C# compiler whinging that label 'loop4' has no statements

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end FLOAT

    // $ANTLR start STRING 
    public void mSTRING() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = STRING;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:6: ( '\\'' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )* '\\'' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:10: '\\'' ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )* '\\''
            {
            	Match('\''); 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:15: ( EscapeSequence | ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) ) )*
            	do 
            	{
            	    int alt5 = 3;
            	    int LA5_0 = input.LA(1);
            	    
            	    if ( (LA5_0 == '\\') )
            	    {
            	        alt5 = 1;
            	    }
            	    else if ( ((LA5_0 >= ' ' && LA5_0 <= '&') || (LA5_0 >= '(' && LA5_0 <= '[') || (LA5_0 >= ']' && LA5_0 <= '\uFFFE')) )
            	    {
            	        alt5 = 2;
            	    }
            	    
            	
            	    switch (alt5) 
            		{
            			case 1 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:17: EscapeSequence
            			    {
            			    	mEscapeSequence(); 
            			    
            			    }
            			    break;
            			case 2 :
            			    // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:34: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) )
            			    {
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:34: ( options {greedy=false; } : ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' ) )
            			    	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:301:61: ~ ( '\\u0000' .. '\\u001f' | '\\\\' | '\\'' )
            			    	{
            			    		if ( (input.LA(1) >= ' ' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFE') ) 
            			    		{
            			    		    input.Consume();
            			    		
            			    		}
            			    		else 
            			    		{
            			    		    MismatchedSetException mse =
            			    		        new MismatchedSetException(null,input);
            			    		    Recover(mse);    throw mse;
            			    		}

            			    	
            			    	}

            			    
            			    }
            			    break;
            	
            			default:
            			    goto loop5;
            	    }
            	} while (true);
            	
            	loop5:
            		;	// Stops C# compiler whinging that label 'loop5' has no statements

            	Match('\''); 
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end STRING

    // $ANTLR start NULL 
    public void mNULL() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = NULL;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:305:6: ( 'null' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:305:10: 'null'
            {
            	Match("null"); 

            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end NULL

    // $ANTLR start LETTER 
    public void mLETTER() // throws RecognitionException [2]
    {
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:318:2: ( 'a' .. 'z' | 'A' .. 'Z' | '_' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();
            	
            	}
            	else 
            	{
            	    MismatchedSetException mse =
            	        new MismatchedSetException(null,input);
            	    Recover(mse);    throw mse;
            	}

            
            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end LETTER

    // $ANTLR start DIGIT 
    public void mDIGIT() // throws RecognitionException [2]
    {
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:324:2: ( '0' .. '9' )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:324:4: '0' .. '9'
            {
            	MatchRange('0','9'); 
            
            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end DIGIT

    // $ANTLR start EscapeSequence 
    public void mEscapeSequence() // throws RecognitionException [2]
    {
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:328:2: ( '\\\\' ( 'n' | 'r' | 't' | '\\'' | '\\\\' | UnicodeEscape ) )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:328:4: '\\\\' ( 'n' | 'r' | 't' | '\\'' | '\\\\' | UnicodeEscape )
            {
            	Match('\\'); 
            	// C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:329:4: ( 'n' | 'r' | 't' | '\\'' | '\\\\' | UnicodeEscape )
            	int alt6 = 6;
            	switch ( input.LA(1) ) 
            	{
            	case 'n':
            		{
            	    alt6 = 1;
            	    }
            	    break;
            	case 'r':
            		{
            	    alt6 = 2;
            	    }
            	    break;
            	case 't':
            		{
            	    alt6 = 3;
            	    }
            	    break;
            	case '\'':
            		{
            	    alt6 = 4;
            	    }
            	    break;
            	case '\\':
            		{
            	    alt6 = 5;
            	    }
            	    break;
            	case 'u':
            		{
            	    alt6 = 6;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d6s0 =
            		        new NoViableAltException("329:4: ( 'n' | 'r' | 't' | '\\'' | '\\\\' | UnicodeEscape )", 6, 0, input);
            	
            		    throw nvae_d6s0;
            	}
            	
            	switch (alt6) 
            	{
            	    case 1 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:330:5: 'n'
            	        {
            	        	Match('n'); 
            	        
            	        }
            	        break;
            	    case 2 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:331:4: 'r'
            	        {
            	        	Match('r'); 
            	        
            	        }
            	        break;
            	    case 3 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:332:4: 't'
            	        {
            	        	Match('t'); 
            	        
            	        }
            	        break;
            	    case 4 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:333:4: '\\''
            	        {
            	        	Match('\''); 
            	        
            	        }
            	        break;
            	    case 5 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:334:4: '\\\\'
            	        {
            	        	Match('\\'); 
            	        
            	        }
            	        break;
            	    case 6 :
            	        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:335:4: UnicodeEscape
            	        {
            	        	mUnicodeEscape(); 
            	        
            	        }
            	        break;
            	
            	}

            
            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end EscapeSequence

    // $ANTLR start HexDigit 
    public void mHexDigit() // throws RecognitionException [2]
    {
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:340:2: ( ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' ) )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:340:5: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )
            {
            	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f') ) 
            	{
            	    input.Consume();
            	
            	}
            	else 
            	{
            	    MismatchedSetException mse =
            	        new MismatchedSetException(null,input);
            	    Recover(mse);    throw mse;
            	}

            
            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end HexDigit

    // $ANTLR start UnicodeEscape 
    public void mUnicodeEscape() // throws RecognitionException [2]
    {
        try 
    	{
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:344:6: ( 'u' HexDigit HexDigit HexDigit HexDigit )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:344:12: 'u' HexDigit HexDigit HexDigit HexDigit
            {
            	Match('u'); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 
            
            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end UnicodeEscape

    // $ANTLR start WS 
    public void mWS() // throws RecognitionException [2]
    {
        try 
    	{
            int _type = WS;
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:348:4: ( ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' ) )
            // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:348:7: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' )
            {
            	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || (input.LA(1) >= '\f' && input.LA(1) <= '\r') || input.LA(1) == ' ' ) 
            	{
            	    input.Consume();
            	
            	}
            	else 
            	{
            	    MismatchedSetException mse =
            	        new MismatchedSetException(null,input);
            	    Recover(mse);    throw mse;
            	}

            	channel=HIDDEN;
            
            }
    
            this.type = _type;
        }
        finally 
    	{
        }
    }
    // $ANTLR end WS

    override public void mTokens() // throws RecognitionException 
    {
        // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:8: ( T15 | T16 | T17 | T18 | T19 | T20 | T21 | T22 | T23 | T24 | T25 | T26 | T27 | T28 | T29 | T30 | T31 | T32 | T33 | T34 | T35 | T36 | T37 | T38 | T39 | T40 | T41 | T42 | T43 | T44 | T45 | T46 | T47 | T48 | T49 | T50 | T51 | T52 | T53 | T54 | T55 | T56 | T57 | T58 | ID | INTEGER | FLOAT | STRING | NULL | WS )
        int alt7 = 50;
        alt7 = dfa7.Predict(input);
        switch (alt7) 
        {
            case 1 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:10: T15
                {
                	mT15(); 
                
                }
                break;
            case 2 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:14: T16
                {
                	mT16(); 
                
                }
                break;
            case 3 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:18: T17
                {
                	mT17(); 
                
                }
                break;
            case 4 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:22: T18
                {
                	mT18(); 
                
                }
                break;
            case 5 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:26: T19
                {
                	mT19(); 
                
                }
                break;
            case 6 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:30: T20
                {
                	mT20(); 
                
                }
                break;
            case 7 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:34: T21
                {
                	mT21(); 
                
                }
                break;
            case 8 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:38: T22
                {
                	mT22(); 
                
                }
                break;
            case 9 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:42: T23
                {
                	mT23(); 
                
                }
                break;
            case 10 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:46: T24
                {
                	mT24(); 
                
                }
                break;
            case 11 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:50: T25
                {
                	mT25(); 
                
                }
                break;
            case 12 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:54: T26
                {
                	mT26(); 
                
                }
                break;
            case 13 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:58: T27
                {
                	mT27(); 
                
                }
                break;
            case 14 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:62: T28
                {
                	mT28(); 
                
                }
                break;
            case 15 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:66: T29
                {
                	mT29(); 
                
                }
                break;
            case 16 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:70: T30
                {
                	mT30(); 
                
                }
                break;
            case 17 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:74: T31
                {
                	mT31(); 
                
                }
                break;
            case 18 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:78: T32
                {
                	mT32(); 
                
                }
                break;
            case 19 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:82: T33
                {
                	mT33(); 
                
                }
                break;
            case 20 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:86: T34
                {
                	mT34(); 
                
                }
                break;
            case 21 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:90: T35
                {
                	mT35(); 
                
                }
                break;
            case 22 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:94: T36
                {
                	mT36(); 
                
                }
                break;
            case 23 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:98: T37
                {
                	mT37(); 
                
                }
                break;
            case 24 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:102: T38
                {
                	mT38(); 
                
                }
                break;
            case 25 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:106: T39
                {
                	mT39(); 
                
                }
                break;
            case 26 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:110: T40
                {
                	mT40(); 
                
                }
                break;
            case 27 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:114: T41
                {
                	mT41(); 
                
                }
                break;
            case 28 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:118: T42
                {
                	mT42(); 
                
                }
                break;
            case 29 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:122: T43
                {
                	mT43(); 
                
                }
                break;
            case 30 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:126: T44
                {
                	mT44(); 
                
                }
                break;
            case 31 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:130: T45
                {
                	mT45(); 
                
                }
                break;
            case 32 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:134: T46
                {
                	mT46(); 
                
                }
                break;
            case 33 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:138: T47
                {
                	mT47(); 
                
                }
                break;
            case 34 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:142: T48
                {
                	mT48(); 
                
                }
                break;
            case 35 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:146: T49
                {
                	mT49(); 
                
                }
                break;
            case 36 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:150: T50
                {
                	mT50(); 
                
                }
                break;
            case 37 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:154: T51
                {
                	mT51(); 
                
                }
                break;
            case 38 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:158: T52
                {
                	mT52(); 
                
                }
                break;
            case 39 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:162: T53
                {
                	mT53(); 
                
                }
                break;
            case 40 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:166: T54
                {
                	mT54(); 
                
                }
                break;
            case 41 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:170: T55
                {
                	mT55(); 
                
                }
                break;
            case 42 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:174: T56
                {
                	mT56(); 
                
                }
                break;
            case 43 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:178: T57
                {
                	mT57(); 
                
                }
                break;
            case 44 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:182: T58
                {
                	mT58(); 
                
                }
                break;
            case 45 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:186: ID
                {
                	mID(); 
                
                }
                break;
            case 46 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:189: INTEGER
                {
                	mINTEGER(); 
                
                }
                break;
            case 47 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:197: FLOAT
                {
                	mFLOAT(); 
                
                }
                break;
            case 48 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:203: STRING
                {
                	mSTRING(); 
                
                }
                break;
            case 49 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:210: NULL
                {
                	mNULL(); 
                
                }
                break;
            case 50 :
                // C:\\Documents and Settings\\Sébastien Ros\\Mes documents\\Développement\\NLinq\\NLinq.g:1:215: WS
                {
                	mWS(); 
                
                }
                break;
        
        }
    
    }


    protected DFA7 dfa7;
	private void InitializeCyclicDFAs()
	{
	    this.dfa7 = new DFA7(this);
	}

    static readonly short[] DFA7_eot = {
        -1, -1, -1, -1, -1, -1, -1, 43, 45, 47, 49, -1, -1, -1, -1, -1, 
        50, -1, -1, -1, 37, -1, -1, 37, 37, 37, 37, 37, 37, 37, 37, 37, 
        37, 37, 37, 37, 37, -1, 70, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, 37, 37, 37, 37, 76, 37, 37, 37, 80, 37, 37, 37, 
        37, 37, 37, 87, 37, 37, -1, 90, 37, 37, 37, 37, -1, 37, 96, 37, 
        -1, 37, 37, 37, 37, 37, 37, -1, 37, 37, -1, -1, 106, 37, 108, 37, 
        -1, 110, 37, 37, 37, 37, 37, 37, 37, 118, -1, 119, -1, 120, -1, 
        37, 37, 37, 37, 37, 126, 37, -1, -1, -1, 37, 129, 37, 37, 132, -1, 
        37, 134, -1, 37, 37, -1, 37, -1, 37, 37, 140, 141, 37, -1, -1, 143, 
        -1
        };
    static readonly short[] DFA7_eof = {
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
        };
    static readonly int[] DFA7_min = {
        9, 0, 0, 0, 0, 0, 0, 61, 61, 61, 61, 0, 0, 0, 0, 0, 48, 0, 0, 0, 
        101, 0, 0, 97, 110, 104, 101, 111, 110, 113, 115, 101, 101, 114, 
        121, 97, 114, 0, 46, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 119, 
        108, 111, 108, 48, 101, 116, 105, 48, 100, 117, 99, 115, 108, 111, 
        48, 116, 117, 0, 48, 108, 109, 115, 111, 0, 114, 48, 110, 0, 101, 
        97, 101, 99, 101, 117, 0, 101, 101, 0, 0, 48, 101, 48, 101, 0, 48, 
        114, 108, 110, 101, 99, 112, 84, 48, 0, 48, 0, 48, 0, 98, 115, 100, 
        110, 116, 48, 105, 0, 0, 0, 121, 48, 105, 100, 48, 0, 109, 48, 0, 
        110, 105, 0, 101, 0, 103, 110, 48, 48, 103, 0, 0, 48, 0
        };
    static readonly int[] DFA7_max = {
        125, 0, 0, 0, 0, 0, 0, 62, 61, 61, 61, 0, 0, 0, 0, 0, 57, 0, 0, 
        0, 117, 0, 0, 114, 110, 104, 101, 111, 114, 113, 115, 101, 101, 
        114, 121, 97, 114, 0, 57, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
        119, 108, 111, 108, 122, 101, 116, 105, 122, 100, 117, 99, 115, 
        108, 111, 122, 116, 117, 0, 122, 108, 109, 115, 111, 0, 114, 122, 
        110, 0, 101, 97, 101, 99, 101, 117, 0, 101, 101, 0, 0, 122, 101, 
        122, 101, 0, 122, 114, 108, 110, 101, 99, 112, 84, 122, 0, 122, 
        0, 122, 0, 98, 115, 100, 110, 116, 122, 105, 0, 0, 0, 121, 122, 
        105, 100, 122, 0, 109, 122, 0, 110, 105, 0, 101, 0, 103, 110, 122, 
        122, 103, 0, 0, 122, 0
        };
    static readonly short[] DFA7_accept = {
        -1, 1, 2, 3, 4, 5, 6, -1, -1, -1, -1, 13, 14, 15, 16, 17, -1, 20, 
        21, 22, -1, 25, 27, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, 45, -1, 48, 50, 7, 23, 26, 8, 18, 10, 9, 12, 11, 19, 
        47, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, 46, -1, -1, -1, -1, -1, 29, -1, -1, -1, 34, -1, -1, 
        -1, -1, -1, -1, 41, -1, -1, 24, 45, -1, -1, -1, -1, 32, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, 28, -1, 30, -1, 33, -1, -1, -1, -1, 
        -1, -1, -1, 43, 44, 31, -1, -1, -1, -1, -1, 40, -1, -1, 35, -1, 
        -1, 39, -1, 36, -1, -1, -1, -1, -1, 42, 37, -1, 38
        };
    static readonly short[] DFA7_special = {
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
        };
    
    static readonly short[] dfa7_transition_null = null;

    static readonly short[] dfa7_transition0 = {
    	37, 37, 37, 37, 37, 37, 37, 37, 37, 37, -1, -1, -1, -1, -1, -1, -1, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, -1, -1, -1, -1, 37, -1, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37
    	};
    static readonly short[] dfa7_transition1 = {
    	91
    	};
    static readonly short[] dfa7_transition2 = {
    	51, -1, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38
    	};
    static readonly short[] dfa7_transition3 = {
    	72
    	};
    static readonly short[] dfa7_transition4 = {
    	40, 40, -1, 40, 40, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
    	    -1, -1, -1, -1, -1, -1, 40, 8, -1, -1, -1, 15, 6, 39, 17, 18, 13, 
    	    11, 19, 12, 16, 14, 38, 38, 38, 38, 38, 38, 38, 38, 38, 38, 4, 
    	    -1, 9, 7, 10, 3, -1, 37, 37, 37, 35, 37, 37, 37, 37, 37, 37, 37, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 1, 
    	    -1, 2, -1, 37, -1, 30, 34, 37, 31, 29, 23, 33, 37, 24, 27, 37, 
    	    26, 37, 20, 28, 37, 37, 37, 32, 36, 37, 37, 25, 37, 37, 37, 21, 
    	    5, 22
    	};
    static readonly short[] dfa7_transition5 = {
    	51, 51, 51, 51, 51, 51, 51, 51, 51, 51
    	};
    static readonly short[] dfa7_transition6 = {
    	37, 37, 37, 37, 37, 37, 37, 37, 37, 37, -1, -1, -1, -1, -1, -1, -1, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, -1, -1, -1, -1, 37, -1, 
    	    37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 37, 
    	    37, 37, 37, 75, 37, 37, 37, 37, 37, 37
    	};
    static readonly short[] dfa7_transition7 = {
    	115
    	};
    static readonly short[] dfa7_transition8 = {
    	125
    	};
    static readonly short[] dfa7_transition9 = {
    	59
    	};
    static readonly short[] dfa7_transition10 = {
    	86
    	};
    static readonly short[] dfa7_transition11 = {
    	103
    	};
    static readonly short[] dfa7_transition12 = {
    	116
    	};
    static readonly short[] dfa7_transition13 = {
    	62
    	};
    static readonly short[] dfa7_transition14 = {
    	56
    	};
    static readonly short[] dfa7_transition15 = {
    	84
    	};
    static readonly short[] dfa7_transition16 = {
    	114
    	};
    static readonly short[] dfa7_transition17 = {
    	101
    	};
    static readonly short[] dfa7_transition18 = {
    	131
    	};
    static readonly short[] dfa7_transition19 = {
    	124
    	};
    static readonly short[] dfa7_transition20 = {
    	139
    	};
    static readonly short[] dfa7_transition21 = {
    	57
    	};
    static readonly short[] dfa7_transition22 = {
    	136
    	};
    static readonly short[] dfa7_transition23 = {
    	41, 42
    	};
    static readonly short[] dfa7_transition24 = {
    	142
    	};
    static readonly short[] dfa7_transition25 = {
    	58
    	};
    static readonly short[] dfa7_transition26 = {
    	102
    	};
    static readonly short[] dfa7_transition27 = {
    	85
    	};
    static readonly short[] dfa7_transition28 = {
    	105
    	};
    static readonly short[] dfa7_transition29 = {
    	74
    	};
    static readonly short[] dfa7_transition30 = {
    	93
    	};
    static readonly short[] dfa7_transition31 = {
    	107
    	};
    static readonly short[] dfa7_transition32 = {
    	127
    	};
    static readonly short[] dfa7_transition33 = {
    	117
    	};
    static readonly short[] dfa7_transition34 = {
    	104
    	};
    static readonly short[] dfa7_transition35 = {
    	88
    	};
    static readonly short[] dfa7_transition36 = {
    	137
    	};
    static readonly short[] dfa7_transition37 = {
    	133
    	};
    static readonly short[] dfa7_transition38 = {
    	89
    	};
    static readonly short[] dfa7_transition39 = {
    	67
    	};
    static readonly short[] dfa7_transition40 = {
    	68
    	};
    static readonly short[] dfa7_transition41 = {
    	97
    	};
    static readonly short[] dfa7_transition42 = {
    	66
    	};
    static readonly short[] dfa7_transition43 = {
    	79
    	};
    static readonly short[] dfa7_transition44 = {
    	78
    	};
    static readonly short[] dfa7_transition45 = {
    	65
    	};
    static readonly short[] dfa7_transition46 = {
    	77
    	};
    static readonly short[] dfa7_transition47 = {
    	95
    	};
    static readonly short[] dfa7_transition48 = {
    	109
    	};
    static readonly short[] dfa7_transition49 = {
    	60, -1, -1, -1, 61
    	};
    static readonly short[] dfa7_transition50 = {
    	94
    	};
    static readonly short[] dfa7_transition51 = {
    	44
    	};
    static readonly short[] dfa7_transition52 = {
    	138
    	};
    static readonly short[] dfa7_transition53 = {
    	135
    	};
    static readonly short[] dfa7_transition54 = {
    	130
    	};
    static readonly short[] dfa7_transition55 = {
    	123
    	};
    static readonly short[] dfa7_transition56 = {
    	64
    	};
    static readonly short[] dfa7_transition57 = {
    	113
    	};
    static readonly short[] dfa7_transition58 = {
    	100
    	};
    static readonly short[] dfa7_transition59 = {
    	83
    	};
    static readonly short[] dfa7_transition60 = {
    	63
    	};
    static readonly short[] dfa7_transition61 = {
    	121
    	};
    static readonly short[] dfa7_transition62 = {
    	128
    	};
    static readonly short[] dfa7_transition63 = {
    	98
    	};
    static readonly short[] dfa7_transition64 = {
    	111
    	};
    static readonly short[] dfa7_transition65 = {
    	81
    	};
    static readonly short[] dfa7_transition66 = {
    	55, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 
    	    54
    	};
    static readonly short[] dfa7_transition67 = {
    	112
    	};
    static readonly short[] dfa7_transition68 = {
    	122
    	};
    static readonly short[] dfa7_transition69 = {
    	82
    	};
    static readonly short[] dfa7_transition70 = {
    	99
    	};
    static readonly short[] dfa7_transition71 = {
    	46
    	};
    static readonly short[] dfa7_transition72 = {
    	48
    	};
    static readonly short[] dfa7_transition73 = {
    	52, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 53
    	};
    static readonly short[] dfa7_transition74 = {
    	92
    	};
    static readonly short[] dfa7_transition75 = {
    	73
    	};
    static readonly short[] dfa7_transition76 = {
    	69
    	};
    static readonly short[] dfa7_transition77 = {
    	71
    	};
    
    static readonly short[][] DFA7_transition = {
    	dfa7_transition4,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition23,
    	dfa7_transition51,
    	dfa7_transition71,
    	dfa7_transition72,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition5,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition73,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition66,
    	dfa7_transition14,
    	dfa7_transition21,
    	dfa7_transition25,
    	dfa7_transition9,
    	dfa7_transition49,
    	dfa7_transition13,
    	dfa7_transition60,
    	dfa7_transition56,
    	dfa7_transition45,
    	dfa7_transition42,
    	dfa7_transition39,
    	dfa7_transition40,
    	dfa7_transition76,
    	dfa7_transition_null,
    	dfa7_transition2,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition77,
    	dfa7_transition3,
    	dfa7_transition75,
    	dfa7_transition29,
    	dfa7_transition6,
    	dfa7_transition46,
    	dfa7_transition44,
    	dfa7_transition43,
    	dfa7_transition0,
    	dfa7_transition65,
    	dfa7_transition69,
    	dfa7_transition59,
    	dfa7_transition15,
    	dfa7_transition27,
    	dfa7_transition10,
    	dfa7_transition0,
    	dfa7_transition35,
    	dfa7_transition38,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition1,
    	dfa7_transition74,
    	dfa7_transition30,
    	dfa7_transition50,
    	dfa7_transition_null,
    	dfa7_transition47,
    	dfa7_transition0,
    	dfa7_transition41,
    	dfa7_transition_null,
    	dfa7_transition63,
    	dfa7_transition70,
    	dfa7_transition58,
    	dfa7_transition17,
    	dfa7_transition26,
    	dfa7_transition11,
    	dfa7_transition_null,
    	dfa7_transition34,
    	dfa7_transition28,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition31,
    	dfa7_transition0,
    	dfa7_transition48,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition64,
    	dfa7_transition67,
    	dfa7_transition57,
    	dfa7_transition16,
    	dfa7_transition7,
    	dfa7_transition12,
    	dfa7_transition33,
    	dfa7_transition0,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition_null,
    	dfa7_transition61,
    	dfa7_transition68,
    	dfa7_transition55,
    	dfa7_transition19,
    	dfa7_transition8,
    	dfa7_transition0,
    	dfa7_transition32,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition62,
    	dfa7_transition0,
    	dfa7_transition54,
    	dfa7_transition18,
    	dfa7_transition0,
    	dfa7_transition_null,
    	dfa7_transition37,
    	dfa7_transition0,
    	dfa7_transition_null,
    	dfa7_transition53,
    	dfa7_transition22,
    	dfa7_transition_null,
    	dfa7_transition36,
    	dfa7_transition_null,
    	dfa7_transition52,
    	dfa7_transition20,
    	dfa7_transition0,
    	dfa7_transition0,
    	dfa7_transition24,
    	dfa7_transition_null,
    	dfa7_transition_null,
    	dfa7_transition0,
    	dfa7_transition_null
        };
    
    protected class DFA7 : DFA
    {
        public DFA7(BaseRecognizer recognizer) 
        {
            this.recognizer = recognizer;
            this.decisionNumber = 7;
            this.eot = DFA7_eot;
            this.eof = DFA7_eof;
            this.min = DFA7_min;
            this.max = DFA7_max;
            this.accept     = DFA7_accept;
            this.special    = DFA7_special;
            this.transition = DFA7_transition;
        }
    
        override public string Description
        {
            get { return "1:1: Tokens : ( T15 | T16 | T17 | T18 | T19 | T20 | T21 | T22 | T23 | T24 | T25 | T26 | T27 | T28 | T29 | T30 | T31 | T32 | T33 | T34 | T35 | T36 | T37 | T38 | T39 | T40 | T41 | T42 | T43 | T44 | T45 | T46 | T47 | T48 | T49 | T50 | T51 | T52 | T53 | T54 | T55 | T56 | T57 | T58 | ID | INTEGER | FLOAT | STRING | NULL | WS );"; }
        }
    
    }
    
 
    
}

using System;
using O2.Debugger.Mdbg.Tools.Mdbg;

namespace O2.Debugger.Mdbg.O2Debugger
{
    public class OriginalMDbgMessages
    {
        public static Func<string, bool> internalMDbgMessage;
        public static Func<string, bool> commandExecutionMessage;

        public static bool mdbgLoggingEnabled;

        public static void WriteLine(string message)            
        {
            //if (mdbgLoggingEnabled)
            //    System.Diagnostics.Trace.WriteLine(message);
            if (internalMDbgMessage != null)
                internalMDbgMessage(message);
            //Display("[OriginalMdbb msg]" +  message, MDbgIO.HighlighType.None, 0, 0);
        }

        public static void Display(string text, MDbgIO.HighlighType ht, int highlightStart, int highlighLen)
        {
            if (commandExecutionMessage != null)
            {                
                commandExecutionMessage(text.Trim());
            }


            //System.Diagnostics.Debug.WriteLine("[MDBG] " + text);

            /*
            if (highlighLen == 0 || highlightStart >= text.Length)
            {
                Console.Write(text);
            }
            else
            {
                Console.Write(text.Substring(0, highlightStart));
                ConsoleColor fc = Console.ForegroundColor;
                ConsoleColor bc = Console.BackgroundColor;

                switch (ht)
                {
                    case HighlighType.StatementLocation:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case HighlighType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Debug.Assert(false);
                        throw new InvalidOperationException();
                }
                int l = highlightStart + highlighLen;
                if (l > text.Length)
                {
                    highlighLen = text.Length - highlightStart;
                }
                Console.Write(text.Substring(highlightStart, highlighLen));
                Console.ForegroundColor = fc;
                Console.BackgroundColor = bc;
                if (highlightStart + highlighLen < text.Length)
                {
                    Console.Write(text.Substring(highlightStart + highlighLen));
                }
            }*/
        }
    }
}
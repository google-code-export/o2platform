using System;
using System.Threading;

namespace O2.Views.ASCX.SourceCodeEdit.ScriptSamples
{
    public class For_UnitTest_SimpleCalls
    {
        public static int count;
        public static int sleepValue = 500;
        public static string textToShow = "";

        public static void Main()
        {
            Console.WriteLine("In Simple test");
            while (true)
            {
                Console.WriteLine("\ncalling normal call");
                normalCall();
                Console.WriteLine("calling show text");
                showText();
            }
        }

        public static void showText()
        {
            Console.WriteLine(textToShow);
            Thread.Sleep(sleepValue);
            Console.WriteLine(textToShow);
            Thread.Sleep(sleepValue);
            Console.WriteLine(textToShow);
            Thread.Sleep(sleepValue);
            Console.WriteLine(textToShow);
            Thread.Sleep(sleepValue);
            Console.WriteLine(textToShow);
            Thread.Sleep(sleepValue);
        }

        public static void normalCall()
        {
            textToShow = "... a in normal call " + count++;
        }


        public static void hookCall()
        {
            textToShow = "** in a HOOKed call  ";
        }
    }
}
// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;

namespace O2.UnitTests._testScripts
{
    public class TestScript_MultipleCalls
    {
        public static void Main(string[] args)
        {
            if (args.Length >0)
                Console.WriteLine("This is a " + test(args[0]));
            else
                Console.WriteLine("This is a " + test("no args"));
        }

        private static string test(string args)
        {
            var testContent = anotherMethod(" param "," param 2 " + args);
            var data = testContent + " inside test()";
            return (data + "  ....   out ....");
        }

        private static string anotherMethod(string param1, string param2)
        {
            var joinData = string.Format(" in anotherMethod :{0} + {1}", param1, param2);
            return validate(joinData);
        }

        private static string validate(string dataToValidate)
        {
            return dataToValidate.Replace("'","\\");
        }

    }
}
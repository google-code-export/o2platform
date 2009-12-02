using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2_Scanner_DotNet._TestScripts
{
    public class TestScript_MultipleCalls
    {
        public static void main(string[] args)
        {
            if (args.Length >0)
                Console.WriteLine("This is a " + test(args[0]));
            else
                Console.WriteLine("This is a " + test("no args"));
        }

        private static string test(string args)
        {
 	        var testContent = anotherMethod("param","param2" + args);
            var data = testContent + " inside test()";
            return data += "  ....   out ....";
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

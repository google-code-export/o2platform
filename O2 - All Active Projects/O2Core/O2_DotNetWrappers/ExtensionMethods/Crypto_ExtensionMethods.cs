using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Crypto_ExtensionMethods
    {
        public static Random randomObject = new Random((int)DateTime.Now.Ticks);
         
        public static int random(this int maxValue)
        {
            return randomObject.Next(maxValue);
        }

        // inspired from the accepted answer from http://stackoverflow.com/questions/1122483/c-random-string-generator
        public static string randomString(this int size)
        {
            var random = Crypto_ExtensionMethods.randomObject;

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                var intValue = Convert.ToInt32(Math.Floor(93 * random.NextDouble() + 33));  // gets a ASCII value from 33 till 126		        	
                var ch = Convert.ToChar(intValue);
                stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }

        public static string randomNumbers(this int size)
        {
            var random = Crypto_ExtensionMethods.randomObject;

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                var intValue = Convert.ToInt32(Math.Floor(10 * random.NextDouble() + 48));  // gets a ASCII value from 33 till 126		        	
                var ch = Convert.ToChar(intValue);
                stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }



        public static string randomLetters(this int size)
        {
            var random = Crypto_ExtensionMethods.randomObject;

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                var intValue = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65));  // gets a ASCII value from 33 till 126
                if (1000.random().isEven())			// if it is an even number
                    intValue += 32;				 	// make it lower case
                var ch = Convert.ToChar(intValue);
                stringBuilder.Append(ch);
            }
            return stringBuilder.ToString();
        }
    }
}

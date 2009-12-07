using System;
using System.Collections.Generic;
using System.Text;

namespace MerlinStepLibrary
{
    /// <summary>
    /// A condition evaluated upon an answer to a query
    /// </summary>
    /// <param name="answer"></param>
    /// <returns></returns>
    public delegate bool AnswerCondition (string answer);

    /// <summary>
    /// Pre-built functions to validate answers of Question
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// answer is valid if it is non-Empty
        /// </summary>
        public static bool NonEmpty(string answer)
        {
            bool nullOrEmpty = string.IsNullOrEmpty(answer);
            return !nullOrEmpty;
        }

        /// <summary>
        /// The answer is valid if its length is between the specified minimum and maximum (inclusive)
        /// </summary>
        /// /// <param name="minimum"></param>
        /// /// <param name="maximum"></param>
        public static AnswerCondition Length(int minimum, int maximum)
        {
            AnswerCondition lengthDelegate = (answer) =>
            {
                return !(answer == null || answer.Length < minimum || answer.Length > maximum);
            };
            return lengthDelegate;
        }

        /// <summary>
        /// The answer is valid if its length is greater than or equal to the specified minimum.
        /// </summary>
        /// <param name="minimum"></param>
        /// <returns></returns>
        public static AnswerCondition MinLength(int minimum)
        {
            return answer => answer != null && answer.Length >= minimum;
        }

        /// <summary>
        /// The answer is valid if it is numeric
        /// </summary>
        public static bool IsNumeric(string answer)
        {
            if(answer == null)
            {
                return false;
            }
            try
            {
                Convert.ToInt32(answer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The answer is valid if it is alphabetic
        /// </summary>
        public static bool IsAlphabetic(string answer)
        {
            if (answer == null)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex regexAlphabetic = new System.Text.RegularExpressions.Regex("[^a-zA-Z]");
            return !regexAlphabetic.IsMatch(answer);
        }

        /// <summary>
        /// The answer is valid if it is alphanumeric
        /// </summary>
        public static bool IsAlphaNumeric(string answer)
        {
            if (answer == null)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex regexAlphaNum = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
            return !regexAlphaNum.IsMatch(answer);
        }

    }

}

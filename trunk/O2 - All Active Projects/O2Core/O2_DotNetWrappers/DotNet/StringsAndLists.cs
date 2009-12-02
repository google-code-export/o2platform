using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace O2.DotNetWrappers.DotNet
{
    public class StringsAndLists
    {
        public static List<String> fromStringList_getListWithUniqueItems(List<String> lsListToFilter)
        {
            lsListToFilter.Sort();
            var lsNewList = new List<string>();
            foreach (String sItem in lsListToFilter)
                if (false == lsNewList.Contains(sItem))
                    lsNewList.Add(sItem);
            lsNewList.Sort();
            return lsNewList;
        }

        public static String fromStringList_getText(List<String> lsListToProcess)
        {
            var sbText = new StringBuilder();
            //var lsNewList = new List<string>();
            foreach (String sItem in lsListToProcess)
                sbText.AppendLine(sItem);
            return sbText.ToString();
        }

        public static List<String> fromTextGetLines(String sText)
        {
            String[] asSplittedLines = sText.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            return new List<String>(asSplittedLines);
        }

        public static List<String> getStringListFromList(object listToConvert)
        {
            var results = new List<String>();

            foreach (object item in (IEnumerable) listToConvert)
            {
                results.Add(item.ToString());
            }
            return results;
        }

        public static bool notNull(Object oObjectToCheck, String sObjectType)
        {
            if (oObjectToCheck == null)
            {
                DI.log.debug("Variable of type {0} was not null!", sObjectType);
                return false;
            }
            return true;
        }

        public static void showListContents(IEnumerable list)
        {
            DI.log.debug("Showing contents of list of type: {0}\n", list.GetType());
            int itemCount = 0;
            foreach (object item in list)
                DI.log.info("      [{0}]   {1}", itemCount++, item.ToString());

            DI.log.info("");
        }

        public static string addSpacesOnUpper(string stringToModify)
        {
            var modifiedString = "";
            foreach (var letter in stringToModify)
                if (Char.IsUpper(letter))
                    modifiedString += " " + letter;
                else
                    modifiedString += letter;
            modifiedString = modifiedString.Trim();
            return modifiedString;
        }
    }
}
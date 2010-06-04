namespace Amazon.SimpleDB.Util
{
    using Amazon.SimpleDB.Model;
    using Amazon.Util;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public static class AmazonSimpleDBUtil
    {
        private static string base64Str = "base64";
        private static string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffzzzz";

        public static void DecodeAttribute(Amazon.SimpleDB.Model.Attribute inputAttribute)
        {
            if (inputAttribute == null)
            {
                throw new ArgumentNullException("inputAttribute", "The Attribute passed in was null");
            }
            string nameEncoding = inputAttribute.NameEncoding;
            if ((nameEncoding != null) && (string.Compare(nameEncoding, base64Str, true) == 0))
            {
                inputAttribute.Name = DecodeBase64String(inputAttribute.Name);
                inputAttribute.NameEncoding = "";
            }
            nameEncoding = inputAttribute.ValueEncoding;
            if ((nameEncoding != null) && (string.Compare(nameEncoding, base64Str, true) == 0))
            {
                inputAttribute.Value = DecodeBase64String(inputAttribute.Value);
                inputAttribute.ValueEncoding = "";
            }
        }

        public static void DecodeAttributes(List<Amazon.SimpleDB.Model.Attribute> attributes)
        {
            if ((attributes != null) && (attributes.Count > 0))
            {
                foreach (Amazon.SimpleDB.Model.Attribute attribute in attributes)
                {
                    DecodeAttribute(attribute);
                }
            }
        }

        public static string DecodeBase64String(string encoded)
        {
            if (encoded == null)
            {
                throw new ArgumentNullException("encoded", "The Encoded String passed in was null");
            }
            byte[] bytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(bytes);
        }

        public static DateTime DecodeDate(string value)
        {
            return DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
        }

        public static void DecodeItem(Item inputItem)
        {
            if (inputItem == null)
            {
                throw new ArgumentNullException("inputItem", "The Item passed in was null");
            }
            string nameEncoding = inputItem.NameEncoding;
            if ((nameEncoding != null) && (string.Compare(nameEncoding, base64Str, true) == 0))
            {
                inputItem.Name = DecodeBase64String(inputItem.Name);
                inputItem.NameEncoding = "";
            }
            DecodeAttributes(inputItem.Attribute);
        }

        public static void DecodeItems(List<Item> inputItems)
        {
            if ((inputItems != null) && (inputItems.Count > 0))
            {
                foreach (Item item in inputItems)
                {
                    DecodeItem(item);
                }
            }
        }

        public static float DecodeRealNumberRangeFloat(string value, int maxDigitsRight, int offsetValue)
        {
            return (float) ((((double) long.Parse(value)) / Math.Pow(10.0, (double) maxDigitsRight)) - offsetValue);
        }

        public static int DecodeRealNumberRangeInt(string value, int offsetValue)
        {
            return (((int) long.Parse(value)) - offsetValue);
        }

        public static float DecodeZeroPaddingFloat(string value)
        {
            return float.Parse(value);
        }

        public static int DecodeZeroPaddingInt(string value)
        {
            return int.Parse(value);
        }

        public static string EncodeDate(DateTime date)
        {
            return date.ToString(dateFormat);
        }

        public static string EncodeRealNumberRange(int number, int maxNumDigits, int offsetValue)
        {
            int num = number + offsetValue;
            return num.ToString().PadLeft(maxNumDigits, '0');
        }

        public static string EncodeRealNumberRange(float number, int maxDigitsLeft, int maxDigitsRight, int offsetValue)
        {
            long num = (long) Math.Pow(10.0, (double) maxDigitsRight);
            long num2 = (long) Math.Round((double) ((number + offsetValue) * num));
            return num2.ToString().PadLeft(maxDigitsLeft + maxDigitsRight, '0');
        }

        public static string EncodeZeroPadding(int number, int maxNumDigits)
        {
            return number.ToString().PadLeft(maxNumDigits, '0');
        }

        public static string EncodeZeroPadding(float number, int maxNumDigits)
        {
            string str = number.ToString();
            int index = str.IndexOf('.');
            if (index == -1)
            {
                return str.PadLeft(maxNumDigits, '0');
            }
            return str.PadLeft(maxNumDigits + (str.Length - index), '0');
        }

        public static string FormattedCurrentTimestamp
        {
            get
            {
                return AWSSDKUtils.FormattedCurrentTimestampISO8601;
            }
        }
    }
}


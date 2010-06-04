namespace Amazon.CloudFront.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class UrlTrustedSigners
    {
        private List<string> awsAccounts;
        private bool enableSelf;

        internal bool IsSetAwsAccounts()
        {
            return ((this.AwsAccountNumbers != null) && (this.AwsAccountNumbers.Count > 0));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            if (this.EnableSelf)
            {
                builder.Append("<Self/>");
            }
            if (this.IsSetAwsAccounts())
            {
                foreach (string str in this.AwsAccountNumbers)
                {
                    builder.Append("<AwsAccountNumber>" + str + "</AwsAccountNumber>");
                }
            }
            return builder.ToString();
        }

        public UrlTrustedSigners WithAwsAccountNumbers(params string[] awsAccounts)
        {
            foreach (string str in awsAccounts)
            {
                this.AwsAccountNumbers.Add(str);
            }
            return this;
        }

        public UrlTrustedSigners WithEnableSelf(bool enableSelf)
        {
            this.enableSelf = enableSelf;
            return this;
        }

        [XmlElement(ElementName="AwsAccountNumber")]
        public List<string> AwsAccountNumbers
        {
            get
            {
                if (this.awsAccounts == null)
                {
                    this.awsAccounts = new List<string>();
                }
                return this.awsAccounts;
            }
        }

        [XmlElement(ElementName="Self")]
        public bool EnableSelf
        {
            get
            {
                return this.enableSelf;
            }
            set
            {
                this.enableSelf = value;
            }
        }
    }
}


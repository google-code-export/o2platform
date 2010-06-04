namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpcRequest
    {
        private string cidrBlockField;

        public bool IsSetCidrBlock()
        {
            return (this.cidrBlockField != null);
        }

        public CreateVpcRequest WithCidrBlock(string cidrBlock)
        {
            this.cidrBlockField = cidrBlock;
            return this;
        }

        [XmlElement(ElementName="CidrBlock")]
        public string CidrBlock
        {
            get
            {
                return this.cidrBlockField;
            }
            set
            {
                this.cidrBlockField = value;
            }
        }
    }
}


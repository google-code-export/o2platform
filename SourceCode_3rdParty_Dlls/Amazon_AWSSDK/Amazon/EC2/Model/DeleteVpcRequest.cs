namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DeleteVpcRequest
    {
        private string vpcIdField;

        public bool IsSetVpcId()
        {
            return (this.vpcIdField != null);
        }

        public DeleteVpcRequest WithVpcId(string vpcId)
        {
            this.vpcIdField = vpcId;
            return this;
        }

        [XmlElement(ElementName="VpcId")]
        public string VpcId
        {
            get
            {
                return this.vpcIdField;
            }
            set
            {
                this.vpcIdField = value;
            }
        }
    }
}


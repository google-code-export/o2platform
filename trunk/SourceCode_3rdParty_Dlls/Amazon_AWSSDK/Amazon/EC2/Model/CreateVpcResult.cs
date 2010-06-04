namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class CreateVpcResult
    {
        private Amazon.EC2.Model.Vpc vpcField;

        public bool IsSetVpc()
        {
            return (this.vpcField != null);
        }

        public CreateVpcResult WithVpc(Amazon.EC2.Model.Vpc vpc)
        {
            this.vpcField = vpc;
            return this;
        }

        [XmlElement(ElementName="Vpc")]
        public Amazon.EC2.Model.Vpc Vpc
        {
            get
            {
                return this.vpcField;
            }
            set
            {
                this.vpcField = value;
            }
        }
    }
}


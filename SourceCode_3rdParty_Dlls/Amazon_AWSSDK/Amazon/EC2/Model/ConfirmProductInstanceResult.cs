namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ConfirmProductInstanceResult
    {
        private string ownerIdField;

        public bool IsSetOwnerId()
        {
            return (this.ownerIdField != null);
        }

        public ConfirmProductInstanceResult WithOwnerId(string ownerId)
        {
            this.ownerIdField = ownerId;
            return this;
        }

        [XmlElement(ElementName="OwnerId")]
        public string OwnerId
        {
            get
            {
                return this.ownerIdField;
            }
            set
            {
                this.ownerIdField = value;
            }
        }
    }
}


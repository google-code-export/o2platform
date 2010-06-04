namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class ResetSnapshotAttributeRequest
    {
        private string attributeField;
        private string snapshotIdField;

        public bool IsSetAttribute()
        {
            return (this.attributeField != null);
        }

        public bool IsSetSnapshotId()
        {
            return (this.snapshotIdField != null);
        }

        public ResetSnapshotAttributeRequest WithAttribute(string attribute)
        {
            this.attributeField = attribute;
            return this;
        }

        public ResetSnapshotAttributeRequest WithSnapshotId(string snapshotId)
        {
            this.snapshotIdField = snapshotId;
            return this;
        }

        [XmlElement(ElementName="Attribute")]
        public string Attribute
        {
            get
            {
                return this.attributeField;
            }
            set
            {
                this.attributeField = value;
            }
        }

        [XmlElement(ElementName="SnapshotId")]
        public string SnapshotId
        {
            get
            {
                return this.snapshotIdField;
            }
            set
            {
                this.snapshotIdField = value;
            }
        }
    }
}


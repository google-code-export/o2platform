namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeSnapshotAttributeResult
    {
        private Amazon.EC2.Model.SnapshotAttribute snapshotAttributeField;

        public bool IsSetSnapshotAttribute()
        {
            return (this.snapshotAttributeField != null);
        }

        public DescribeSnapshotAttributeResult WithSnapshotAttribute(Amazon.EC2.Model.SnapshotAttribute snapshotAttribute)
        {
            this.snapshotAttributeField = snapshotAttribute;
            return this;
        }

        [XmlElement(ElementName="SnapshotAttribute")]
        public Amazon.EC2.Model.SnapshotAttribute SnapshotAttribute
        {
            get
            {
                return this.snapshotAttributeField;
            }
            set
            {
                this.snapshotAttributeField = value;
            }
        }
    }
}


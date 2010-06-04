namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeReservedInstancesRequest
    {
        private List<string> reservedInstancesIdField;

        public bool IsSetReservedInstancesId()
        {
            return (this.ReservedInstancesId.Count > 0);
        }

        public DescribeReservedInstancesRequest WithReservedInstancesId(params string[] list)
        {
            foreach (string str in list)
            {
                this.ReservedInstancesId.Add(str);
            }
            return this;
        }

        [XmlElement(ElementName="ReservedInstancesId")]
        public List<string> ReservedInstancesId
        {
            get
            {
                if (this.reservedInstancesIdField == null)
                {
                    this.reservedInstancesIdField = new List<string>();
                }
                return this.reservedInstancesIdField;
            }
            set
            {
                this.reservedInstancesIdField = value;
            }
        }
    }
}


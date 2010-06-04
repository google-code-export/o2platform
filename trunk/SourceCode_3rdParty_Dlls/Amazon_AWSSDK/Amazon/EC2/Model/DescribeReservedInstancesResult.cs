namespace Amazon.EC2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class DescribeReservedInstancesResult
    {
        private List<Amazon.EC2.Model.ReservedInstances> reservedInstancesField;

        public bool IsSetReservedInstances()
        {
            return (this.ReservedInstances.Count > 0);
        }

        public DescribeReservedInstancesResult WithReservedInstances(params Amazon.EC2.Model.ReservedInstances[] list)
        {
            foreach (Amazon.EC2.Model.ReservedInstances instances in list)
            {
                this.ReservedInstances.Add(instances);
            }
            return this;
        }

        [XmlElement(ElementName="ReservedInstances")]
        public List<Amazon.EC2.Model.ReservedInstances> ReservedInstances
        {
            get
            {
                if (this.reservedInstancesField == null)
                {
                    this.reservedInstancesField = new List<Amazon.EC2.Model.ReservedInstances>();
                }
                return this.reservedInstancesField;
            }
            set
            {
                this.reservedInstancesField = value;
            }
        }
    }
}


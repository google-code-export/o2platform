namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class GetPasswordDataResult
    {
        private Amazon.EC2.Model.PasswordData passwordDataField;

        public bool IsSetPasswordData()
        {
            return (this.passwordDataField != null);
        }

        public GetPasswordDataResult WithPasswordData(Amazon.EC2.Model.PasswordData passwordData)
        {
            this.passwordDataField = passwordData;
            return this;
        }

        [XmlElement(ElementName="PasswordData")]
        public Amazon.EC2.Model.PasswordData PasswordData
        {
            get
            {
                return this.passwordDataField;
            }
            set
            {
                this.passwordDataField = value;
            }
        }
    }
}


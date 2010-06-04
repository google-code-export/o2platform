namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class KeyPair
    {
        private string keyFingerprintField;
        private string keyMaterialField;
        private string keyNameField;

        public bool IsSetKeyFingerprint()
        {
            return (this.keyFingerprintField != null);
        }

        public bool IsSetKeyMaterial()
        {
            return (this.keyMaterialField != null);
        }

        public bool IsSetKeyName()
        {
            return (this.keyNameField != null);
        }

        public KeyPair WithKeyFingerprint(string keyFingerprint)
        {
            this.keyFingerprintField = keyFingerprint;
            return this;
        }

        public KeyPair WithKeyMaterial(string keyMaterial)
        {
            this.keyMaterialField = keyMaterial;
            return this;
        }

        public KeyPair WithKeyName(string keyName)
        {
            this.keyNameField = keyName;
            return this;
        }

        [XmlElement(ElementName="KeyFingerprint")]
        public string KeyFingerprint
        {
            get
            {
                return this.keyFingerprintField;
            }
            set
            {
                this.keyFingerprintField = value;
            }
        }

        [XmlElement(ElementName="KeyMaterial")]
        public string KeyMaterial
        {
            get
            {
                return this.keyMaterialField;
            }
            set
            {
                this.keyMaterialField = value;
            }
        }

        [XmlElement(ElementName="KeyName")]
        public string KeyName
        {
            get
            {
                return this.keyNameField;
            }
            set
            {
                this.keyNameField = value;
            }
        }
    }
}


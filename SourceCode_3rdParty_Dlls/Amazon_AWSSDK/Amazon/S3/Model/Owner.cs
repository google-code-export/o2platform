namespace Amazon.S3.Model
{
    using System;
    using System.Xml.Serialization;

    public class Owner
    {
        private string displayName;
        private string id;

        public Owner WithDisplayName(string displayName)
        {
            this.displayName = displayName;
            return this;
        }

        public Owner WithId(string id)
        {
            this.id = id;
            return this;
        }

        [XmlElement(ElementName="DisplayName")]
        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        [XmlElement(ElementName="Id")]
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}


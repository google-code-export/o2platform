namespace Amazon.SimpleNotificationService.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://sns.amazonaws.com/doc/2010-03-31/", IsNullable=false)]
    public class CreateTopicRequest
    {
        private string nameField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public CreateTopicRequest WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
}


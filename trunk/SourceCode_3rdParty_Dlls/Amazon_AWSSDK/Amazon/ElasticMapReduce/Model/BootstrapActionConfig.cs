namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class BootstrapActionConfig
    {
        private string nameField;
        private ScriptBootstrapActionConfig scriptBootstrapActionField;

        public bool IsSetName()
        {
            return (this.nameField != null);
        }

        public bool IsSetScriptBootstrapAction()
        {
            return (this.scriptBootstrapActionField != null);
        }

        public BootstrapActionConfig WithName(string name)
        {
            this.nameField = name;
            return this;
        }

        public BootstrapActionConfig WithScriptBootstrapAction(ScriptBootstrapActionConfig scriptBootstrapAction)
        {
            this.scriptBootstrapActionField = scriptBootstrapAction;
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

        [XmlElement(ElementName="ScriptBootstrapAction")]
        public ScriptBootstrapActionConfig ScriptBootstrapAction
        {
            get
            {
                return this.scriptBootstrapActionField;
            }
            set
            {
                this.scriptBootstrapActionField = value;
            }
        }
    }
}


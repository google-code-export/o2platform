namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class ScriptBootstrapActionConfig
    {
        private List<string> argsField;
        private string pathField;

        public bool IsSetArgs()
        {
            return (this.Args.Count > 0);
        }

        public bool IsSetPath()
        {
            return (this.pathField != null);
        }

        public ScriptBootstrapActionConfig WithArgs(params string[] list)
        {
            foreach (string str in list)
            {
                this.Args.Add(str);
            }
            return this;
        }

        public ScriptBootstrapActionConfig WithPath(string path)
        {
            this.pathField = path;
            return this;
        }

        [XmlElement(ElementName="Args")]
        public List<string> Args
        {
            get
            {
                if (this.argsField == null)
                {
                    this.argsField = new List<string>();
                }
                return this.argsField;
            }
            set
            {
                this.argsField = value;
            }
        }

        [XmlElement(ElementName="Path")]
        public string Path
        {
            get
            {
                return this.pathField;
            }
            set
            {
                this.pathField = value;
            }
        }
    }
}


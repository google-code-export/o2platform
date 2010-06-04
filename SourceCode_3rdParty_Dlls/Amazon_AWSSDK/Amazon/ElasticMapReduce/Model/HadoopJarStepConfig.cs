namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class HadoopJarStepConfig
    {
        private List<string> argsField;
        private string jarField;
        private string mainClassField;
        private List<KeyValue> propertiesField;

        public bool IsSetArgs()
        {
            return (this.Args.Count > 0);
        }

        public bool IsSetJar()
        {
            return (this.jarField != null);
        }

        public bool IsSetMainClass()
        {
            return (this.mainClassField != null);
        }

        public bool IsSetProperties()
        {
            return (this.Properties.Count > 0);
        }

        public HadoopJarStepConfig WithArgs(params string[] list)
        {
            foreach (string str in list)
            {
                this.Args.Add(str);
            }
            return this;
        }

        public HadoopJarStepConfig WithJar(string jar)
        {
            this.jarField = jar;
            return this;
        }

        public HadoopJarStepConfig WithMainClass(string mainClass)
        {
            this.mainClassField = mainClass;
            return this;
        }

        public HadoopJarStepConfig WithProperties(params KeyValue[] list)
        {
            foreach (KeyValue value2 in list)
            {
                this.Properties.Add(value2);
            }
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

        [XmlElement(ElementName="Jar")]
        public string Jar
        {
            get
            {
                return this.jarField;
            }
            set
            {
                this.jarField = value;
            }
        }

        [XmlElement(ElementName="MainClass")]
        public string MainClass
        {
            get
            {
                return this.mainClassField;
            }
            set
            {
                this.mainClassField = value;
            }
        }

        [XmlElement(ElementName="Properties")]
        public List<KeyValue> Properties
        {
            get
            {
                if (this.propertiesField == null)
                {
                    this.propertiesField = new List<KeyValue>();
                }
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }
    }
}


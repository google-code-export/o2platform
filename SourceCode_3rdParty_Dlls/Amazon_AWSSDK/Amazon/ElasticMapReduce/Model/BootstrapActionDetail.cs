namespace Amazon.ElasticMapReduce.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://elasticmapreduce.amazonaws.com/doc/2009-03-31", IsNullable=false)]
    public class BootstrapActionDetail
    {
        private Amazon.ElasticMapReduce.Model.BootstrapActionConfig bootstrapActionConfigField;

        public bool IsSetBootstrapActionConfig()
        {
            return (this.bootstrapActionConfigField != null);
        }

        public BootstrapActionDetail WithBootstrapActionConfig(Amazon.ElasticMapReduce.Model.BootstrapActionConfig bootstrapActionConfig)
        {
            this.bootstrapActionConfigField = bootstrapActionConfig;
            return this;
        }

        [XmlElement(ElementName="BootstrapActionConfig")]
        public Amazon.ElasticMapReduce.Model.BootstrapActionConfig BootstrapActionConfig
        {
            get
            {
                return this.bootstrapActionConfigField;
            }
            set
            {
                this.bootstrapActionConfigField = value;
            }
        }
    }
}


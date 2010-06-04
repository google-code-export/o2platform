namespace Amazon.EC2.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://ec2.amazonaws.com/doc/2009-11-30/", IsNullable=false)]
    public class GetConsoleOutputResult
    {
        private Amazon.EC2.Model.ConsoleOutput consoleOutputField;

        public bool IsSetConsoleOutput()
        {
            return (this.consoleOutputField != null);
        }

        public GetConsoleOutputResult WithConsoleOutput(Amazon.EC2.Model.ConsoleOutput consoleOutput)
        {
            this.consoleOutputField = consoleOutput;
            return this;
        }

        [XmlElement(ElementName="ConsoleOutput")]
        public Amazon.EC2.Model.ConsoleOutput ConsoleOutput
        {
            get
            {
                return this.consoleOutputField;
            }
            set
            {
                this.consoleOutputField = value;
            }
        }
    }
}


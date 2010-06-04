namespace Amazon.AutoScaling.Model
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://autoscaling.amazonaws.com/doc/2009-05-15/", IsNullable=false)]
    public class DeleteLaunchConfigurationRequest
    {
        private string launchConfigurationNameField;

        public bool IsSetLaunchConfigurationName()
        {
            return (this.launchConfigurationNameField != null);
        }

        public DeleteLaunchConfigurationRequest WithLaunchConfigurationName(string launchConfigurationName)
        {
            this.launchConfigurationNameField = launchConfigurationName;
            return this;
        }

        [XmlElement(ElementName="LaunchConfigurationName")]
        public string LaunchConfigurationName
        {
            get
            {
                return this.launchConfigurationNameField;
            }
            set
            {
                this.launchConfigurationNameField = value;
            }
        }
    }
}


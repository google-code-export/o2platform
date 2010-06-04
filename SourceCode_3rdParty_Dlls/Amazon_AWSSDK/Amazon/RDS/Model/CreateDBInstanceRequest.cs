namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class CreateDBInstanceRequest
    {
        private decimal? allocatedStorageField;
        private string availabilityZoneField;
        private decimal? backupRetentionPeriodField;
        private string DBInstanceClassField;
        private string DBInstanceIdentifierField;
        private string DBNameField;
        private string DBParameterGroupNameField;
        private List<string> DBSecurityGroupsField;
        private string engineField;
        private string masterUsernameField;
        private string masterUserPasswordField;
        private decimal? portField;
        private string preferredBackupWindowField;
        private string preferredMaintenanceWindowField;

        public bool IsSetAllocatedStorage()
        {
            return this.allocatedStorageField.HasValue;
        }

        public bool IsSetAvailabilityZone()
        {
            return (this.availabilityZoneField != null);
        }

        public bool IsSetBackupRetentionPeriod()
        {
            return this.backupRetentionPeriodField.HasValue;
        }

        public bool IsSetDBInstanceClass()
        {
            return (this.DBInstanceClassField != null);
        }

        public bool IsSetDBInstanceIdentifier()
        {
            return (this.DBInstanceIdentifierField != null);
        }

        public bool IsSetDBName()
        {
            return (this.DBNameField != null);
        }

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetDBSecurityGroups()
        {
            return (this.DBSecurityGroups.Count > 0);
        }

        public bool IsSetEngine()
        {
            return (this.engineField != null);
        }

        public bool IsSetMasterUsername()
        {
            return (this.masterUsernameField != null);
        }

        public bool IsSetMasterUserPassword()
        {
            return (this.masterUserPasswordField != null);
        }

        public bool IsSetPort()
        {
            return this.portField.HasValue;
        }

        public bool IsSetPreferredBackupWindow()
        {
            return (this.preferredBackupWindowField != null);
        }

        public bool IsSetPreferredMaintenanceWindow()
        {
            return (this.preferredMaintenanceWindowField != null);
        }

        public CreateDBInstanceRequest WithAllocatedStorage(decimal allocatedStorage)
        {
            this.allocatedStorageField = new decimal?(allocatedStorage);
            return this;
        }

        public CreateDBInstanceRequest WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public CreateDBInstanceRequest WithBackupRetentionPeriod(decimal backupRetentionPeriod)
        {
            this.backupRetentionPeriodField = new decimal?(backupRetentionPeriod);
            return this;
        }

        public CreateDBInstanceRequest WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public CreateDBInstanceRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public CreateDBInstanceRequest WithDBName(string DBName)
        {
            this.DBNameField = DBName;
            return this;
        }

        public CreateDBInstanceRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public CreateDBInstanceRequest WithDBSecurityGroups(params string[] list)
        {
            foreach (string str in list)
            {
                this.DBSecurityGroups.Add(str);
            }
            return this;
        }

        public CreateDBInstanceRequest WithEngine(string engine)
        {
            this.engineField = engine;
            return this;
        }

        public CreateDBInstanceRequest WithMasterUsername(string masterUsername)
        {
            this.masterUsernameField = masterUsername;
            return this;
        }

        public CreateDBInstanceRequest WithMasterUserPassword(string masterUserPassword)
        {
            this.masterUserPasswordField = masterUserPassword;
            return this;
        }

        public CreateDBInstanceRequest WithPort(decimal port)
        {
            this.portField = new decimal?(port);
            return this;
        }

        public CreateDBInstanceRequest WithPreferredBackupWindow(string preferredBackupWindow)
        {
            this.preferredBackupWindowField = preferredBackupWindow;
            return this;
        }

        public CreateDBInstanceRequest WithPreferredMaintenanceWindow(string preferredMaintenanceWindow)
        {
            this.preferredMaintenanceWindowField = preferredMaintenanceWindow;
            return this;
        }

        [XmlElement(ElementName="AllocatedStorage")]
        public decimal AllocatedStorage
        {
            get
            {
                return this.allocatedStorageField.GetValueOrDefault();
            }
            set
            {
                this.allocatedStorageField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="AvailabilityZone")]
        public string AvailabilityZone
        {
            get
            {
                return this.availabilityZoneField;
            }
            set
            {
                this.availabilityZoneField = value;
            }
        }

        [XmlElement(ElementName="BackupRetentionPeriod")]
        public decimal BackupRetentionPeriod
        {
            get
            {
                return this.backupRetentionPeriodField.GetValueOrDefault();
            }
            set
            {
                this.backupRetentionPeriodField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="DBInstanceClass")]
        public string DBInstanceClass
        {
            get
            {
                return this.DBInstanceClassField;
            }
            set
            {
                this.DBInstanceClassField = value;
            }
        }

        [XmlElement(ElementName="DBInstanceIdentifier")]
        public string DBInstanceIdentifier
        {
            get
            {
                return this.DBInstanceIdentifierField;
            }
            set
            {
                this.DBInstanceIdentifierField = value;
            }
        }

        [XmlElement(ElementName="DBName")]
        public string DBName
        {
            get
            {
                return this.DBNameField;
            }
            set
            {
                this.DBNameField = value;
            }
        }

        [XmlElement(ElementName="DBParameterGroupName")]
        public string DBParameterGroupName
        {
            get
            {
                return this.DBParameterGroupNameField;
            }
            set
            {
                this.DBParameterGroupNameField = value;
            }
        }

        [XmlElement(ElementName="DBSecurityGroups")]
        public List<string> DBSecurityGroups
        {
            get
            {
                if (this.DBSecurityGroupsField == null)
                {
                    this.DBSecurityGroupsField = new List<string>();
                }
                return this.DBSecurityGroupsField;
            }
            set
            {
                this.DBSecurityGroupsField = value;
            }
        }

        [XmlElement(ElementName="Engine")]
        public string Engine
        {
            get
            {
                return this.engineField;
            }
            set
            {
                this.engineField = value;
            }
        }

        [XmlElement(ElementName="MasterUsername")]
        public string MasterUsername
        {
            get
            {
                return this.masterUsernameField;
            }
            set
            {
                this.masterUsernameField = value;
            }
        }

        [XmlElement(ElementName="MasterUserPassword")]
        public string MasterUserPassword
        {
            get
            {
                return this.masterUserPasswordField;
            }
            set
            {
                this.masterUserPasswordField = value;
            }
        }

        [XmlElement(ElementName="Port")]
        public decimal Port
        {
            get
            {
                return this.portField.GetValueOrDefault();
            }
            set
            {
                this.portField = new decimal?(value);
            }
        }

        [XmlElement(ElementName="PreferredBackupWindow")]
        public string PreferredBackupWindow
        {
            get
            {
                return this.preferredBackupWindowField;
            }
            set
            {
                this.preferredBackupWindowField = value;
            }
        }

        [XmlElement(ElementName="PreferredMaintenanceWindow")]
        public string PreferredMaintenanceWindow
        {
            get
            {
                return this.preferredMaintenanceWindowField;
            }
            set
            {
                this.preferredMaintenanceWindowField = value;
            }
        }
    }
}


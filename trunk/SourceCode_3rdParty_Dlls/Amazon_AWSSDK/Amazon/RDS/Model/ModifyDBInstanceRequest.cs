namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class ModifyDBInstanceRequest
    {
        private decimal? allocatedStorageField;
        private bool? applyImmediatelyField;
        private decimal? backupRetentionPeriodField;
        private string DBInstanceClassField;
        private string DBInstanceIdentifierField;
        private string DBParameterGroupNameField;
        private List<string> DBSecurityGroupsField;
        private string masterUserPasswordField;
        private string preferredBackupWindowField;
        private string preferredMaintenanceWindowField;

        public bool IsSetAllocatedStorage()
        {
            return this.allocatedStorageField.HasValue;
        }

        public bool IsSetApplyImmediately()
        {
            return this.applyImmediatelyField.HasValue;
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

        public bool IsSetDBParameterGroupName()
        {
            return (this.DBParameterGroupNameField != null);
        }

        public bool IsSetDBSecurityGroups()
        {
            return (this.DBSecurityGroups.Count > 0);
        }

        public bool IsSetMasterUserPassword()
        {
            return (this.masterUserPasswordField != null);
        }

        public bool IsSetPreferredBackupWindow()
        {
            return (this.preferredBackupWindowField != null);
        }

        public bool IsSetPreferredMaintenanceWindow()
        {
            return (this.preferredMaintenanceWindowField != null);
        }

        public ModifyDBInstanceRequest WithAllocatedStorage(decimal allocatedStorage)
        {
            this.allocatedStorageField = new decimal?(allocatedStorage);
            return this;
        }

        public ModifyDBInstanceRequest WithApplyImmediately(bool applyImmediately)
        {
            this.applyImmediatelyField = new bool?(applyImmediately);
            return this;
        }

        public ModifyDBInstanceRequest WithBackupRetentionPeriod(decimal backupRetentionPeriod)
        {
            this.backupRetentionPeriodField = new decimal?(backupRetentionPeriod);
            return this;
        }

        public ModifyDBInstanceRequest WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public ModifyDBInstanceRequest WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public ModifyDBInstanceRequest WithDBParameterGroupName(string DBParameterGroupName)
        {
            this.DBParameterGroupNameField = DBParameterGroupName;
            return this;
        }

        public ModifyDBInstanceRequest WithDBSecurityGroups(params string[] list)
        {
            foreach (string str in list)
            {
                this.DBSecurityGroups.Add(str);
            }
            return this;
        }

        public ModifyDBInstanceRequest WithMasterUserPassword(string masterUserPassword)
        {
            this.masterUserPasswordField = masterUserPassword;
            return this;
        }

        public ModifyDBInstanceRequest WithPreferredBackupWindow(string preferredBackupWindow)
        {
            this.preferredBackupWindowField = preferredBackupWindow;
            return this;
        }

        public ModifyDBInstanceRequest WithPreferredMaintenanceWindow(string preferredMaintenanceWindow)
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

        [XmlElement(ElementName="ApplyImmediately")]
        public bool ApplyImmediately
        {
            get
            {
                return this.applyImmediatelyField.GetValueOrDefault();
            }
            set
            {
                this.applyImmediatelyField = new bool?(value);
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


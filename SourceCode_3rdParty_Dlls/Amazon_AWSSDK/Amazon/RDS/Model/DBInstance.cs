namespace Amazon.RDS.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(Namespace="http://rds.amazonaws.com/admin/2009-10-16/", IsNullable=false)]
    public class DBInstance
    {
        private decimal? allocatedStorageField;
        private string availabilityZoneField;
        private decimal? backupRetentionPeriodField;
        private string DBInstanceClassField;
        private string DBInstanceIdentifierField;
        private string DBInstanceStatusField;
        private string DBNameField;
        private List<DBParameterGroupStatus> DBParameterGroupField;
        private List<DBSecurityGroupMembership> DBSecurityGroupField;
        private Amazon.RDS.Model.Endpoint endpointField;
        private string engineField;
        private DateTime? instanceCreateTimeField;
        private DateTime? latestRestorableTimeField;
        private string masterUsernameField;
        private Amazon.RDS.Model.PendingModifiedValues pendingModifiedValuesField;
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

        public bool IsSetDBInstanceStatus()
        {
            return (this.DBInstanceStatusField != null);
        }

        public bool IsSetDBName()
        {
            return (this.DBNameField != null);
        }

        public bool IsSetDBParameterGroup()
        {
            return (this.DBParameterGroup.Count > 0);
        }

        public bool IsSetDBSecurityGroup()
        {
            return (this.DBSecurityGroup.Count > 0);
        }

        public bool IsSetEndpoint()
        {
            return (this.endpointField != null);
        }

        public bool IsSetEngine()
        {
            return (this.engineField != null);
        }

        public bool IsSetInstanceCreateTime()
        {
            return this.instanceCreateTimeField.HasValue;
        }

        public bool IsSetLatestRestorableTime()
        {
            return this.latestRestorableTimeField.HasValue;
        }

        public bool IsSetMasterUsername()
        {
            return (this.masterUsernameField != null);
        }

        public bool IsSetPendingModifiedValues()
        {
            return (this.pendingModifiedValuesField != null);
        }

        public bool IsSetPreferredBackupWindow()
        {
            return (this.preferredBackupWindowField != null);
        }

        public bool IsSetPreferredMaintenanceWindow()
        {
            return (this.preferredMaintenanceWindowField != null);
        }

        public DBInstance WithAllocatedStorage(decimal allocatedStorage)
        {
            this.allocatedStorageField = new decimal?(allocatedStorage);
            return this;
        }

        public DBInstance WithAvailabilityZone(string availabilityZone)
        {
            this.availabilityZoneField = availabilityZone;
            return this;
        }

        public DBInstance WithBackupRetentionPeriod(decimal backupRetentionPeriod)
        {
            this.backupRetentionPeriodField = new decimal?(backupRetentionPeriod);
            return this;
        }

        public DBInstance WithDBInstanceClass(string DBInstanceClass)
        {
            this.DBInstanceClassField = DBInstanceClass;
            return this;
        }

        public DBInstance WithDBInstanceIdentifier(string DBInstanceIdentifier)
        {
            this.DBInstanceIdentifierField = DBInstanceIdentifier;
            return this;
        }

        public DBInstance WithDBInstanceStatus(string DBInstanceStatus)
        {
            this.DBInstanceStatusField = DBInstanceStatus;
            return this;
        }

        public DBInstance WithDBName(string DBName)
        {
            this.DBNameField = DBName;
            return this;
        }

        public DBInstance WithDBParameterGroup(params DBParameterGroupStatus[] list)
        {
            foreach (DBParameterGroupStatus status in list)
            {
                this.DBParameterGroup.Add(status);
            }
            return this;
        }

        public DBInstance WithDBSecurityGroup(params DBSecurityGroupMembership[] list)
        {
            foreach (DBSecurityGroupMembership membership in list)
            {
                this.DBSecurityGroup.Add(membership);
            }
            return this;
        }

        public DBInstance WithEndpoint(Amazon.RDS.Model.Endpoint endpoint)
        {
            this.endpointField = endpoint;
            return this;
        }

        public DBInstance WithEngine(string engine)
        {
            this.engineField = engine;
            return this;
        }

        public DBInstance WithInstanceCreateTime(DateTime instanceCreateTime)
        {
            this.instanceCreateTimeField = new DateTime?(instanceCreateTime);
            return this;
        }

        public DBInstance WithLatestRestorableTime(DateTime latestRestorableTime)
        {
            this.latestRestorableTimeField = new DateTime?(latestRestorableTime);
            return this;
        }

        public DBInstance WithMasterUsername(string masterUsername)
        {
            this.masterUsernameField = masterUsername;
            return this;
        }

        public DBInstance WithPendingModifiedValues(Amazon.RDS.Model.PendingModifiedValues pendingModifiedValues)
        {
            this.pendingModifiedValuesField = pendingModifiedValues;
            return this;
        }

        public DBInstance WithPreferredBackupWindow(string preferredBackupWindow)
        {
            this.preferredBackupWindowField = preferredBackupWindow;
            return this;
        }

        public DBInstance WithPreferredMaintenanceWindow(string preferredMaintenanceWindow)
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

        [XmlElement(ElementName="DBInstanceStatus")]
        public string DBInstanceStatus
        {
            get
            {
                return this.DBInstanceStatusField;
            }
            set
            {
                this.DBInstanceStatusField = value;
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

        [XmlElement(ElementName="DBParameterGroup")]
        public List<DBParameterGroupStatus> DBParameterGroup
        {
            get
            {
                if (this.DBParameterGroupField == null)
                {
                    this.DBParameterGroupField = new List<DBParameterGroupStatus>();
                }
                return this.DBParameterGroupField;
            }
            set
            {
                this.DBParameterGroupField = value;
            }
        }

        [XmlElement(ElementName="DBSecurityGroup")]
        public List<DBSecurityGroupMembership> DBSecurityGroup
        {
            get
            {
                if (this.DBSecurityGroupField == null)
                {
                    this.DBSecurityGroupField = new List<DBSecurityGroupMembership>();
                }
                return this.DBSecurityGroupField;
            }
            set
            {
                this.DBSecurityGroupField = value;
            }
        }

        [XmlElement(ElementName="Endpoint")]
        public Amazon.RDS.Model.Endpoint Endpoint
        {
            get
            {
                return this.endpointField;
            }
            set
            {
                this.endpointField = value;
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

        [XmlElement(ElementName="InstanceCreateTime")]
        public DateTime InstanceCreateTime
        {
            get
            {
                return this.instanceCreateTimeField.GetValueOrDefault();
            }
            set
            {
                this.instanceCreateTimeField = new DateTime?(value);
            }
        }

        [XmlElement(ElementName="LatestRestorableTime")]
        public DateTime LatestRestorableTime
        {
            get
            {
                return this.latestRestorableTimeField.GetValueOrDefault();
            }
            set
            {
                this.latestRestorableTimeField = new DateTime?(value);
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

        [XmlElement(ElementName="PendingModifiedValues")]
        public Amazon.RDS.Model.PendingModifiedValues PendingModifiedValues
        {
            get
            {
                return this.pendingModifiedValuesField;
            }
            set
            {
                this.pendingModifiedValuesField = value;
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


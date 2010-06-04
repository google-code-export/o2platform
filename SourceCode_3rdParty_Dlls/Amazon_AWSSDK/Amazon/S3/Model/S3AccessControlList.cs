namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [XmlType(Namespace="http://s3.amazonaws.com/doc/2006-03-01/"), XmlRoot(Namespace="http://s3.amazonaws.com/doc/2006-03-01/", IsNullable=false)]
    public class S3AccessControlList
    {
        private List<S3Grant> grantList = new List<S3Grant>();
        private Amazon.S3.Model.Owner owner;

        public void AddGrant(S3Grantee grantee, S3Permission permission)
        {
            S3Grant item = new S3Grant();
            item.WithGrantee(grantee);
            item.WithPermission(permission);
            this.grantList.Add(item);
        }

        internal bool IsSetGrants()
        {
            return (this.grantList.Count > 0);
        }

        internal bool IsSetOwner()
        {
            return (this.owner != null);
        }

        public void RemoveGrant(S3Grantee grantee)
        {
            List<S3Grant> list = new List<S3Grant>();
            foreach (S3Grant grant in this.grantList)
            {
                if (grant.Grantee.Equals(grantee))
                {
                    list.Add(grant);
                }
            }
            foreach (S3Grant grant2 in list)
            {
                this.grantList.Remove(grant2);
            }
        }

        public void RemoveGrant(S3Grantee grantee, S3Permission permission)
        {
            foreach (S3Grant grant in this.grantList)
            {
                if (grant.Grantee.Equals(grantee) && (grant.Permission == permission))
                {
                    this.grantList.Remove(grant);
                    break;
                }
            }
        }

        internal void Sort()
        {
            this.grantList.Sort(new ComparatorGrant());
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x400);
            builder.Append("<AccessControlPolicy xmlns=\"http://s3.amazonaws.com/doc/2006-03-01/\">");
            builder.Append("<Owner>");
            builder.Append("<ID>" + this.Owner.Id + "</ID>");
            builder.Append("<DisplayName>" + this.Owner.DisplayName + "</DisplayName>");
            builder.Append("</Owner>");
            builder.Append("<AccessControlList>");
            foreach (S3Grant grant in this.grantList)
            {
                builder.Append(grant.ToXML());
            }
            builder.Append("</AccessControlList>");
            builder.Append("</AccessControlPolicy>");
            return builder.ToString();
        }

        public S3AccessControlList WithGrants(params S3Grant[] args)
        {
            foreach (S3Grant grant in args)
            {
                this.grantList.Add(grant);
            }
            return this;
        }

        public S3AccessControlList WithOwner(Amazon.S3.Model.Owner owner)
        {
            this.owner = owner;
            return this;
        }

        [XmlElement(ElementName="Grants")]
        public List<S3Grant> Grants
        {
            get
            {
                return this.grantList;
            }
        }

        [XmlElement(ElementName="Owner")]
        public Amazon.S3.Model.Owner Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }
    }
}


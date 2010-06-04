namespace Amazon.S3.Model
{
    using System;
    using System.Text;
    using System.Xml.Serialization;

    public class S3ObjectVersion : S3Object
    {
        private bool fIsDeleteMarker;
        private bool fIsLatest;
        private string versionId;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Properties: {");
            if (base.IsSetKey())
            {
                builder.Append("Key:" + base.Key);
            }
            builder.Append(", VersionId:" + this.versionId);
            builder.Append(", IsLatest:" + this.fIsLatest);
            builder.Append(", IsDeleteMarker:" + this.fIsDeleteMarker);
            builder.Append(", LastModified:" + base.LastModified);
            builder.Append(", ETag:" + base.ETag);
            builder.Append(", Size:" + base.Size);
            builder.Append(", StorageClass:" + base.StorageClass);
            builder.Append(", Owner Properties: {");
            builder.Append("Id:" + base.Owner.Id);
            builder.Append(", DisplayName:" + base.Owner.DisplayName);
            builder.Append("}}");
            return builder.ToString();
        }

        [XmlElement(ElementName="IsDeleteMarker")]
        public bool IsDeleteMarker
        {
            get
            {
                return this.fIsDeleteMarker;
            }
            set
            {
                this.fIsDeleteMarker = value;
            }
        }

        [XmlElement(ElementName="IsLatest")]
        public bool IsLatest
        {
            get
            {
                return this.fIsLatest;
            }
            set
            {
                this.fIsLatest = value;
            }
        }

        [XmlElement(ElementName="VersionId")]
        public string VersionId
        {
            get
            {
                return this.versionId;
            }
            set
            {
                this.versionId = value;
            }
        }
    }
}


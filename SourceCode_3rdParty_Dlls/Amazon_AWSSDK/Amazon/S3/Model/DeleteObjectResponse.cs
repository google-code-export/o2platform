namespace Amazon.S3.Model
{
    using System;
    using System.Net;

    public class DeleteObjectResponse : S3Response
    {
        private bool isDeleteMarker;

        public override WebHeaderCollection Headers
        {
            set
            {
                base.Headers = value;
                string str = null;
                if (!string.IsNullOrEmpty(str = value.Get("x-amz-delete-marker")))
                {
                    this.isDeleteMarker = Convert.ToBoolean(str);
                }
            }
        }

        public bool IsDeleteMarker
        {
            get
            {
                return this.isDeleteMarker;
            }
            set
            {
                this.isDeleteMarker = value;
            }
        }
    }
}


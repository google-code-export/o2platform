namespace Amazon.S3.Model
{
    using System;

    public class PutObjectProgressArgs : EventArgs
    {
        private long total;
        private long transferred;

        public PutObjectProgressArgs(long transferred, long total)
        {
            this.transferred = transferred;
            this.total = total;
        }

        public override string ToString()
        {
            return string.Concat(new object[] { "Put Object Statistics. Percentage completed: ", this.PercentDone, ", Bytes transferred: ", this.transferred, ", Total bytes to transfer: ", this.total });
        }

        public int PercentDone
        {
            get
            {
                return (int) ((this.transferred * 100L) / this.total);
            }
        }

        public long TotalBytes
        {
            get
            {
                return this.total;
            }
        }

        public long TransferredBytes
        {
            get
            {
                return this.transferred;
            }
        }
    }
}


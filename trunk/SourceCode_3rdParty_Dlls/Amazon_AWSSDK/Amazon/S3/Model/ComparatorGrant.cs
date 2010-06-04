namespace Amazon.S3.Model
{
    using System;
    using System.Collections.Generic;

    public class ComparatorGrant : IComparer<S3Grant>
    {
        public int Compare(S3Grant x, S3Grant y)
        {
            return x.ToString().CompareTo(y.ToString());
        }
    }
}


namespace Amazon.CloudFront.Model
{
    using System;

    public class Tuple<T, U> : IEquatable<Tuple<T, U>>
    {
        private T first;
        private U second;

        public Tuple()
        {
        }

        public Tuple(T first, U second)
        {
            this.first = first;
            this.second = second;
        }

        public bool Equals(Tuple<T, U> other)
        {
            if (other == null)
            {
                return false;
            }
            if (((other.first == null) && (this.first == null)) && ((other.second == null) && (this.second == null)))
            {
                return true;
            }
            if (((other.first == null) && (this.first != null)) || ((other.second == null) && (this.second != null)))
            {
                return false;
            }
            return (other.first.Equals(this.first) && other.second.Equals(this.second));
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (base.GetType() == obj.GetType())) && this.Equals((Tuple<T, U>) obj));
        }

        public override int GetHashCode()
        {
            if ((this.first != null) && (this.second != null))
            {
                return (this.first.GetHashCode() ^ this.second.GetHashCode());
            }
            return base.GetHashCode();
        }

        public T First
        {
            get
            {
                return this.first;
            }
            set
            {
                this.first = value;
            }
        }

        public U Second
        {
            get
            {
                return this.second;
            }
            set
            {
                this.second = value;
            }
        }
    }
}


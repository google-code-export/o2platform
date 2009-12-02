using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Play
{
    public class Address
    {
        public Address(string owner, string city)
        {
            this.owner = owner;
            this.city = city;
        }

        private string owner;

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public override string ToString()
        {
            return city;
        }
    }
}

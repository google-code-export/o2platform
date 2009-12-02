using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq.Play
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string firstname, int age)
        {
            this.firstname = firstname;
            this.age = age;
        }

        private string firstname;

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public override string ToString()
        {
            return firstname + " (" + Age.ToString() + ")";
        }

        private List<Person> friends = new List<Person>();

        public List<Person> Friends
        {
            get { return friends; }
        }

    }
}

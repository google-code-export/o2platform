using System;
using System.Collections.Generic;
using System.Text;
using Evaluant.NLinq;
using Evaluant.NLinq.Memory;
using System.Collections;

namespace Evaluant.NLinq.Play
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] people = new Person[] { 
                new Person("Bill", 31), 
                new Person("John", 30), 
                new Person("Cindy", 25), 
                new Person("Sue", 29) 
            };

            // For testing physical links
            people[0].Friends.Add(people[0]);
            people[0].Friends.Add(people[1]);
            people[1].Friends.Add(people[2]);
            people[2].Friends.Add(people[3]);
            people[3].Friends.Add(people[0]);

            // For testing logical links
            Address[] addresses = new Address[] {
                new Address("Bill", "Redmon"),
                new Address("Bill", "Boston"),
                new Address("Cindy", "New York")
            };

            NLinqQuery query;
            LinqToMemory linq;

            #region Projections
            Console.WriteLine();
            Console.WriteLine("Projections");
            Console.WriteLine();

            query = new NLinqQuery(
                @"  from c in people 
                    from d in people
                    where c.Age > d.Age
                    select new Evaluant.NLinq.Play.Person ( c.Firstname, d.Age )");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            foreach (Person o in linq.Enumerate<Person>())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Anonymous types
            Console.WriteLine();
            Console.WriteLine("Anonymous types");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from c in people 
                    select new Evaluant.NLinq.Play.Person { c.Firstname, Age = c.Age * 2}");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            foreach (Person o in linq.Enumerate<Person>())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Physical links
            Console.WriteLine();
            Console.WriteLine("Physical links");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from c in people
                    from f in c.Friends
                    select new { c.Firstname, Friend = f.Firstname.Substring(0,1) }");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            foreach (object o in linq.Enumerate())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Ordering
            Console.WriteLine();
            Console.WriteLine("Ordering");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from c in people
                    orderby c.Age descending, c.Firstname
                    select c");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            foreach (object o in linq.Enumerate<Person>())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Grouping
            Console.WriteLine();
            Console.WriteLine("Grouping");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from m in methods
                    where !m.IsStatic
                    orderby m.Name
                    group m by m.Name into g
                    select new { MethodName = g.Key, Overloads = g.Count() }");

            linq = new LinqToMemory(query);
            linq.AddSource("methods", typeof(string).GetMethods());

            foreach (object o in linq.Enumerate())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Computed properties
            Console.WriteLine();
            Console.WriteLine("Computed properties");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from c in people
                    let t = c.Age * 3
                    where t % 2 == 0
                    orderby t descending
                    select new { c.Firstname, t }");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            foreach (object o in linq.Enumerate())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Logical links (Joins)
            Console.WriteLine();
            Console.WriteLine("Logical links (Joins)");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from p in people
                    join a in addresses on p.Firstname equals a.Owner
                    select new { p.Firstname, a.City }");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);
            linq.AddSource("addresses", addresses);

            foreach (object o in linq.Enumerate())
            {
                Console.WriteLine(o);
            }
            #endregion

            #region Useful scenario - Relationship sorting
            Console.WriteLine();
            Console.WriteLine("Useful scenario - Relationship sorting");
            Console.WriteLine();
            query = new NLinqQuery(
                @"  from f in friends
                    orderby f.Firstname descending
                    select f"
                );

            linq = new LinqToMemory(query);
            linq.AddSource("friends", people[0].Friends);

            foreach (Person o in linq.Enumerate<Person>())
            {
                Console.WriteLine(o);
            }
            #endregion


        }

        public class Truc
        {
            public Truc(string value)
            {
                this.value = value;
            }

            private string value;

            public string Value
            {
                get { return value; }
                set { value = value; }
            }

            private List<Truc> trucs = new List<Truc>();

            public List<Truc> Trucs
            {
                get { return trucs; }
                set { trucs = value; }
            }

        }

    }
}

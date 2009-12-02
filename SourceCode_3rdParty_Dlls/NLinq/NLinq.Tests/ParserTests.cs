using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Evaluant.NLinq;
using Evaluant.NLinq.Memory;
using System.Collections;
using System.Reflection;

namespace Evaluant.NLinq.Tests
{
    [TestFixture]
    public class ParserTests
    {
        LinqToMemory linq;
        NLinqQuery query;

        [Test]
        public void WhereSimple1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers
                where n < 5
                select n ");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(5, where1.Count);

        }

        [Test]
        public void WhereIndexed()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            query = new NLinqQuery(@"
                from n in 
                    digits.Where((digit, index) => index > digit.Length )
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("digits", digits);

            IList where1 = linq.List();

            Assert.AreEqual(5, where1.Count);
        }

        [Test]
        public void WhereIndexedBis()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            query = new NLinqQuery(@"
                from n in 
                    digits.Where((digit, index) => digit.Length < index)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("digits", digits);

            IList where1 = linq.List();

            Assert.AreEqual(5, where1.Count);
        }

        [Test]
        public void TakeSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers.Take(3)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(3, where1.Count);

        }

        [Test]
        public void SkipSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers.Skip(4)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(6, where1.Count);
        }


        [Test]
        public void TakeWhileSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers.TakeWhile(n => n < 6)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(4, where1.Count);

        }

        [Test]
        public void SkipWhileSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers.SkipWhile(n => n % 3 != 0)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(7, where1.Count);

        }

        [Test]
        public void SelectIndexed()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                from n in numbers.Select((num, index) => new {Num = num, InPlace = (num == index)})
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            IList where1 = linq.List();

            Assert.AreEqual(10, where1.Count);
            Assert.AreEqual(false, ((Variant)where1[0])["InPlace"]);
            Assert.AreEqual(true, ((Variant)where1[3])["InPlace"]);

        }

        [Test]
        public void Distinct1()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            query = new NLinqQuery(@"
                from n in 
                    factorsOf300.Distinct()
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("factorsOf300", factorsOf300);

            IList where1 = linq.List();

            Assert.AreEqual(3, where1.Count);

        }


        [Test]
        public void Union1()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            query = new NLinqQuery(@"
                from n in 
                    numbersA.Union(numbersB)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbersA", numbersA);
            linq.AddSource("numbersB", numbersB);

            IList where1 = linq.List();

            Assert.AreEqual(10, where1.Count);

        }

        [Test]
        public void Intersect1()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            query = new NLinqQuery(@"
                from n in 
                    numbersA.Intersect(numbersB)
                select n");

            List<object> n = new List<object>();

            linq = new LinqToMemory(query);
            linq.AddSource("numbersA", numbersA);
            linq.AddSource("numbersB", numbersB);

            IList where1 = linq.List();

            Assert.AreEqual(2, where1.Count);
        }

        [Test]
        public void Except()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            query = new NLinqQuery(@"
                from n in 
                    numbersA.Except(numbersB)
                select n");

            linq = new LinqToMemory(query);
            linq.AddSource("numbersA", numbersA);
            linq.AddSource("numbersB", numbersB);

            IList where1 = linq.List();

            Assert.AreEqual(5, where1.Count);
        }

        [Test]
        public void First()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.First((num, index) => (num % 2 == 0) && (index % 2 == 0))"
                );

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(6, result);
        }

        [Test]
        public void ElementAt()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                (from n in numbers
                where n < 5
                select n ).ElementAt(3)"
                );

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void CountSimple()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            query = new NLinqQuery(@"
                factorsOf300.Distinct().Count() 
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("factorsOf300", factorsOf300);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void CountConditional()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 }; 

            query = new NLinqQuery(@"
                numbers.Count(n => n % 2 == 1)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(5, result);
        }

        [Test]
        public void CountIndexed()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.Count((n, index) => n % 2 == index % 2)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(4, result);
        }

        [Test]
        public void SumSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.Sum()
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(45, result);
        }

        [Test]
        public void SumProjection()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            query = new NLinqQuery(@"
                words.Sum(w => w.Length)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(20, result);
        }

        [Test]
        public void MinSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.Min()
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(0, result);
        }


        [Test]
        public void MaxSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.Max()
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(9, result);
        }

        [Test]
        public void MinProjection()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            query = new NLinqQuery(@"
                words.Min(w => w.Length)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(5, result);
        }

        [Test]
        public void MaxProjection()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            query = new NLinqQuery(@"
                words.Max(w => w.Length)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            int result = linq.Evaluate<int>();

            Assert.AreEqual(9, result);
        }

        [Test]
        public void AverageSimple()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            query = new NLinqQuery(@"
                numbers.Average()
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("numbers", numbers);

            double result = linq.Evaluate<double>();

            Assert.AreEqual(4.5, result);
        }

        [Test]
        public void AverageProjection()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            query = new NLinqQuery(@"
                words.Average(w => w.Length)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            double result = linq.Evaluate<double>();

            Assert.AreEqual(6.66666666666667, result);
        }

        [Test]
        public void ShouldHandleEscapeSequence()
        {
            string[] words = { " che rry " };

            query = new NLinqQuery(@"
                from m in words
                select ' \'che rr :     y\' '
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            Assert.AreEqual(" 'che rr :     y' ", linq.List<string>()[0]);

            query = new NLinqQuery(@"
                from m in words
                select 's.ros\u0040evaluant.com'
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            Assert.AreEqual("s.ros@evaluant.com", linq.List<string>()[0]);
        }

        [Test]
        public void ShouldHandleKeywordsInStringLiterals()
        {
            string[] words = { " che rry " };

            query = new NLinqQuery(@"
                from m in words
                select 'true'
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("words", words);

            Assert.AreEqual("true", linq.List<string>()[0]);
        }

        class Person
        {
            public Person(string name)
            {
                this.name = name;
            }

            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

        [Test]
        public void ShouldSortAList()
        {
            List<Person> people = new List<Person>(
                new Person[] { new Person("B"), new Person("A"), new Person("C") });

            query = new NLinqQuery(@"
                from p in people
                orderby p.Name
                select p
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            List<Person> result = linq.List<Person>();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("A", result[0].Name);
            Assert.AreEqual("B", result[1].Name);
            Assert.AreEqual("C", result[2].Name);
        }

        [Test]
        public void ShouldProvidePaging()
        {
            List<Person> people = new List<Person>(
                new Person[] { new Person("B"), new Person("A"), new Person("C") });

            query = new NLinqQuery(@"
                (from p in people
                orderby p.Name
                select p).Where((p, index) => index > 0)
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            /// TODO: be able to add linq.AddSource("first", 0);

            List<Person> result = linq.List<Person>();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("B", result[0].Name);
            Assert.AreEqual("C", result[1].Name);
        }

        [Test]
        public void ShouldHandleNullValues()
        {
            List<Person> people = new List<Person>(
                new Person[] { new Person("B"), new Person("A"), null });

            query = new NLinqQuery(@"
                from p in people
                where p != null
                select p
            ");

            linq = new LinqToMemory(query);
            linq.AddSource("people", people);

            List<Person> result = linq.List<Person>();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("B", result[0].Name);
            Assert.AreEqual("A", result[1].Name);
        }

        [Test]
        public void ShouldHandleSubQueries()
        {
            query = new NLinqQuery(@"
                from m in methods
                where !m.IsStatic
                orderby m.Name
                group m by m.Name into g
                select new { MethodName = g.Key, Overloads = g.Count() }");

            linq = new LinqToMemory(query);
            linq.AddSource("methods", typeof(string).GetMethods());

            IList result = linq.List();

            Assert.AreEqual(35, result.Count);

            query = new NLinqQuery(@"
                from m in methods
                where !m.IsStatic
                orderby m.Name
                group m by m.Name into g
                select new { MethodName = g.Key, Overloads = g.Count(), MethodNamesLength = g.Sum(
                    meth => meth.Name.Length ) }");

            linq = new LinqToMemory(query);
            linq.AddSource("methods", typeof(string).GetMethods());


            result = linq.List();

            Assert.AreEqual(35, result.Count);
        }

        [Test]
        public void ShouldHandleSubQueries2()
        {
            query = new NLinqQuery(@"
                from m in methods
                where !m.IsStatic
                orderby m.Name
                group m by m.Name into g
                select new { MethodName = g.Key, Overloads = g.Count(), MethodParameters = g.Sum(
                    meth => (from p in meth.GetParameters() select p).Count() 
                    ) 
                }"
            );

            linq = new LinqToMemory(query);
            linq.AddSource("methods", typeof(string).GetMethods());

            IList result = linq.List();

            Assert.AreEqual(35, result.Count);

            foreach (Variant v in result)
            {
                Console.WriteLine(v["MethodName"] + " " + v["Overloads"] + " " + v["MethodParameters"]);
            }
        }
    }
}

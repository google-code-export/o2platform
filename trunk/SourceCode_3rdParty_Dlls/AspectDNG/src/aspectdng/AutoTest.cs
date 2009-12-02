/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
#if TEST
using System;
using System.Diagnostics;
using DotNetGuru.AspectDNG.XPath;
using DotNetGuru.AspectDNG.Util;
using Mono.Cecil;


namespace DotNetGuru.AspectDNG {
    public class AutoTest {
        public static void AssertEqual(object expected, object actual) {
            if (!expected.Equals(actual)) {
                string msg = string.Format("Expected {0} but got {1}", expected, actual);
                throw new Exception(msg);
            }
        }
        public static void AssertFalse(bool condition, string message) {
            if (condition) 
                throw new Exception(message);
        }
        public static void AssertFalse(bool condition) {
            AssertFalse(condition, "Expected false");
        }
        public static void AssertTrue(bool condition) {
            AssertFalse(!condition, "Expected true");
        }

        private static void TestSimpleRegex() {
            string txt = "some some .text";
            AssertTrue(SimpleRegex.IsPreciseMatch(txt, "so*xt"));
            AssertFalse(SimpleRegex.IsPreciseMatch(txt, "so*.ext"));
            AssertFalse(SimpleRegex.IsPreciseMatch(txt, "{some }2.text"));
        }

        public static void TestSuite() {
            TestSimpleRegex();

            Navigator nav = new Navigator(AssemblyFactory.GetAssembly(typeof(AutoTest).Assembly.Location));

            nav.MoveToRoot();
            AssertEqual("/", nav.Name);

            nav.MoveToFirstChild();
            AssertEqual("Assembly", nav.Name);

            nav.MoveToFirstAttribute();
            AssertEqual("Name", nav.Name);
            AssertEqual("aspectdng", nav.Value);

            nav.MoveToNextAttribute();
            AssertEqual("FullName", nav.Name);
            AssertFalse(nav.MoveToNextAttribute(), "only be 2 attributes on an assembly");
            AssertFalse(nav.MoveToFirstChild(), "attributes don't have firstChild");

            nav.MoveToParent(); // To assembly
            AssertEqual("Assembly", nav.Name);

            nav.MoveToFirstChild();
            AssertEqual("Attribute", nav.Name);
            nav.MoveToNext();
            AssertEqual("Attribute", nav.Name);
            
            while (nav.Name != "Type") nav.MoveToNext();
            nav.MoveToNext(); // Second type
            AssertEqual(1, nav.ChildIndex);

            Navigator otherNav = (Navigator) nav.Clone();

            AssertTrue(nav.MoveToFirstChild()); // Type children
            while (nav.Name != "Method") nav.MoveToNext(); // First Method
            nav.MoveToNext(); // Second Method
            AssertEqual(1, nav.ChildIndex);

            AssertTrue(nav.MoveToFirstChild()); // Method children
            nav.MoveToFirstAttribute();
            AssertTrue(nav.MoveToParent()); // Back to method children
            AssertTrue(nav.MoveToParent()); // Back to Method
            AssertTrue(nav.MoveToParent()); // Back to Type

            otherNav.MoveToFirstChild();
            otherNav.MoveToParent();
            AssertTrue(nav.IsSamePosition(otherNav));

            nav.MoveToRoot();
            AssertEqual(1, nav.SelectList("/").Count);
            AssertEqual(1, nav.SelectList("/Assembly").Count);

            AssertFalse(nav.SelectList("//Type").Count == 0);
            AssertFalse(nav.SelectList("//Type[starts-with(@Name, 'A')]").Count == 0);
            AssertTrue(nav.SelectList("//Type/*/*/*").Count == 0);
            AssertTrue(nav.SelectList("//Type").Count > nav.SelectList("//Type[starts-with(@Name, 'A')]").Count);
            AssertTrue(nav.SelectList("//Type").Count > nav.SelectList("//Type[.//Attribute]").Count);

            AssertFalse(nav.SelectList("//Type[.//Attribute]").Count == 0);

            DateTime start = DateTime.Now;
            int nb = nav.SelectList("//*[name(following::*[1]) = 'Type']").Count;
            Console.WriteLine("Time elapsed : " + (DateTime.Now - start).Milliseconds);
        }
    }
}
#endif
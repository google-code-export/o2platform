using System;
using System.Collections.Generic;
using System.Text;
using Jint.Expressions;

namespace Jint.Native
{
    [Serializable]
    public class JsFunction : JsDictionaryObject
    {
        public string Name { get; set; }
        public Statement Statement { get; set; }
        public List<string> Arguments { get; set; }
        public JsScope Scope { get; set; }

        public List<JsDictionaryObject> DeclaringScopes { get; set; }

        public JsFunction()
        {
            Arguments = new List<string>();
            Statement = new EmptyStatement();
            Scope = new JsScope();
            DeclaringScopes = new List<JsDictionaryObject>();
        }

        public JsFunction(Statement statement)
            : this()
        {
            Statement = statement;
        }

        public override bool HasProperty(string key)
        {
            return GetDescriptor(key) != null;
        }

        public override Descriptor GetDescriptor(string index)
        {
            if (index == PROTOTYPE)
                return prototypeDescriptor;

            Descriptor d;
            if (properties.TryGet(index, out d))
                return d;

            d = Scope.GetDescriptor(index);
            if (d != null)
                return d;

            foreach (JsDictionaryObject scope in DeclaringScopes)
            {
                d = scope.GetDescriptor(index);
                if (d != null)
                    return d;
            }

            return base.GetDescriptor(index);
        }

        public override object Value
        {
            get { return null; }
            set { }
        }

        public virtual JsInstance Execute(IJintVisitor visitor, JsDictionaryObject that, JsInstance[] parameters)
        {
            visitor.CallFunction(this, that, parameters);
            return that;
        }

        public const string TYPEOF = "function";

        public override string Class
        {
            get { return TYPEOF; }
        }

        public override string ToSource()
        {
            return String.Concat("(function ", String.IsNullOrEmpty(Name) ? "anonymous" : Name, "() { ... })");
        }

        public override string ToString()
        {
            return TYPEOF;
        }

        public override bool ToBoolean()
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Evaluant.NLinq
{
    public class TypeResolver
    {
        public static Type ResolveType(string fullname)
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = a.GetType(fullname, false, false);

                if (type != null)
                    return type;
            }

            return null;
        }
    }
}

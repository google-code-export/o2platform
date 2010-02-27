﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace O2.Kernel
{
    public class O2LiveObjects
    {
        public static Dictionary<string, object> LiveObjects;

        static O2LiveObjects()
        {
            clear();
        }

        public static void set(string name, object value)
        {
            add(name, value);        
        }

        public static void add(string name, object value)
        {
            if (LiveObjects.ContainsKey(name))
                LiveObjects[name] = value;
            else
                LiveObjects.Add(name, value);
        }

        public static object get(string name)
        {
            if (LiveObjects.ContainsKey(name))
                return LiveObjects[name];
            return null;
        }

        public static void clear()
        {
            LiveObjects = new Dictionary<string, object>();
        }
    }
}

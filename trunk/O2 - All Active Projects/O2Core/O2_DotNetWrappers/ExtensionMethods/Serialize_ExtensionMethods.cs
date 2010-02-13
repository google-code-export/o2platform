using System;
using O2.DotNetWrappers.DotNet;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Serialize_ExtensionMethods
    {
        public static bool serialize(this Object _object, string file)
        {
            return Serialize.createSerializedXmlFileFromObject(_object, file);
        }

        public static T deserialize<T>(this string file)
        {
            return (T)Serialize.getDeSerializedObjectFromXmlFile(file, typeof(T));
        }    	
    }
}

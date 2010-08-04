﻿using System;
using O2.DotNetWrappers.DotNet;
using O2.Kernel;

namespace O2.DotNetWrappers.ExtensionMethods
{
    public static class Serialize_ExtensionMethods
    {
        public static string serialize(this object _object)
        {
            var tempFile = PublicDI.config.getTempFileInTempDirectory(".xml");
            if (_object.serialize(tempFile))
                return tempFile;
            return "";
        }

        public static bool serialize(this Object _object, string file)
        {
            return Serialize.createSerializedXmlFileFromObject(_object, file);
        }

        public static string serialize(this object _object, bool serializeToFile)
        {
            if (serializeToFile)
                return _object.serialize();
            return Serialize.createSerializedXmlStringFromObject(_object);
        }

        public static T deserialize<T>(this string file)
        {
            return (T)Serialize.getDeSerializedObjectFromXmlFile(file, typeof(T));
        }

        public static string save(this object _object)
        {
            return _object.serialize();
        }

        
        public static T load<T>(this string pathToSerializedObject)
        {
            return pathToSerializedObject.deserialize<T>();
        }
    }
}

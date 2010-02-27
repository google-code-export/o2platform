﻿using System.Collections.Generic;

namespace O2.Kernel.ExtensionMethods
{
    public static class Logging_ExtensionMethods
    {
        public static void info(this object _object, string infoMessage)
        {
            PublicDI.log.info("[{0}] {1}", _object.type().Name, infoMessage);
        }

        public static void info(this object _object, string messageFormat, params object[] parameters)
        {
            PublicDI.log.info(messageFormat.format(parameters));
        }

        public static void debug(this object _object, string debugMessage)
        {
            PublicDI.log.debug("[{0}] {1}", _object.type().Name, debugMessage);
        }

        public static void debug(this object _object, string messageFormat, params object[] parameters)
        {
            PublicDI.log.debug(messageFormat.format(parameters));
        }

        public static void error(this object _object, string messageFormat, params object[] parameters)
        {
            PublicDI.log.info(messageFormat.format(parameters));
        }

        public static void info(this bool enabled, string infoFormat, params object[] parameters)
        {
            if (enabled)
                PublicDI.log.info(infoFormat, parameters);
        }

        public static void debug(this bool enabled, string debugFormat, params object[] parameters)
        {
            if (enabled)
                PublicDI.log.debug(debugFormat, parameters);
        }

        public static void error(this bool enabled, string errorFormat, params object[] parameters)
        {
            if (enabled)
                PublicDI.log.error(errorFormat, parameters);
        }


        public static void showInLog(this List<string> list)
        {
            PublicDI.log.debug("Showing {0} items from List<string>:");
            foreach (var item in list)
                PublicDI.log.info("   {0}", item);
        }

        public static void log(this string _string)
        {
            PublicDI.log.info(_string);
        }

        public static void info(this string _string)
        {
            PublicDI.log.info(_string);
        }

        public static void debug(this string _string)
        {
            PublicDI.log.debug(_string);
        }

        public static void error(this string _string)
        {
            PublicDI.log.error(_string);
        }
    }
}
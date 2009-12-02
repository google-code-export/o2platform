/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using DotNetGuru.AspectDNG.Config;

namespace DotNetGuru.AspectDNG {
	public enum LogLevel{
		Debug, Warn, Error
	}

    public class Log {
		public static LogLevel Level;
        private static long start = DateTime.Now.Ticks;

		static Log(){
			Level = AspectDngConfig.Instance.debug ? LogLevel.Debug : LogLevel.Warn;
		}

        public static void TimedDebug(string message) {
            Console.WriteLine("{0} [{1}]", message, (DateTime.Now.Ticks - start) / 10000);
        }

		public static void Debug(string message) {
			if (Level <= LogLevel.Debug)
				Console.Error.WriteLine(message);
		}
		public static void Debug(string message, params object[] data) {
			if (Level <= LogLevel.Debug){
                Console.Error.WriteLine(message, data);
			}
		}
		public static void Warn(string message) {
			if (Level <= LogLevel.Warn)
                Console.Error.WriteLine(message);
		}
		public static void Warn(string message, params object[] data) {
			if (Level <= LogLevel.Warn)
                Console.Error.WriteLine(message, data);
		}
		public static void Error(string message) {
            Console.Error.WriteLine(message);
		}
		public static void Error(string message, params object[] data) {
            Console.Error.WriteLine(message, data);
		}
		
		private static ArrayList m_ExecutedSpecs = new ArrayList();
		public static void LogExecutedSpec(AdviceSpec spec){
			m_ExecutedSpecs.Add(spec);
		}

		private static ArrayList m_Warnings = new ArrayList();
		public static void LogWarning(object cecilObject){
            string warning = "[AspectDNG Warning] Try to avoid:\n\t" + cecilObject;
            m_Warnings.Add(warning);
            Warn(warning);
		}

		private static void SaveWarnings() {
			string path = AspectDngConfig.Instance.warnings;
			if (path != null) {			
				StreamWriter sout = new StreamWriter(path);
				foreach(string warning in m_Warnings)
					sout.WriteLine(warning);
				sout.Close();
			}			
		}
		private static void SaveWeaving() {
			string path = AspectDngConfig.Instance.weaving;
			if (path != null){
				XmlTextWriter writer = new XmlTextWriter(path, Encoding.Unicode);
				writer.Formatting = Formatting.Indented;
				writer.WriteStartDocument();
				writer.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='Log.xsl'");

				writer.WriteStartElement("AspectDngLog");
				foreach(AdviceSpec spec in m_ExecutedSpecs)
					spec.ToXml(writer);
				writer.WriteEndDocument();
				writer.Close();

				string xsl = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Log.xsl")).ReadToEnd();
				StreamWriter output = new StreamWriter(Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + "Log.xsl");
				output.Write(xsl);
				output.Close();
			}
		}

		public static void Save(){
			SaveWeaving();
			SaveWarnings();
		}
	}
}
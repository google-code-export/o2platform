/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Text;
using System.Xml;
using DotNetGuru.AspectDNG.Joinpoints;

namespace DotNetGuru.AspectDNG.Config {
	public abstract class AdviceSpec : Attribute{
		public string targetXPath;
		public string targetRegExp;
		public string aspectXPath;
		public string aspectRegExp;

		// Same data, but with de-referenced variables
		public string realTargetXPath;
		public string realAspectXPath;

		// Where does this spec come from (file, type)
		public string origin;
		
		public readonly ArrayList AspectNavigators = new ArrayList();
		public readonly ArrayList TargetNavigators = new ArrayList();

        private static string m_Namespace = typeof(JoinPoint).Namespace + ".";

		public static AdviceSpec Create(Decorator d){
			AdviceSpec spec = (AdviceSpec) Activator.CreateInstance(Type.GetType(m_Namespace + d.Name));
			spec.aspectRegExp = d["@aspectRegExp"];
			spec.targetRegExp = d["@targetRegExp"];
			spec.aspectXPath = d["@aspectXPath"];
			spec.targetXPath = d["@targetXPath"];
			return spec;
		}
				
		public override string ToString() {
			StringBuilder builder = new StringBuilder().Append("\n");
			builder.AppendFormat("[{0}]\n", GetType().Name);

			Display(builder, "Origin", origin);
			
			Display(builder, "TargetXPath", targetXPath);
			Display(builder, "TargetRegExp", targetRegExp);
			Display(builder, " -> RealTargetXPath", realTargetXPath);

			Display(builder, "AspectXPath", aspectXPath);
			Display(builder, "AspectRegExp", aspectRegExp);
			Display(builder, " -> RealAspectXPath", realAspectXPath);

			return builder.ToString();
		}

		private static void Display(StringBuilder builder, string label, string str){
			if (str != null && str.Length > 0){
				builder.AppendFormat("\t[{0}] {1}\n", label, str);
			}
		}

		public void ToXml(XmlWriter writer) {
			writer.WriteStartElement(GetType().Name);
			Display(writer, "Origin", origin);
			
			Display(writer, "TargetXPath", targetXPath);
			Display(writer, "TargetRegExp", targetRegExp);
            Display(writer, "RealTargetXPath", realTargetXPath);
            Display(writer, "NbTargets", TargetNavigators.Count.ToString());

			Display(writer, "AspectXPath", aspectXPath);
			Display(writer, "AspectRegExp", aspectRegExp);
			Display(writer, "RealAspectXPath", realAspectXPath);
            Display(writer, "NbAspects", AspectNavigators.Count.ToString());
            writer.WriteEndElement();
		}

		private static void Display(XmlWriter writer, string label, string str){
			if (str != null && str.Length > 0)
				writer.WriteElementString(label, str);
		}

		public void DereferenceVariables(){
			AspectDngConfig conf = AspectDngConfig.Instance;
			aspectRegExp = conf.DereferenceVariablesIn(aspectRegExp);
			targetRegExp = conf.DereferenceVariablesIn(targetRegExp);
			aspectXPath = conf.DereferenceVariablesIn(aspectXPath);
			targetXPath = conf.DereferenceVariablesIn(targetXPath);
		}
	}
}
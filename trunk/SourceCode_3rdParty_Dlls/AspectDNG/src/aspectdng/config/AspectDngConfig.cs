/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace DotNetGuru.AspectDNG.Config {
    public class AspectDngConfig {
		private static AspectDngConfig m_Instance;
		public static AspectDngConfig Instance{
			get{ 
				if (m_Instance == null) m_Instance = new AspectDngConfig();
				return m_Instance; 
			}
		}

		public readonly string warnings;
		public readonly string weaving;
		public bool debug;
		
		public string TargetAssembly;
		public string AspectsAssembly;
		public string WeavedAssembly;
		
		public readonly string[] PrivateLocations;

		public readonly ArrayList Advice = new ArrayList();
		public readonly Hashtable Variables = new Hashtable();


		public AspectDngConfig() {}
		public AspectDngConfig(string configFile) {
			m_Instance = this;

			try{
				Decorator conf = new Decorator(configFile);
				conf.GoTo("/AspectDngConfig");

				// Read variables from the main config file
				foreach(Decorator d in conf.GetDecorators("//Variable"))
					Variables[d["@name"]] = d["@value"];

				debug = bool.Parse(conf["@debug"]);
				warnings = DereferenceVariablesIn(conf["@warnings"]);
				weaving = DereferenceVariablesIn(conf["@weaving"]);

				AspectsAssembly = DereferenceVariablesIn(conf["AspectsAssembly"]);
				TargetAssembly = DereferenceVariablesIn(conf["TargetAssembly"]);
				WeavedAssembly = DereferenceVariablesIn(conf["WeavedAssembly"]);

				PrivateLocations = conf.GetStrings("//PrivatePath");

				foreach(Decorator d in conf.GetDecorators("//Advice/*[not(self::Variable)]")){
					AdviceSpec spec = AdviceSpec.Create(d);
					spec.DereferenceVariables();
					AddAdvice(spec, configFile);
				}

				foreach(string path in conf.GetStrings("//AdviceFile")){
					Decorator otherConf = new Decorator(DereferenceVariablesIn(path));
					foreach(Decorator otherD in otherConf.GetDecorators("//Advice/*[not(self::Variable)]")){
						AdviceSpec spec = AdviceSpec.Create(otherD);
						spec.DereferenceVariables();
						AddAdvice(spec, path);
					}
					foreach(Decorator otherD in otherConf.GetDecorators("//Variable"))
						Variables[otherD["@name"]] = otherD["@value"];
				}

				Log.Level = debug ? LogLevel.Debug : LogLevel.Warn;
			}
			catch(Exception e){
                if (e.GetType().FullName.StartsWith("System.Xml"))
                    throw new ConfigurationException(e);
                else 
                    throw e;
			}

			foreach(AdviceSpec spec in Advice)
				spec.origin = configFile;
			Environment.CurrentDirectory = new FileInfo(configFile).Directory.FullName;
			m_Instance = this;
		}

		public void AddAdvice(AdviceSpec spec, string origin){
			spec.origin = origin;
			Advice.Add(spec);
		}

		private static Regex m_VariableRegEx = new Regex(@"(\$\([^$]*\))", RegexOptions.Compiled);
		public string DereferenceVariablesIn(string expression){
			if (expression != null){
				bool evaluateStringAgain = true;
				while (evaluateStringAgain){
					Match m = m_VariableRegEx.Match(expression);
					if (evaluateStringAgain = m.Success){
						string var = m.Captures[0].ToString();
						string varName = var.Substring(2, var.Length - 3);
						string varValue = Variables[varName] as string;
						if (varValue != null)
							expression = expression.Replace(var, varValue);
						
						else
							throw new Exception("problem: variable not declared : " + varName);
					}
				}
			}
			return expression;
		}

	}
}
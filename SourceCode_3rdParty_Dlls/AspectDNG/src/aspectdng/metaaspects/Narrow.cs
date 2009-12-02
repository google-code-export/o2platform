/* 
	Author 	: Thomas GIL (DotNetGuru.org)
	Date 	: 17 september 2006
	License : Public Domain
*/
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Binary;
using DotNetGuru.AspectDNG.Config;
using DotNetGuru.AspectDNG.Util;
using DotNetGuru.AspectDNG.XPath;

namespace DotNetGuru.AspectDNG.MetaAspects {
    public abstract class Narrow {
        // Utility methods (safe casts)
        public static MethodDefinition Method(object obj, AdviceSpec spec) {
            MethodDefinition result = obj as MethodDefinition;
            if (result == null)
                throw new AdviceException
                    ("Expected a method, but got " + obj + ":" + obj.GetType(), spec);
            return result;
        }
        public static MethodDefinition Method(Navigator nav, AdviceSpec spec) {
            return Method((object)nav.Current, spec);
        }

        public static MethodDefinition ConcreteMethod(Navigator nav, AdviceSpec spec) {
            MethodDefinition result = Method(nav, spec);
            if (result.IsAbstract) throw new AdviceException
                ("Expected a concrete method, but got " + result, spec);
            return result;
        }

        public static MethodDefinition StaticMethodMatching(Navigator nav, string regex, AdviceSpec spec) {
            MethodDefinition result = ConcreteMethodMatching(nav, regex, spec);
            if (!result.IsStatic) throw new AdviceException
               ("Expected a method, but got " + result, spec);
            return result;
        }

        public static MethodDefinition ConcreteMethodMatching(Navigator nav, string regex, AdviceSpec spec) {
            MethodDefinition result = Method(nav, spec);
            if (!SimpleRegex.IsMatch(result, regex)) {
                string errorMsg = string.Format
                    ("Expected to match signature {0}, but got {1}",
                    SimpleRegex.EscapePseudoRegex(regex), result);
                throw new AdviceException(errorMsg, spec);
            }
            return result;
        }

        public static MethodDefinition Interceptor(Navigator nav, AdviceSpec spec) {
            return StaticMethodMatching(nav,
                "System.Object *::*(DotNetGuru.AspectDNG.Joinpoints.*)", spec);
        }

        public static Instruction Instruction(Navigator nav, AdviceSpec spec) {
            Instruction result = nav.Current as Instruction;
            if (result == null)
                throw new AdviceException
                    ("Expected an instruction, but got " + nav.Current + ":" + nav.Current.GetType(), spec);
            return result;
        }

        public static Instruction CallInstruction(Navigator nav, AdviceSpec spec) {
            Instruction result = Instruction(nav, spec);
            string name = result.OpCode.Name;
            if (!name.StartsWith("call") && !name.StartsWith("newobj"))
                throw new AdviceException("Expected a call instruction, but got " + nav.Current, spec);
            return result;
        }

        public static TypeDefinition TypeDefinition(Navigator nav, AdviceSpec spec) {
            TypeDefinition result = nav.Current as TypeDefinition;
            if (result == null)
                throw new AdviceException
                    ("Expected a type definition, but got " + nav.Current + " : " + nav.Current.GetType(), spec);
            return result;
        }

        public static TypeDefinition InterfaceDefinition(Navigator nav, AdviceSpec spec) {
            TypeDefinition result = TypeDefinition(nav, spec);
            if (!result.IsInterface) throw new AdviceException
               ("Expected an interface definition, but got " + nav.Current, spec);
            return result;
        }
    }
}

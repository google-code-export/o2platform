using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.SharpDevelop.Dom;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class FieldDeclaration_ExtensionMethods
    {
        public static FieldDeclaration add_Field(this CompilationUnit compilationUnit, IField iField)
        {
            
            var fieldtype = compilationUnit.add_Type(iField.DeclaringType);
            return fieldtype.add_Field(iField);            
        }

            /*   // move to method add_Field
                var @namespace = iField.DeclaringType.Namespace;
                var typeName = iField.DeclaringType.Name;
                //var methodDeclaration = o2MethodStream.O2MappedAstData.methodDeclaration(iMethod);
                var type = compilationUnit.add_Type(
                //compilationUnit.add_Method(@namespace, typeName, methodDeclaration);
             */
 
        public static FieldDeclaration add_Field(this TypeDeclaration typeDeclaration, IField iField)
        {            
            // move to fields(..) extensionmethods
            FieldDeclaration field = null;
            foreach(var child in typeDeclaration.Children)
                if (child is FieldDeclaration)
                    foreach (var variable in (child as FieldDeclaration).Fields)
                    {
                        if (variable.Name == iField.Name)
                            field = (child as FieldDeclaration);
                    }
            if (field != null)
                return field;

            var classFinder = new ClassFinder(iField.DeclaringType, 0,0);
            field = ICSharpCode.SharpDevelop.Dom.Refactoring.CodeGenerator.ConvertMember(iField, classFinder);
            if (field != null)
                typeDeclaration.Children.Insert(0,field);
            //var varia = new
            //var field = new FieldDeclaration(
            /*var fields = (from child in typeDeclaration.Children
                         where child is FieldDeclaration
                         select (FieldDeclaration)child).ToList();
            */
            //var fields = typeDeclaration.fields

            /*var newType = namespaceDeclaration.types(typeName);		// check if already exists and if it does return it
            if (newType != null)
                return newType;

            const Modifiers modifiers = Modifiers.None | Modifiers.Public;
            newType = new TypeDeclaration(modifiers, new List<AttributeSection>())
            {
                Name = typeName
            };
            namespaceDeclaration.AddChild(newType);*/
            return field;
        }

        public static FieldDeclaration add_Field(this TypeDeclaration typeDeclaration, string fieldType, string fieldName)
        {
            return null;
        }
    }
}

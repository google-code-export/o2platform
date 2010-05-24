using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.NRefactory.Ast;

namespace O2.API.AST.ExtensionMethods.CSharp
{
    public static class PropertyDeclaration_ExtensionMethods
    {

        public static PropertyDeclaration add_Property(this CompilationUnit compilationUnit, IProperty iProperty)
        {
            var propertyType = compilationUnit.add_Type(iProperty.DeclaringType);
            return propertyType.add_Property(iProperty);            
        }

        public static PropertyDeclaration add_Property(this TypeDeclaration typeDeclaration, IProperty iProperty)
        {           
            foreach (var child in typeDeclaration.Children)
                if (child is PropertyDeclaration)
                    if ((child as PropertyDeclaration).Name == iProperty.Name)
                        return (child as PropertyDeclaration);                    
            //if (field != null)
            //    return field;
            AttributedNode property = null;
            var classFinder = new ClassFinder(iProperty.DeclaringType, 0, 0);
            property = ICSharpCode.SharpDevelop.Dom.Refactoring.CodeGenerator.ConvertMember(iProperty, classFinder);
            if (property != null)
                typeDeclaration.Children.Insert(0, property);
            return (PropertyDeclaration)property;
        }

        public static PropertyDeclaration add_Property(this TypeDeclaration typeDeclaration, string propertyType, string propertyName)
        {
            return null;
        }
    }
}

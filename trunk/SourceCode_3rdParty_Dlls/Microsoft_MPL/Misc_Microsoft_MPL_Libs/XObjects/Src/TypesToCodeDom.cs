//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;

namespace Xml.Schema.Linq.CodeGen
{
    public class CodeDomTypesGenerator {
        
        LinqToXsdSettings settings;
        TypeBuilder typeBuilder;
        TypeBuilder emptyTypeBuilder;
        
        XmlQualifiedName rootElementName = XmlQualifiedName.Empty;
        CodeNamespace codeNamespace;
 
        Dictionary<string, CodeNamespace> codeNamespacesTable;
        Dictionary<XmlSchemaObject, string> nameMappings;
        Dictionary<CodeNamespace, List<CodeTypeDeclaration>> xroots;
        List<ClrWrapperTypeInfo> wrapperRootElements;

        string currentNamespace;
        string currentFullTypeName;

        static CodeStatementCollection typeDictionaryAddStatements;
        static CodeStatementCollection elementDictionaryAddStatements;
        static CodeStatementCollection wrapperDictionaryAddStatements;

        public CodeDomTypesGenerator(bool nameMangler2) : 
            this(new LinqToXsdSettings(nameMangler2)) 
        {
        }

        public CodeDomTypesGenerator(LinqToXsdSettings settings) {
            if (settings == null) {
                throw new ArgumentNullException("Argument setttings should not be null.");
            }
            this.settings = settings;
            codeNamespacesTable = new Dictionary<string, CodeNamespace>();
            xroots = new Dictionary<CodeNamespace, List<CodeTypeDeclaration>>();
            typeDictionaryAddStatements = new CodeStatementCollection();
            elementDictionaryAddStatements = new CodeStatementCollection();
            wrapperDictionaryAddStatements = new CodeStatementCollection();
        }

        public IEnumerable<CodeNamespace> GenerateTypes(ClrMappingInfo binding) {
            if (binding == null) {
                throw new ArgumentException("binding");
            }
            nameMappings = binding.NameMappings;
            Debug.Assert(nameMappings != null);
            foreach(ClrTypeInfo type in binding.Types) { 
                if (type.IsWrapper) { 
                    if (wrapperRootElements == null) {
                        wrapperRootElements = new List<ClrWrapperTypeInfo>();
                    }
                    wrapperRootElements.Add(type as ClrWrapperTypeInfo);

                }
                else {
                    codeNamespace = GetCodeNamespace(type.clrtypeNs);
                    ClrSimpleTypeInfo stInfo = type as ClrSimpleTypeInfo;
                    if (stInfo != null) {
                        codeNamespace.Types.Add(TypeBuilder.CreateSimpleType(stInfo, nameMappings, settings));
                    }
                    else {
                        CodeTypeDeclaration decl = ProcessType(type as ClrContentTypeInfo, null, true); //Sets current codeNamespace
                        codeNamespace.Types.Add(decl);

                        if (type.IsRootElement) {
                            List<CodeTypeDeclaration> types;

                            if (!xroots.TryGetValue(codeNamespace, out types)) {
                                types = new List<CodeTypeDeclaration>();
                                xroots.Add(codeNamespace, types);
                            }

                            types.Add(decl);
                        }
                    }
                }
            }
            ProcessWrapperTypes();
            CreateTypeManager();
            CreateXRoots();
            return codeNamespacesTable.Values;
        }

        private CodeTypeDeclaration ProcessType(ClrContentTypeInfo typeInfo, string parentIdentifier, bool globalType) {
            
            SetFullTypeName(typeInfo, parentIdentifier);
                      
            if(globalType) {
                currentNamespace = typeInfo.clrtypeNs;
            }
            //Build type using TypeBuilder
            typeBuilder = GetTypeBuilder();
            typeBuilder.CreateTypeDeclaration(typeInfo);
            ProcessProperties(typeInfo.Content, typeInfo.Annotations);
            typeBuilder.CreateFunctionalConstructor(typeInfo.Annotations);
            typeBuilder.ImplementInterfaces(settings.EnableServiceReference);
            typeBuilder.ApplyAnnotations(typeInfo);
            if (globalType) {
                if (typeInfo.typeOrigin == SchemaOrigin.Fragment) {
                    typeBuilder.AddTypeToTypeManager(typeDictionaryAddStatements, Constants.TypeDictionaryField);
                }
                else {
                    typeBuilder.AddTypeToTypeManager(elementDictionaryAddStatements, Constants.ElementDictionaryField);
                }
            }
            CodeTypeDeclaration builtType = typeBuilder.TypeDeclaration;
            ProcessNestedTypes(typeInfo.NestedTypes, builtType, typeInfo.clrFullTypeName);
            return builtType;
        }

        
        private void ProcessNestedTypes(List<ClrTypeInfo> anonymousTypes, CodeTypeDeclaration parentTypeDecl, string parentIdentifier) {
            foreach(ClrTypeInfo nestedType in anonymousTypes) {
                ClrSimpleTypeInfo stInfo = nestedType as ClrSimpleTypeInfo;
                CodeTypeDeclaration decl = null;
                if (stInfo != null) {
                    decl = TypeBuilder.CreateSimpleType(stInfo, nameMappings, settings);
                    decl.TypeAttributes = System.Reflection.TypeAttributes.NestedPrivate; //Anonymous simple types are private within the scope of the parent class
                }
                else {
                    decl = ProcessType(nestedType as ClrContentTypeInfo, parentIdentifier, false);
                }
                parentTypeDecl.Members.Add(decl);
            }
        }

        private void ProcessProperties(IEnumerable<ContentInfo> properties, List<ClrAnnotation> annotations) {
            foreach(ContentInfo child in properties) { //Child can either be a property directly for attributes or a grouping for content model,
                if (child.ContentType == ContentType.Property) {
                    ClrPropertyInfo propertyInfo = child as ClrPropertyInfo;
                    propertyInfo.UpdateTypeReference(currentFullTypeName, currentNamespace, nameMappings);
                    typeBuilder.CreateAttributeProperty(child as ClrPropertyInfo, null);
                }
                else  {
                    GroupingInfo rootGroup = child as GroupingInfo;
                    if (rootGroup.IsComplex) {
                        typeBuilder.StartGrouping(rootGroup);
                        ProcessComplexGroupProperties(rootGroup, annotations);
                        typeBuilder.EndGrouping();
                    }
                    else {
                        ProcessGroup(rootGroup, annotations);
                    }                        
                }
            }
        }

        private void ProcessGroup(GroupingInfo grouping, List<ClrAnnotation> annotations) {
            typeBuilder.StartGrouping(grouping);
            foreach(ContentInfo child in grouping.Children) {
                if (child.ContentType == ContentType.Property) {
                    ClrPropertyInfo propertyInfo = child as ClrPropertyInfo;
                    propertyInfo.UpdateTypeReference(currentFullTypeName, currentNamespace, nameMappings);
                    typeBuilder.CreateProperty(propertyInfo, annotations);
                }
                else if (child.ContentType == ContentType.WildCardProperty) {
                    ClrWildCardPropertyInfo propertyInfo = child as ClrWildCardPropertyInfo;
                    typeBuilder.CreateProperty(propertyInfo, annotations);
                }
                else {
                    Debug.Assert(child.ContentType == ContentType.Grouping);
                    ProcessGroup(child as GroupingInfo, annotations);
                }
            }
            typeBuilder.EndGrouping();
        }

        private void ProcessComplexGroupProperties(GroupingInfo grouping, List<ClrAnnotation> annotations) {
            foreach(ContentInfo child in grouping.Children) {
                if (child.ContentType == ContentType.Property) {
                    ClrPropertyInfo propertyInfo = child as ClrPropertyInfo;
                    propertyInfo.UpdateTypeReference(currentFullTypeName, currentNamespace, nameMappings);
                    typeBuilder.CreateProperty(propertyInfo, annotations);
                }
                else if (child.ContentType == ContentType.WildCardProperty) {
                    ClrWildCardPropertyInfo propertyInfo = child as ClrWildCardPropertyInfo;
                    typeBuilder.CreateProperty(propertyInfo, annotations);
                }
                else {
                    Debug.Assert(child.ContentType == ContentType.Grouping);
                    ProcessComplexGroupProperties(child as GroupingInfo, annotations);
                }
            }
        }

        private void CreateXRoots() {
            // For the global XRoot structure, we union the lists of types
            List<CodeTypeDeclaration> allTypes = new List<CodeTypeDeclaration>();
            List<CodeNamespace> allNamespaces = new List<CodeNamespace>();
            string rootClrNamespace = settings.GetClrNamespace(rootElementName.Namespace);
            CodeNamespace rootCodeNamespace = null;
            if(!codeNamespacesTable.TryGetValue(rootClrNamespace, out rootCodeNamespace)) { //This might happen if the schema set has no global elements and only global types
                rootCodeNamespace = codeNamespacesTable.Values.FirstOrDefault();                                    //then you can create a root tag with xsi:type 
                // rootCodeNamespace may still be null  if schema has only simple typed global elements or simple types which we are ignoring for now 
            }

            //Build list of types that will need to be included 
            //in XRoot
            foreach (CodeNamespace codeNamespace in xroots.Keys){
                if (rootCodeNamespace == null) rootCodeNamespace = codeNamespace;

                for (int i = 0; i < xroots[codeNamespace].Count; i++) {
                    allTypes.Add(xroots[codeNamespace][i]);
                    allNamespaces.Add(codeNamespace);
                }
             
                CreateXRoot(codeNamespace, "XRootNamespace", xroots[codeNamespace], null);
                
            }

            CreateXRoot(rootCodeNamespace, "XRoot", allTypes, allNamespaces);
        }


        private void CreateXRoot(CodeNamespace codeNamespace, string rootName, List<CodeTypeDeclaration> elements, List<CodeNamespace> namespaces)
        {

            LocalSymbolTable lst = new LocalSymbolTable();
            
            CodeTypeDeclaration xroot = CodeDomHelper.CreateTypeDeclaration(rootName, null);

            //Create Methods
            CodeMemberField docField = CodeDomHelper.CreateMemberField("doc", 
                                                                "XDocument", 
                                                                MemberAttributes.Private, 
                                                                false);

            CodeMemberField rootField = CodeDomHelper.CreateMemberField("rootObject", 
                                                                Constants.XTypedElement, 
                                                                MemberAttributes.Private, 
                                                                false);

            xroot.Members.Add(docField);
            xroot.Members.Add(rootField);


            lst.Init(rootName);
            lst.RegisterMember("doc");
            lst.RegisterMember("rootObject");
            lst.RegisterMember("Load");
            lst.RegisterMember("Parse");
            lst.RegisterMember("Save");
            lst.RegisterMember("XDocument");

            // Constructor
            xroot.Members.Add(CodeDomHelper.CreateConstructor(MemberAttributes.Private));            

            //Load Methods
            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Load", new string[][]{new string[]{"System.String",  "xmlFile"}}));

            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Load", new string[][]{new string[]{"System.String",  "xmlFile"}, 
                                                                                             new string[]{"LoadOptions",  "options"}}));
            

            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Load", new string[][]{new string[]{"TextReader",  "textReader"}}));

            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Load", new string[][]{new string[]{"TextReader",  "textReader"}, 
                                                                                             new string[]{"LoadOptions",  "options"}}));

          
            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Load", new string[][]{new string[]{"XmlReader",  "xmlReader"}}));


            //Parse Methods
            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Parse", new string[][]{new string[]{"System.String",  "text"}}));

            xroot.Members.Add(CodeDomHelper.CreateXRootMethod(rootName, "Parse", new string[][]{new string[]{"System.String",  "text"}, 
                                                                                              new string[]{"LoadOptions",  "options"}}));


            //Save Methods
            xroot.Members.Add(CodeDomHelper.CreateXRootSave(new string[][]{new string[]{"System.String",  "fileName"}}));
            xroot.Members.Add(CodeDomHelper.CreateXRootSave(new string[][]{new string[]{"TextWriter",  "textWriter"}}));
            xroot.Members.Add(CodeDomHelper.CreateXRootSave(new string[][]{new string[]{"XmlWriter",  "writer"}}));

            xroot.Members.Add(CodeDomHelper.CreateXRootSave(new string[][]{new string[]{"TextWriter",  "textWriter"}, 
                                                                           new string[]{"SaveOptions",  "options"}}));
            xroot.Members.Add(CodeDomHelper.CreateXRootSave(new string[][]{new string[]{"System.String",  "fileName"}, 
                                                                           new string[]{"SaveOptions",  "options"}}));

            CodeMemberProperty prop = CodeDomHelper.CreateProperty("XDocument", 
                                                                    "XDocument",
                                                                    docField,
                                                                    MemberAttributes.Public,
                                                                    false);
            xroot.Members.Add(prop);

            for (int i = 0; i < elements.Count; i++)
            {
                string typeName = elements[i].Name;
                string fqTypeName = (namespaces == null || namespaces[i].Name == String.Empty) ? typeName : "global::" + namespaces[i].Name + "." + typeName;
                
                xroot.Members.Add(CodeDomHelper.CreateXRootFunctionalConstructor(fqTypeName));         
                xroot.Members.Add(CodeDomHelper.CreateXRootGetter(typeName, fqTypeName, lst));         
            }

            codeNamespace.Types.Add(xroot);
        }
        
        private void ProcessWrapperTypes() {
            if (wrapperRootElements == null) { //No Globalelements with global types
                return;
            }
            XWrapperTypedElementBuilder wrapperBuilder = new XWrapperTypedElementBuilder();
            XSimpleTypedElementBuilder simpleTypeBuilder = new XSimpleTypedElementBuilder();

            TypeBuilder builder = null;
            ClrPropertyInfo typedValPropertyInfo = null;
            foreach (ClrWrapperTypeInfo typeInfo in wrapperRootElements) { 
                SetFullTypeName(typeInfo, null);
                ClrTypeReference innerType = typeInfo.InnerType;
                if (innerType.IsSimpleType) {
                    typedValPropertyInfo = InitializeTypedValuePropertyInfo(typeInfo, typedValPropertyInfo, innerType);
                    simpleTypeBuilder.Init(typedValPropertyInfo.ClrTypeName, innerType.IsSchemaList);
                    simpleTypeBuilder.CreateTypeDeclaration(typeInfo);
                    simpleTypeBuilder.CreateFunctionalConstructor(typeInfo.Annotations);
                    typedValPropertyInfo.SetFixedDefaultValue(typeInfo);
                    simpleTypeBuilder.CreateProperty(typedValPropertyInfo, typeInfo.Annotations);
                    simpleTypeBuilder.AddTypeToTypeManager(elementDictionaryAddStatements, Constants.ElementDictionaryField);
                    simpleTypeBuilder.ApplyAnnotations(typeInfo);
                    builder = simpleTypeBuilder;
                }
                else {
                    string innerTypeName = null;
                    string innerTypeFullName = innerType.GetClrFullTypeName(typeInfo.clrtypeNs, nameMappings, out innerTypeName);
                    string innerTypeNs = innerType.Namespace;
                    
                    CodeNamespace innerTypeCodeNamespace = GetCodeNamespace(innerTypeNs);
                    CodeTypeDeclaration innerTypeDecl = GetCodeTypeDeclaration(innerTypeName, innerTypeCodeNamespace);
                    TypeAttributes innerTypeAttributes = TypeAttributes.Class;
                    if (innerTypeDecl != null) {
                        innerTypeAttributes = innerTypeDecl.TypeAttributes;
                    }
                    else if (innerTypeName != Constants.XTypedElement) {
                        continue;
                    }

                    currentNamespace = typeInfo.clrtypeNs;
                    wrapperBuilder.Init(innerTypeFullName, innerTypeNs, innerTypeAttributes);
                    wrapperBuilder.CreateTypeDeclaration(typeInfo);
                    wrapperBuilder.CreateFunctionalConstructor(typeInfo.Annotations);
                    wrapperBuilder.ApplyAnnotations(typeInfo);
                    wrapperBuilder.AddTypeToTypeManager(elementDictionaryAddStatements, wrapperDictionaryAddStatements);

                    if (!typeInfo.HasBaseContentType) { //Add innerType properties only if the wrapper's type is not the same as the substitutionGroup head type
                        ClrWrappingPropertyInfo wrappingPropertyInfo = null;
                         //Create forwarding properties
                        if (innerTypeName != Constants.XTypedElement) { //If the wrapped type is xs:anyType, no forwarding properties to create
                            wrappingPropertyInfo = new ClrWrappingPropertyInfo();
                                                  
                            foreach(CodeTypeMember member in innerTypeDecl.Members) {
                                CodeMemberProperty memberProperty = member as CodeMemberProperty;
                                if (ForwardProperty(memberProperty)) { //Do not forward over TypeManager, SchemaName etc
                                    wrappingPropertyInfo.Init(memberProperty);
                                    wrapperBuilder.CreateProperty(wrappingPropertyInfo, typeInfo.Annotations);
                                }
                            }
                        }
                    }                        
                    builder = wrapperBuilder;
                }
                builder.ImplementInterfaces(settings.EnableServiceReference);
                codeNamespace = GetCodeNamespace(typeInfo.clrtypeNs);
                codeNamespace.Types.Add(builder.TypeDeclaration);

                List<CodeTypeDeclaration> types;
                codeNamespace = GetCodeNamespace(typeInfo.clrtypeNs);

                if (!xroots.TryGetValue(codeNamespace, out types))
                {
                    types = new List<CodeTypeDeclaration>();
                    xroots.Add(codeNamespace, types);
                }

                types.Add(builder.TypeDeclaration);
                
            }
        }

        private void CreateTypeManager() {
            string rootClrNamespace = settings.GetClrNamespace(rootElementName.Namespace);
            CodeNamespace rootCodeNamespace = null;
            if(!codeNamespacesTable.TryGetValue(rootClrNamespace, out rootCodeNamespace)) { //This might happen if the schema set has no global elements and only global types
                rootCodeNamespace = codeNamespacesTable.Values.FirstOrDefault();                                    //then you can create a root tag with xsi:type 
            }
            if (rootCodeNamespace != null) { //It might be null if schema has only simple typed global elements or simple types which we are ignoring for now
                rootCodeNamespace.Types.Add(TypeBuilder.CreateTypeManager(rootElementName, settings.EnableServiceReference, typeDictionaryAddStatements, elementDictionaryAddStatements, wrapperDictionaryAddStatements));
                //Add using statements in the rest of the namespaces for the root namespace to avoid error on TypeManager reference
                //Add using statements in the root namespace for the rest of the namespaces to avoid errors while building type dictionaries
                CodeNamespaceImport rootImport = new CodeNamespaceImport(rootCodeNamespace.Name);
                foreach(CodeNamespace cns in codeNamespacesTable.Values) {
                    if (cns != rootCodeNamespace) {
                        if (rootCodeNamespace.Name.Length > 0) {
                            cns.Imports.Add(rootImport);
                        }
                        if (cns.Name.Length > 0) {
                            rootCodeNamespace.Imports.Add(new CodeNamespaceImport(cns.Name));
                        }
                    }
                }
            }
        }

        private bool ForwardProperty(CodeMemberProperty property) {
            return property != null && property.ImplementationTypes.Count == 0; //Its not an interface impl (IXMetaData)
        }

        private CodeTypeDeclaration GetCodeTypeDeclaration(string typeName, CodeNamespace innerTypeCodeNamespace) {
            if (innerTypeCodeNamespace == null) {
                return null;
            }
            CodeTypeDeclarationCollection types = innerTypeCodeNamespace.Types;
            foreach(CodeTypeDeclaration decl in types) {
                if (decl.Name.Equals(typeName)) {
                    return decl;
                }
            }
            return null;
        }

        private void AddDefaultImports(CodeNamespace newCodeNamespace) {
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Diagnostics"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml.Schema"));
            if (settings.EnableServiceReference) {
                newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml.Serialization"));
            }
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Xml.Linq"));
            newCodeNamespace.Imports.Add(new CodeNamespaceImport("Xml.Schema.Linq"));
        }

        private TypeBuilder GetTypeBuilder() {
            if (typeBuilder == null) {
                typeBuilder = new XTypedElementBuilder();
            }
            else {
                typeBuilder.Init();
            }
            return typeBuilder;
        }

        private TypeBuilder GetEmptyTypeBuilder() {
            if (emptyTypeBuilder == null) {
                emptyTypeBuilder = new XEmptyTypedElementBuilder();
            }
            else {
                emptyTypeBuilder.Init();
            }
            return emptyTypeBuilder;
        }
        
        private void SetFullTypeName(ClrTypeInfo typeInfo, string parentIdentifier) {
            if(parentIdentifier == null) {
                if(typeInfo.clrtypeNs == string.Empty)
                    currentFullTypeName = typeInfo.clrtypeName;
                else
                    currentFullTypeName = typeInfo.clrtypeNs + "." + typeInfo.clrtypeName;
            }
            else {
                currentFullTypeName = parentIdentifier + "." + typeInfo.clrtypeName;
            }
            typeInfo.clrFullTypeName = currentFullTypeName;
            XmlQualifiedName baseTypeName = typeInfo.BaseTypeName;
            if (baseTypeName != XmlQualifiedName.Empty) {
                string clrNamespace = settings.GetClrNamespace(baseTypeName.Namespace);
                string baseTypeIdentifier = null;
                if (nameMappings.TryGetValue(typeInfo.baseType, out baseTypeIdentifier)) {
                    typeInfo.baseTypeClrName = baseTypeIdentifier; 
                    typeInfo.baseTypeClrNs = clrNamespace;
                }
            }
            if (typeInfo.typeOrigin == SchemaOrigin.Element && (rootElementName.IsEmpty || typeInfo.IsRoot)) {
                rootElementName = new XmlQualifiedName(typeInfo.schemaName, typeInfo.schemaNs);
            }
        }

        private ClrPropertyInfo InitializeTypedValuePropertyInfo(ClrTypeInfo typeInfo, ClrPropertyInfo typedValPropertyInfo, ClrTypeReference innerType) {
            if (typedValPropertyInfo == null) {
                typedValPropertyInfo = new ClrPropertyInfo(Constants.SInnerTypePropertyName, string.Empty, Constants.SInnerTypePropertyName, Occurs.One);
                typedValPropertyInfo.Origin = SchemaOrigin.Text; 
            }
            else {
                typedValPropertyInfo.Reset(); 
            }
            typedValPropertyInfo.TypeReference = innerType;
            if (typeInfo.IsSubstitutionMember()) {
                typedValPropertyInfo.IsNew = true;
            }
            typedValPropertyInfo.UpdateTypeReference(currentFullTypeName, currentNamespace, nameMappings);
            return typedValPropertyInfo;
        }

        private CodeNamespace GetCodeNamespace(string clrNamespace) {
            if (codeNamespace != null && codeNamespace.Name == clrNamespace) {
                return codeNamespace;
            }
             
            CodeNamespace currentCodeNamespace = null;
            if(!codeNamespacesTable.TryGetValue(clrNamespace, out currentCodeNamespace)) {
                currentCodeNamespace = new CodeNamespace(clrNamespace);
                AddDefaultImports(currentCodeNamespace);
                codeNamespacesTable.Add(clrNamespace, currentCodeNamespace);
            }
            return currentCodeNamespace;
        }
    }

}

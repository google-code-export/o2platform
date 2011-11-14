//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------


namespace PasteXmlAsType
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.ServiceModel.Description;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    //using Microsoft.Silverlight.ServiceReference;
    using System.Xml.Linq;
    using System.Collections.Generic;
    //using FluentSharp.O2.Kernel;

    class Converter
    {
        private XmlSchemaSet schemas;
        public static readonly Converter SoleInstance = new Converter();

        public bool CanConvert(string xml)
        {
            bool isSchema = false;
            schemas = new XmlSchemaSet();
            List<XElement> instances = new List<XElement>();
            
            xml = xml.Trim();

            // Figure out if we have valid XML instance or XML schema
            try
            {
                instances.Add(XElement.Parse(xml));

                if (instances[0].Name.LocalName.Equals("schema") && instances[0].Name.NamespaceName.Equals("http://www.w3.org/2001/XMLSchema"))
                {
                    isSchema = true;
                }
            } 
            catch (XmlException)
            {
                try
                {
                    // Could be two separate XML instances back to back with no root node, check for that
                    XElement fakeRoot = XElement.Parse("<Root>" + xml + "</Root>");
                    foreach (XElement child in fakeRoot.Elements())
                    {
                        instances.Add(child);
                    }

                }
                catch (XmlException e)
                {
                    //RtlAwareMessageBox.Show(
//                    PublicDI.log.error("Could not parse clipboard content as a XML instance. Please ensure the clipboard contains a valid XML instance." + Environment.NewLine + Environment.NewLine + e.Message);
                    //, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                    return false;
                }
            }

            if (isSchema)
            {

                AddSchemasForPrimitiveTypes(schemas);

                try
                {
                    schemas.Add(null, instances[0].CreateReader());
                    schemas.Compile();
                }
                catch (Exception exception)
                {
                    if (exception is XmlException || exception is XmlSchemaException)
                    {
                        //RtlAwareMessageBox.Show
//                            PublicDI.log.error("Could not parse clipboard content as a XML Schema. Please ensure the clipboard contains valid XML Schema." +
  //                          Environment.NewLine + Environment.NewLine + exception.Message);
                        //, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                        return false;
                    }
                    else
                    {
                        // Rethrow unknown exceptions
                        throw;
                    }
                }


                RemoveComplexTypeSchemaIfNotUsed(schemas);

            }
            else
            {
                XmlSchemaInference inference = new XmlSchemaInference();

                try
                {
                    foreach (XElement element in instances)
                    {
                        schemas = inference.InferSchema(element.CreateReader(), schemas);
                    }

                    schemas.Compile();
                }
                catch (Exception exception)
                {
                    if (exception is XmlException || exception is XmlSchemaInferenceException)
                    {
                        //RtlAwareMessageBox.Show(
//                        PublicDI.log.error("Could not infer the structure of the XML instance on the clipboard. Please consider modifying the instance." + Environment.NewLine + Environment.NewLine + exception.Message);                    
                        //    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                        return false;
                    }
                    else
                    {
                        // Rethrow unknown exceptions
                        throw;
                    }
                }
            }

            return true;
        }

        // If the user schema is generated by WCF, it may contain references to some primitive WCF
        // types in the following namespaces. We need add those schemas to the schema set by default
        // so these types can be resolved correctly.
        //
        // * http://schemas.microsoft.com/2003/10/Serialization
        // * http://schemas.microsoft.com/2003/10/Serialization/Arrays
        // * http://microsoft.com/wsdl/types/
        // * http://schemas.datacontract.org/2004/07/System
        private void AddSchemasForPrimitiveTypes(XmlSchemaSet schemas)
        {
            // Add DCS special types
            XsdDataContractExporter dataContractExporter = new XsdDataContractExporter(schemas);
      
            // We want to export Guid, Char, and TimeSpan, however even a single call causes all
            // schemas to be exported.
            dataContractExporter.Export(typeof(Guid));

            // Export DateTimeOffset, DBNull, array types
            dataContractExporter.Export(typeof(DateTimeOffset));
            dataContractExporter.Export(typeof(DBNull));
            dataContractExporter.Export(typeof(bool[]));
            dataContractExporter.Export(typeof(DateTime[]));
            dataContractExporter.Export(typeof(decimal[]));
            dataContractExporter.Export(typeof(double[]));
            dataContractExporter.Export(typeof(float[]));
            dataContractExporter.Export(typeof(int[]));
            dataContractExporter.Export(typeof(long[]));
            dataContractExporter.Export(typeof(XmlQualifiedName[]));
            dataContractExporter.Export(typeof(short[]));
            dataContractExporter.Export(typeof(string[]));
            dataContractExporter.Export(typeof(uint[]));
            dataContractExporter.Export(typeof(ulong[]));
            dataContractExporter.Export(typeof(ushort[]));
            dataContractExporter.Export(typeof(Char[]));
            dataContractExporter.Export(typeof(TimeSpan[]));
            dataContractExporter.Export(typeof(Guid[]));
            // Arrays of DateTimeOffset and DBNull are not supported

            // Add XS special types

            // XmlSchemaExporter takes XmlSchemas so we need that temporarily
            XmlSchemas xmlSchemas = new XmlSchemas();
            XmlReflectionImporter importer = new XmlReflectionImporter();
            XmlSchemaExporter xmlExporter = new XmlSchemaExporter(xmlSchemas);
            xmlExporter.ExportTypeMapping(importer.ImportTypeMapping(typeof(Guid)));
            xmlExporter.ExportTypeMapping(importer.ImportTypeMapping(typeof(Char)));

            foreach (XmlSchema schema in xmlSchemas)
            {
                schemas.Add(schema);
            }
        }

        // DateTimeOffset and DBNull are complex types, so they would always get generated 
        // since we added them to the schema set. Here we see if it they are actually used and if not 
        // we remove them from the schema set.
        private void RemoveComplexTypeSchemaIfNotUsed(XmlSchemaSet schemas)
        {
            List<XmlQualifiedName> typesToLookFor = new List<XmlQualifiedName>();
            typesToLookFor.Add(new XmlQualifiedName("DateTimeOffset", "http://schemas.datacontract.org/2004/07/System"));
            typesToLookFor.Add(new XmlQualifiedName("DBNull", "http://schemas.datacontract.org/2004/07/System"));
            List<XmlQualifiedName> typesToRemove = new List<XmlQualifiedName>(typesToLookFor);


            foreach (XmlSchemaType type in schemas.GlobalTypes.Values)
            {
                if (type is XmlSchemaComplexType)
                {
                    XmlSchemaComplexType complexType = type as XmlSchemaComplexType;

                    if (complexType.ContentTypeParticle != null &&
                        complexType.ContentTypeParticle is XmlSchemaSequence)
                    {
                        foreach (XmlSchemaObject xmlSchemaObject in (complexType.ContentTypeParticle as XmlSchemaSequence).Items)
                        {
                            if (xmlSchemaObject is XmlSchemaElement)
                            {
                                XmlSchemaElement element = (xmlSchemaObject as XmlSchemaElement);

                                foreach (XmlQualifiedName name in typesToLookFor)
                                {
                                    if (element.SchemaTypeName.Equals(name))
                                    {
                                        typesToRemove.Remove(name);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (typesToRemove.Count != 0)
            {
                foreach (XmlSchema schema in schemas.Schemas())
                {
                    foreach (XmlQualifiedName name in typesToRemove)
                    {
                        if (schema.Elements.Contains(name))
                        {
                            schema.Items.Remove(schema.Elements[name]);
                        }

                        if (schema.SchemaTypes.Contains(name))
                        {
                            schema.Items.Remove(schema.SchemaTypes[name]);
                        }
                    }
                }
            }
        }

        private List<XmlQualifiedName> BuildDataContractArrayTypeList()
        {
            const string dataContractArrayNamespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
            List<XmlQualifiedName> list = new List<XmlQualifiedName>();

            list.Add(new XmlQualifiedName("ArrayOfboolean", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfdateTime", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfdecimal", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfdouble", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOffloat", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfint", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOflong", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfQName", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfshort", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfstring", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfunsignedInt", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfunsignedLong", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfunsignedShort", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfchar", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfduration", dataContractArrayNamespace));
            list.Add(new XmlQualifiedName("ArrayOfguid", dataContractArrayNamespace));

            return list;
        }

        public string Convert(CodeDomProvider codeProvider)
        {
            StringBuilder code = new StringBuilder();
            List<XmlQualifiedName> arrayTypeList = BuildDataContractArrayTypeList();

            try
            {
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                CodeNamespace namespace2 = new CodeNamespace();
                compileUnit.Namespaces.Add(namespace2);

                XmlCodeExporter codeExporter = new XmlCodeExporter(namespace2, compileUnit, codeProvider, CodeGenerationOptions.GenerateProperties, null);

                // XmlSchemaImporter needs XmlSchemas and not XmlSchemaSet
                XmlSchemas userSchemas = new XmlSchemas();
                foreach (XmlSchema schema in schemas.Schemas())
                {
                    userSchemas.Add(schema);
                }

                XmlSchemaImporter schemaImporter = new XmlSchemaImporter(userSchemas, CodeGenerationOptions.GenerateProperties, codeProvider, new ImportContext(new CodeIdentifiers(), false));

                foreach (XmlSchema schema in userSchemas)
                {
                    if (schema != null)
                    {
                        foreach (XmlSchemaElement element in schema.Elements.Values)
                        {
                            // Don't generate code for abstract elements or array types
                            if (!element.IsAbstract && !arrayTypeList.Contains(element.QualifiedName))
                            {
                                XmlTypeMapping mapping = schemaImporter.ImportTypeMapping(element.QualifiedName);
                                codeExporter.ExportTypeMapping(mapping);
                            }
                        }
                    }
                }

                CodeTypeDeclarationCollection types = namespace2.Types;
                if ((types == null) || (types.Count == 0))
                {
                    //RtlAwareMessageBox.Show(
//                        PublicDI.log.error("The XML instance or XML Schema is valid, but no classes could be generated to match. Please ensure it contains a root element with some nested elements inside of it.");
                    //, "Error",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
                }
                else
                {
                    CodeGenerator.ValidateIdentifiers(namespace2);

                    // Now we have to run Silverlight-specific fix-up
                    
                    //DC
//                    ServiceContractGenerator generator = new ServiceContractGenerator(compileUnit);
//                    WcfSilverlightCodeGenerationExtension fixupExtension = new WcfSilverlightCodeGenerationExtension();
//                    fixupExtension.ClientGenerated(generator);

                    using (StringWriter writer = new StringWriter(code, CultureInfo.CurrentCulture))
                    {
                        foreach (CodeTypeDeclaration type in namespace2.Types)
                        {
                            codeProvider.GenerateCodeFromType(type, writer, null);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                if (((exception is ThreadAbortException) || (exception is StackOverflowException)) || (exception is OutOfMemoryException))
                {
                    throw;
                }
                //RtlAwareMessageBox.Show(  //DC
//                PublicDI.log.error("The XML instance or XML Schema is valid, but no classes could be generated to match." + Environment.NewLine + Environment.NewLine + exception.Message);
                //, "Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0);
            }

            return code.ToString();
        }
    }
}


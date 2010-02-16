// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Evaluant.NLinq.Memory;
using O2.DotNetWrappers.O2Findings;
using O2.Interfaces.O2Findings;
using O2.Views.ASCX.classes;

namespace O2.External.Evaluant
{
    public class OzasmtLinq
    {
        public static DataTable getDataTableFromO2FindingsLinqQuery(List<object> dataToConvert, int maxNumberOfRecords,
                                                                    ref Dictionary<DataRow, object>
                                                                        mappingOfDataRowsToObjects)
        {
            mappingOfDataRowsToObjects = new Dictionary<DataRow, object>();
            var dataTable = new DataTable();
            try
            {
                // calculate Rows names
                // column names                
                PropertyInfo[] properties = null;
                foreach (object firstItem in dataToConvert)
                {
                    // only first time, others will follow
                    //if (dataTable.Columns.Count == 0)
                    {
                        switch (firstItem.GetType().Name)
                        {
                            case "String":
                            case "Byte":
                            case "UInt32":
                            case "Int32":
                                dataTable.Columns.Add(new DataColumn(firstItem.GetType().Name, firstItem.GetType()));
                                break;
                            case "Variant":
                                foreach (string key in ((Variant) firstItem).members.Keys)
                                {
                                    switch (key)
                                    {
                                        case "finding":
                                            // this 'finding' is an hardcoded keyword that I am using to represent a complete finding (this way we can also have results to save on queries that result in Variant results
                                            break; // don't show these
                                        case "getSource":
                                        case "o2Traces":
                                            dataTable.Columns.Add(new DataColumn(key, typeof (string)));
                                            break;
                                        default:
                                            dataTable.Columns.Add(new DataColumn(key,
                                                                                 ((Variant) firstItem).members[key].
                                                                                     GetType()));
                                            break;
                                    }
                                    // object keyValue = ((Variant)firstItem).members[key];                          
                                    // if (keyValue is List<O2Trace>)
                                    //     dataTable.Columns.Add(new DataColumn("o2Traces", keyType));
                                    // else
                                    //dataTable.Columns.Add(new DataColumn(key, keyType));

                                    //((Variant)firstItem).members[key].GetType()));
                                }
                                break;
                                /*  case "o2Trace":
                                dataTable.Columns.Add(new DataColumn("o2Trace",typeof(string)));
                                break;*/
                            default:
                                properties = (firstItem.GetType()).GetProperties();
                                foreach (PropertyInfo pi in properties)
                                {
                                    switch (pi.Name)
                                    {
                                        case "getSource":
                                        case "getSink":
                                        case "getLostSink":
                                        case "getKnownSink":
                                            break; // hide this ones
                                        case "confidence":
                                        case "severity":
                                            dataTable.Columns.Add(new DataColumn(pi.Name, typeof (string)));
                                            break;
                                        default:

                                            Type colType = pi.PropertyType;
                                            if ((colType.IsGenericType) &&
                                                (colType.GetGenericTypeDefinition() == typeof (Nullable<>)))
                                                colType = colType.GetGenericArguments()[0];
                                            dataTable.Columns.Add(new DataColumn(pi.Name, colType));
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                    break; // we only want the first one
                }

                // Could add a check to verify that there is an element 0
                foreach (object item in dataToConvert)
                {
                    DataRow dataRow = dataTable.NewRow();
                    mappingOfDataRowsToObjects.Add(dataRow, item);
                    // this will allow to save the data of the rows selected
                    switch (item.GetType().Name)
                    {
                        case "String":
                        case "Byte":
                        case "UInt32":
                        case "Int32":
                            dataRow[item.GetType().Name] = item;

                            break;

                        case "Variant":
                            foreach (string key in ((Variant) item).members.Keys)
                                switch (key)
                                {
                                    case "finding":
                                        // this 'finding' is an hardcoded keyword that I am using to represent a complete finding (this way we can also have results to save on queries that result in Variant results
                                        break; // don't show these
                                    case "o2Traces":
                                        dataRow[key] = ((List<IO2Trace>) ((Variant) item)[key]).Count > 0 ? "has smart trace" : "no trace";
                                        break;
                                    case "getSource":
                                        if (((Variant) item)[key] is IO2Trace)
                                        {
                                            var o2Trace = (IO2Trace) ((Variant) item)[key];
                                            dataRow[key] = o2Trace.signature;
                                        }
                                        else
                                            dataRow[key] = "..........";
                                        break;
                                    default:
                                        dataRow[key] = ((Variant) item)[key];
                                        break;
                                }

                            break;
                        default:

                            if (properties != null)
                                foreach (PropertyInfo pi in properties)
                                {
                                    switch (pi.Name)
                                    {
                                        case "getSource":
                                        case "getSink":
                                        case "getLostSink":
                                        case "getKnownSink":
                                            break; // hide this ones
                                        case "severity":
                                            dataRow[pi.Name] = OzasmtUtils.getSeverityFromId((byte) pi.GetValue(item, null));
                                            break;                                            
                                        case "confidence":
                                            dataRow[pi.Name] = OzasmtUtils.getConfidenceFromId((byte)pi.GetValue(item, null));                                                                                       

                                            break;
                                        case "o2Traces":
                                            dataRow[pi.Name] = ((List<IO2Trace>) pi.GetValue(item, null)).Count > 0 ? pi.GetValue(item, null) : DBNull.Value;
                                            break;
                                        default:
                                            dataRow[pi.Name] = pi.GetValue(item, null) ?? DBNull.Value;
                                            break;
                                    }
                                }
                            /*
                            {
                                if (pi.GetValue(rec, null) == null)
                                    dr[pi.Name] = DBNull.Value;
                                else
                                {
                                    dr[pi.Name] = pi.GetValue(rec, null);
                                }
                            }*/
                            break;
                    }
                    //
                    dataTable.Rows.Add(dataRow);
                    if (dataTable.Rows.Count > maxNumberOfRecords)
                        break;
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, " in getDataTableFromO2FindingsLinqQuery ", true);
            }
            return dataTable;
        }

        public static void populateComboBoxWithO2FindingsLinqScriptLibraryTitles(ComboBox comboBox)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create(new StringReader(O2CoreResources.o2FindingsScriptLibrary));
                XDocument scriptsLibrary = XDocument.Load(xmlReader);
                IEnumerable<string> scriptTitles =
                    scriptsLibrary.Descendants("script").Select(script => script.Attribute("title").Value);
                foreach (string title in scriptTitles)
                    comboBox.Items.Add(title);
            }
            catch (Exception ex)
            {
                DI.log.ex(ex);
            }
        }

        //todo: there must be a better was to get these values from the Xml document :)
        public static string getO2FindingLinqScript(string scriptName)
        {
            XDocument scriptsLibrary =
                XDocument.Load(XmlReader.Create(new StringReader(O2CoreResources.o2FindingsScriptLibrary)));

            IEnumerable<string> scriptTitles =
                scriptsLibrary.Descendants("script").Where(script => script.Attribute("title").Value == scriptName).
                    Select(script => script.Value);
            if (scriptTitles.Count() == 1)
            {
                foreach (string value in scriptTitles)
                    return value;
            }
            return "";
        }
    }
}

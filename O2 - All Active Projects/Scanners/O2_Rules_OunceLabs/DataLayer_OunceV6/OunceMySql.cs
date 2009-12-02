using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using O2.DotNetWrappers.Windows;
using O2.Rules.OunceLabs.DataLayer;


// driver downloaded from http://dev.mysql.com/downloads/connector/net/5.0.html

namespace O2.Rules.OunceLabs.DataLayer_OunceV6
{
    public class OunceMySql
    {
        public static MySqlConnection msConnection { get; set; }
        public static int iMaxRecords = 200;
        public static String MySqlDatabaseName { get; set;}
        public static String MySqlLoginUsername { get; set; }
        public static String MySqlLoginPassword { get; set; }
        public static String MySqlServerPort { get; set; }
        public static String MySqlServerIP { get; set; }
        

        static OunceMySql()
        {
            iMaxRecords = 200;
            MySqlDatabaseName = "ounce";
            MySqlLoginPassword = "";
            MySqlServerPort = "13106";
            MySqlServerIP = "127.0.0.1";
            MySqlLoginUsername = "root";
        }

        private static String getLddbConnectionString()
        {
            return String.Format("server={0};port={1};uid={2};pwd={3};database={4}", MySqlServerIP, MySqlServerPort, MySqlLoginUsername,
                                 MySqlLoginPassword, MySqlDatabaseName);
        }

        public static bool setMySqlLoginDetails(string username, string password)
        {
            MySqlLoginUsername = username;
            MySqlLoginPassword = password;
            return refreshDbConnection();
        }

        public static bool refreshDbConnection()
        {
            if (msConnection != null && msConnection.State == ConnectionState.Open)
                msConnection.Close();
            return createConnectionToOunceDatabase();
        }

        public static bool isConnectionOpen()
        {
            return createConnectionToOunceDatabase();
        }   

        public static bool createConnectionToOunceDatabase()
        {
            if (msConnection != null && msConnection.State == ConnectionState.Open)
                return true;
            //if (msConnection==null ||msConnection.State !=ConnectionState.Open) // msConnection.State == ConnectionState.Closed)
            //{
            try
            {
                msConnection = new MySqlConnection(getLddbConnectionString());
                msConnection.Open();
                DI.log.debug("Sucessfully connected to local lddb Database");
                return true;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "In createConnectionToOunceDatabase, Could not connected to local lddb Database");
                return false;
            }
            //}
        }

        

        public static MySqlDataReader executeSqlQueryReturnSqlDataReader(String sQueryToExecute)
        {
            createConnectionToOunceDatabase();
            try
            {
                var msComand = new MySqlCommand(sQueryToExecute, msConnection);
                var mySqlDataReader = msComand.ExecuteReader();
                msComand.Dispose();
                return mySqlDataReader;
            }
            catch (Exception ex)
            {
                DI.log.error("OunceMySql.executeSqlQuery : {0}", ex.Message);
                return null;
            }
        }

        public static object executeSqlQuery(String sQueryToExecute)
        {
            return executeSqlQuery(sQueryToExecute, true);
        }

        public static object executeSqlQuery(String sQueryToExecute, bool bShowDebugInfo)
        {
            createConnectionToOunceDatabase();
            try
            {
                var msComand = new MySqlCommand(sQueryToExecute, msConnection);
                object oResult = msComand.ExecuteScalar();
                msComand.Dispose();
                return oResult;
            }
            catch (Exception ex)
            {
                if (bShowDebugInfo)
                    DI.log.error("OunceMySql.executeSqlQuery : {0}", ex.Message);
                return null;
            }
        }

        public static DataTable getDataTableFromSqlQuery(String sQueryToExecute)
        {
            return getDataTableFromSqlQuery(sQueryToExecute, false);
        }

        public static DataTable getDataTableFromSqlQuery(String sQueryToExecute, bool bShowDebugInfo)
        {
            createConnectionToOunceDatabase();
            if (sQueryToExecute == "")
                return null;
            if (bShowDebugInfo)
                DI.log.info("Executing SQL Query: {0}", sQueryToExecute);
            var msDataAdapter = new MySqlDataAdapter(sQueryToExecute, msConnection);

            var dtDataTable = new DataTable("o2.datalayer.mysql.OunceMySql.getDataTableFromSqlQuery");
            try
            {
                // populate DataTable with data from database
                msDataAdapter.Fill(0, iMaxRecords, dtDataTable);
                if (dtDataTable.Rows.Count == 0)
                {
                    if (bShowDebugInfo)
                        DI.log.error("No records returned by query");
                }
                else
                {
                    if (bShowDebugInfo)
                        DI.log.info("{0} records fetched", dtDataTable.Rows.Count.ToString());

                    // check if we received the maximum amount of records allowed (for performance reasons)
                    if (dtDataTable.Rows.Count == iMaxRecords)
                        DI.log.error(
                            "The number of records fetchs was equal to the current iMaxRecords ({0}).This means that not all data was fetched",
                            dtDataTable.Rows.Count.ToString());
                }
            }
            catch (MySqlException msException)
            {
                DI.log.error("MySql Exception : {0}", msException.Message);
            }
            return dtDataTable;
        }

        public static void runQueryAndPopulateDataGrid(DataGridView dgvToPopulate, String sQueryToExecute)
        {
            dgvToPopulate.Columns.Clear();
            //forms.executeMethod_inControl_ThreadSafeWay(dgvToPopulate, dgvToPopulate.Columns, "Clear", new object[] { });
            DataTable dtDataTable = getDataTableFromSqlQuery(sQueryToExecute, false);
            // if dtDataTable contains data, bind it to DataGridView
            if (dtDataTable != null && dtDataTable.Rows.Count > 0)
            {
                // I wanted to make this assigment in a threadSafeWay but the DataGridView doesn't seem to recorgnize the different threat call to set_DataSource
                //     forms.executeMethod_inControl_ThreadSafeWay(dgvToPopulate, dgvToPopulate, "set_DataSource", new object[] { dtDataTable });
                //     forms.executeMethod_inControl_ThreadSafeWay(dgvToPopulate, dgvToPopulate, "Refresh", new object[] { });                
                dgvToPopulate.DataSource = dtDataTable;
            }
        }

        public static void runQueryAndPopulateListBoxWithFirstColumn(ListBox lbListBoxToPopulate, String sQueryToExecute)
        {
            DataTable dtDataTable = getDataTableFromSqlQuery(sQueryToExecute, true);

            // if dtDataTable contains data, bind it to DataGridView
            if (dtDataTable.Rows.Count > 0)
            {
                foreach (DataRow drDataRow in dtDataTable.Rows)
                {
                    object[] oData = drDataRow.ItemArray;
                    if (oData.Length > 0)
                        lbListBoxToPopulate.Items.Add(oData[0].ToString());
                }
            }
            if (lbListBoxToPopulate.Items.Count > 0)
                lbListBoxToPopulate.SelectedIndex = 0;
        }

        public static void populateDataGridViewWithLddbData(String sLddbTable, String sDbId, String sQueryVariableName,
                                                            String sQueryValue, DataGridView dgvCustomRules)
        {
            String sSql = String.Format("Select * from {0} where {1} = {2}", sLddbTable, sQueryVariableName, sQueryValue);
            if (sDbId != "")
                sSql += String.Format(" and db_id = {0}", sDbId);
            runQueryAndPopulateDataGrid(dgvCustomRules, sSql);
        }

        public static void populateDataGridViewWithCustomRules(DataGridView dgvCustomRules)
        {
            String sSqlToFetchCustomRules = Lddb_OunceV6.getSqlQueryStringToSeeCustomRules();
            runQueryAndPopulateDataGrid(dgvCustomRules, sSqlToFetchCustomRules);
        }

        public static void populateDataGridView_ExistentActionsObjectsForSignature(String sDbId, String sSignature,
                                                                                   DataGridView dgvDataGridView,
                                                                                   bool bVerbose)
        {
            dgvDataGridView.Columns.Clear();
            UInt32 uVuln_id = Lddb_OunceV6.action_getVulnIdThatMatchesSignature(sDbId, sSignature, bVerbose);
            if (uVuln_id == 0)
                return;
            populateDataGridView_ExistentActionsObjectsForVulnId(uVuln_id, dgvDataGridView);
        }


        public static void populateDataGridView_ExistentActionsObjectsForVulnId(UInt32 uVulnId,
                                                                                DataGridView dgvDataGridView)
        {
            String sSQl =
                String.Format(
                    "Select vuln_type, signature, severity, type,trace,added, db_id,vuln_id,id from actionobjects where vuln_id = {0}",
                    uVulnId);
            runQueryAndPopulateDataGrid(dgvDataGridView, sSQl);
        }

        public static void populateDataGridView_MethodSignature(String sDbId, String sSignature,
                                                                DataGridView dgvDataGridView)
        {
            UInt32 uVulnId = Lddb_OunceV6.action_getVulnIdThatMatchesSignature(sDbId, sSignature, false);
            if (uVulnId == 0)
                O2Forms.executeMethodThreadSafe(dgvDataGridView, dgvDataGridView.Rows, "Clear", new object[] {});
            else
                populateDataGridView_ExistentActionsObjectsForVulnId(uVulnId, dgvDataGridView);
        }

        public static void populateDataGridView_ExistentCallbacks(DataGridView dgvDataGridView)
        {
            String sSQl = String.Format("Select * from rec where callback = 1");
            runQueryAndPopulateDataGrid(dgvDataGridView, sSQl);
        }

        public static DataTable getDataTableWith_Callbacks()
        {
            String sSQl = String.Format("Select * from rec where callback = 1");
            return getDataTableFromSqlQuery(sSQl);
        }

        public static void populateDataGridView_ExistentActionsObjectsForActionObject(UInt32 uActionObject,
                                                                                      DataGridView dgvDataGridView)
        {
            String sSQl =
                String.Format(
                    "Select vuln_type, signature, severity, type,trace,added, db_id, vuln_id, id from actionobjects where id = {0}",
                    uActionObject);
            runQueryAndPopulateDataGrid(dgvDataGridView, sSQl);
        }

        public static void populateListWithDataTable(String sSqlQuery, Object oTargetListObject)
        {
            DataTable dtResults = getDataTableFromSqlQuery(sSqlQuery, false);
            Type tListObjectType = oTargetListObject.GetType().GetGenericArguments()[0];
            // get the first list type which will be used to autopopulate				
            foreach (DataRow drRows in dtResults.Rows)
            {
                Object oListObject = Activator.CreateInstance(tListObjectType);
                foreach (FieldInfo fField in oListObject.GetType().GetFields())
                {
                    String sFieldName = fField.Name.Replace("clazz", "class");
                    //if (fField.Name != "actionObjects")			// don't auto populate this one
                    try
                    {
                        if (drRows[sFieldName].ToString() == "System.Byte[]")
                            fField.SetValue(oListObject, Encoding.ASCII.GetString((byte[])drRows[sFieldName]));
                        else
                            fField.SetValue(oListObject, drRows[sFieldName].ToString());
                    }
                    catch (Exception)
                    {
                    }
                    // don't worry about the fiels that could not be binded
                }
                DI.reflection.invokeMethod_InstanceStaticPublicNonPublic(oTargetListObject, "Add", new[] { oListObject });
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace O2.Rules.OunceLabs.DataLayer_OunceV6
{
    public class Prexis
    {
        public static String getProjectFilePath(String sProjectName)
        {
            //o2.datalayer.mysql.OunceMySql.
            DataTable dtResults =
                OunceMySql.getDataTableFromSqlQuery(getSqlQueryForRetrivingProjectFilePath(sProjectName), false);
            if (dtResults.Rows.Count == 1)
            {
                var sProjectFile = (String) dtResults.Rows[0].ItemArray[0];
                return Path.GetDirectoryName(sProjectFile);
            }
            return "";
        }


        public static String getSqlQueryForRetrivingProjectFilePath(String sProjectName)
        {
            return "Select file_path from prexis.project where name = '" + sProjectName.Trim() + "'";
        }

        public static String[] getApplications()
        {
            String sSql = "SELECT File_Path FROM prexis.application";
            DataTable dtResult = OunceMySql.getDataTableFromSqlQuery(sSql, false);
            if (dtResult != null)
            {
                var lsApplications = new List<string>();
                foreach (DataRow drRow in dtResult.Rows)
                    lsApplications.Add(drRow["File_Path"].ToString());
                return lsApplications.ToArray();
            }
            return new String[] {};
        }
    }
}
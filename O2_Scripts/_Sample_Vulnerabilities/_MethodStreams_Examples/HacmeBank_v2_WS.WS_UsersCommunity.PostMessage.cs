using System;
using System.Data.SqlClient;
namespace HacmeBank_v2_WS
{
	public class WS_UsersCommunity
	{
		[WebMethod()]
		public void PostMessage(string sessionID, string userID, string messageSubject, string messageText)
		{
			HacmeBank_v2_WS.DataFactory.PostMessage(userID, messageSubject, messageText);
		}
	}
	public class DataFactory
	{
		public static void PostMessage(string userID, string messageSubject, string messageText)
		{
			SqlServerEngine.executeSQLCommand("Insert into fsb_messages " + "(user_id,message_date,subject,text) " + "Values " + "('" + userID + "','" + DateTime.Now + "','" + messageSubject + "','" + messageText + "')");
		}
	}
	public class SqlServerEngine
	{
		public static int executeSQLCommand(string sqlQueryToExecute)
		{
			Global.createSqlServerConnection();
			string text1 = sqlQueryToExecute;
			SqlCommand command1 = new SqlCommand(text1, Global.globalSqlServerConnection);
			Global.globalSqlServerConnection.Open();
			int executeNonQuery_Result = command1.ExecuteNonQuery();
			Global.globalSqlServerConnection.Close();
			return executeNonQuery_Result;
		}
	}
	public class Global
	{
		public static void createSqlServerConnection()
		{
			try {
				globalSqlServerConnection.Close();
			} catch {
			}
			globalSqlServerConnection = new SqlConnection(ConfigurationSettings.AppSettings.Get("FoundStone_Connection"));
		}
	}
}
namespace System.Data.SqlClient
{
	public class SqlConnection
	{
		public virtual void Close()
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
		public SqlConnection(string connectionString)
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
		public virtual void Open()
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
	public class SqlCommand
	{
		public virtual int ExecuteNonQuery()
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
		public SqlCommand(string cmdText, SqlConnection connection)
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
}

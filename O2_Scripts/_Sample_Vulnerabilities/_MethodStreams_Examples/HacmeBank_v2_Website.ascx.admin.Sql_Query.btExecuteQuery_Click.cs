using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Services.Protocols;
namespace HacmeBank_v2_Website.ascx.admin
{
	public class Sql_Query : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.DataGrid dgQueryResult;
		protected System.Web.UI.WebControls.TextBox txtSqlQueryToExecute;
		protected void btExecuteQuery_Click(object sender, System.EventArgs e)
		{
			txtSqlQueryToExecute = sender;
			populateDataGridWithSqlQueryResults();
		}
		private void populateDataGridWithSqlQueryResults()
		{
			try {
				string sqlQueryToexecute = Server.HtmlDecode(txtSqlQueryToExecute.Text);
				XmlNode[] sqlQueryResults = (XmlNode[])Global.objAccountManagement.ExecuteSqlQuery("", sqlQueryToexecute);
				if (sqlQueryResults[0].ChildNodes.Count > 0) {
					DataTable dataTableWithSqlQueryResults = new DataTable();
					for (int i = 0; i < sqlQueryResults[0].ChildNodes.Count; i++) {
						XmlNode resultItem = sqlQueryResults[0].ChildNodes[i];
						BoundColumn dynamicDataGridColumn = new BoundColumn();
						dynamicDataGridColumn.DataField = i.ToString();
						dynamicDataGridColumn.HeaderText = resultItem.InnerText;
						dgQueryResult.Columns.Add(dynamicDataGridColumn);
						dataTableWithSqlQueryResults.Columns.Add(i.ToString());
					}
					if (sqlQueryResults.Length > 1) {
						for (int j = 1; j < sqlQueryResults.Length; j++) {
							object[] rowData = new object[sqlQueryResults[j].ChildNodes.Count];
							for (int i = 0; i < sqlQueryResults[j].ChildNodes.Count; i++) {
								XmlNode resultItem = sqlQueryResults[j].ChildNodes[i];
								rowData[i] = Server.HtmlEncode(resultItem.InnerText);
							}
							dataTableWithSqlQueryResults.Rows.Add(rowData);
						}
					}
					dgQueryResult.DataSource = dataTableWithSqlQueryResults;
					dgQueryResult.DataBind();
				}
			} catch (Exception Ex) {
				lblErrorMessage.Text = Ex.Message;
			}
		}
	}
}
public class WS_AccountManagement : System.Web.Services.Protocols.SoapHttpClientProtocol
{
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ExecuteSqlQuery", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public object[] ExecuteSqlQuery(string SessionID, string sqlQueryToExecute)
	{
		object[] results = this.Invoke("ExecuteSqlQuery", new object[] {
			SessionID,
			sqlQueryToExecute
		});
		return ((object[])(results[0]));
	}
}
namespace System.Web.Services.Protocols
{
	public class SoapHttpClientProtocol : System.Web.Services.Protocols.HttpWebClientProtocol, System.ComponentModel.IComponent, System.IDisposable
	{
		protected object[] Invoke(string methodName, object[] parameters)
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
	public class SoapParameterStyle : System.Enum, System.IComparable, System.IFormattable, System.IConvertible
	{
		//public static const System.Web.Services.Protocols.SoapParameterStyle Wrapped;
	}
}
namespace System.Web.UI.WebControls
{
	public class BoundColumn : System.Web.UI.WebControls.DataGridColumn, System.Web.UI.IStateManager
	{
		public virtual string DataField {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		public BoundColumn()
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
	public class DataGridColumn : System.Web.UI.IStateManager
	{
		public virtual string HeaderText {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
	}
}
namespace System.Web
{
	public class HttpServerUtility
	{
		public string HtmlDecode(string s)
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
		public string HtmlEncode(string s)
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
}
namespace System
{
	public class Int32 : System.ValueType, System.IComparable, System.IFormattable, System.IConvertible, System.IComparable, System.IEquatable
	{
		public virtual string ToString()
		{
			throw new System.Exception("O2 Auto Generated Method");
		}
	}
	public class Exception : System.Runtime.Serialization.ISerializable, System.Runtime.InteropServices._Exception
	{
		public virtual string Message {
			get {
				throw new NotImplementedException();
			}
		}
	}
}
namespace System.Web.Services.Description
{
	public class SoapBindingUse : System.Enum, System.IComparable, System.IFormattable, System.IConvertible
	{
		//public static const System.Web.Services.Description.SoapBindingUse Literal;
	}
}
namespace HacmeBank_v2_Website
{
	public class Global : System.Web.HttpApplication
	{
		public static WS_AccountManagement objAccountManagement;
	}
}
namespace System.Web.UI
{
	public class UserControl : System.Web.UI.TemplateControl, System.ComponentModel.IComponent, System.IDisposable, System.Web.UI.IParserAccessor, System.Web.UI.IUrlResolutionService, System.Web.UI.IDataBindingsAccessor, System.Web.UI.IControlBuilderAccessor, System.Web.UI.IControlDesignerAccessor, System.Web.UI.IExpressionsAccessor, System.Web.UI.INamingContainer, System.Web.UI.IFilterResolutionService, System.Web.UI.IAttributeAccessor, System.Web.UI.INonBindingContainer, System.Web.UI.IUserControlDesignerAccessor
	{
		public System.Web.HttpServerUtility Server {
			get {
				throw new NotImplementedException();
			}
		}
	}
}

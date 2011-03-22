// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Views.ASCX.ExtensionMethods;
//O2Ref:System.Data.dll
using O2.XRules.Database.Utils;
//O2File:_Extra_methods_To_Add_to_Main_CodeBase.cs

namespace O2.XRules.Database.APIs
{
    public class API_SqlServer
    {   
    	public string ConnectionString { get;set; }
    	
    	public API_SqlServer()
		{
			ConnectionString = @"Data Source=.\SQLExpress;Trusted_Connection=True";	 //default to this one			
		}		
	}
	
	public class Database
	{
		public API_SqlServer SqlServer { get; set; }
		public string Name { get; set; }
		public List<Table> Tables { get; set; }
		public List<StoredProcedure> StoredProcedures { get; set; }
		
		public Database(string name)
		{
			Name = name;
			Tables = new List<Table>();
			StoredProcedures = new List<StoredProcedure>();
		}
		
		public Database(API_SqlServer sqlServer, string name) : this (name)
		{
			SqlServer = sqlServer;
		
		}
	}
	
	public class Table
	{
		public API_SqlServer SqlServer { get; set; }		
		public string Catalog {get;set;}
		public string Schema {get;set;}
		public string Name {get;set;}
		public string Type {get;set;}
		public List<Table_Column> Columns {get;set;}
		
		public DataTable TableData { get; set; }
		
		public Table()
		{
			Columns = new List<Table_Column>();
		}
		
		public override string ToString()
		{
			return (Schema.valid()) 
						? "{0}.{1}".format(Schema, Name)
						: "Name";
		}
	}
	
	public class Table_Column
	{		
		public string Name { get; set; }		
		public string Default { get; set; }		
		public string IsNulable { get; set; }		
		public string DataType { get; set; }		
		public string CharMaximumLength	 { get; set; }		
		
		public override string ToString()
		{
			return "{0} ({1})".format(Name, DataType);
		}
	}
	
	public class StoredProcedure
	{
		public string Schema {get;set;}
		public string Name {get;set;}
		public string Value {get;set;}
		
		public StoredProcedure(string schema, string name, string value)
		{
			Schema = schema;
			Name = name;
			Value = value;
		}
		
		public StoredProcedure(string name, string value) : this("",name, value) 
		{
			
		}
		
		public override string ToString()
		{
			return (Schema.valid()) 
						? "{0}.{1}".format(Schema, Name)
						: "Name";
		}
	}
	
	//add these queries should be done using Linq
	public static class API_SqlServer_Helps
	{
		public static Database database(this API_SqlServer sqlServer, string name)
		{
			return new Database(sqlServer, name);
		}
	}
	public static class API_SqlServer_getData
	{
		public static List<string> database_Names(this API_SqlServer sqlServer)
		{
			var names = new List<string>();
			var sqlQuery = "select name from master..sysDatabases"; 
			foreach(DataRow row in sqlServer.executeReader(sqlQuery).Rows)
				names.add(row.ItemArray[0].str());
			return names;		
		}
		
		public static List<string> column_Names(this Table table)
		{
			return (from column in table.columns()
				   	select column.Name).toList();
		}
		
		public static List<Table> tables(this Database database)
		{
			return database.Tables;
		}
		
		public static List<Table_Column> columns(this Table table)
		{
			return table.Columns;
		}
	}
	
	public static class API_SqlServer_PopulateData
	{
		public static Database map_StoredProcedures(this Database database)
		{		
			var sqlQuery = "select Specific_Schema, Specific_Name, Routine_Definition  from {0}.Information_Schema.Routines".format(database.Name);
			var storedProceduresData = database.SqlServer.executeReader(sqlQuery);			
			foreach(DataRow row in storedProceduresData.Rows)
				database.StoredProcedures.Add(new StoredProcedure(row.ItemArray[0].str(),row.ItemArray[1].str(),row.ItemArray[2].str())); 
			return database;
		}	
		
		public static Database map_Tables(this Database database)
		{		
			var sqlQuery = "select * from {0}.Information_Schema.Tables".format(database.Name);
			var tables = database.SqlServer.executeReader(sqlQuery);			
			foreach(DataRow row in tables.Rows)
				database.Tables.Add(new Table(){
													SqlServer = database.SqlServer,
													Catalog = row.ItemArray[0].str(),
													Schema = row.ItemArray[1].str(),
													Name = row.ItemArray[2].str(),
													Type = row.ItemArray[3].str()
													});
			return database;
		}	
		
		public static Database map_Table_Columns(this Database database)
		{		
			foreach(var table in database.tables())
			{			
				var sqlQuery = "select Column_Name, Column_Default, Is_Nullable, Data_Type, Character_Maximum_Length from {0}.Information_Schema.Columns where table_Schema='{1}' and table_name='{2}'"
									.format(table.Catalog, table.Schema,table.Name);
				
	 			
				var columns = database.SqlServer.executeReader(sqlQuery);			
				
				foreach(DataRow row in columns.Rows)
					table.Columns.Add(new Table_Column(){
														Name =  row.ItemArray[0].str(),
														Default = row.ItemArray[1].str(),
														IsNulable = row.ItemArray[2].str(),
														DataType = row.ItemArray[3].str(),
														CharMaximumLength = row.ItemArray[4].str()
														});
			}
			return database;
		}
		
		public static Database map_Table_Data(this Database database)
		{
			"Mapping table data".info();
			var timer = new O2Timer("Mapped tabled data").start();
			foreach(var table in database.tables())
			{					
				var sqlQuery = "select * from [{0}].[{1}].[{2}]".format(table.Catalog,table.Schema, table.Name);
				
				table.TableData = database.SqlServer.executeReader(sqlQuery);
				//"Table {0} had {1} rows".info(table.Name, table.TableData.Rows.Count);
			}
			timer.stop();
			return database;
				
		}

	}
	public static class API_SqlServer_Queries
	{
		public static API_SqlServer executeNonQuery(this API_SqlServer sqlServer, string command)
		{
			SqlConnection sqlConnection = new SqlConnection(sqlServer.ConnectionString);
			sqlConnection.Open();
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = command;
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				ex.log();
			}
			finally
			{
				sqlConnection.Close();
			}
			return sqlServer;
		}
		
		public static object executeScalar(this API_SqlServer sqlServer, string command)
		{
			SqlConnection sqlConnection = new SqlConnection(sqlServer.ConnectionString);
			sqlConnection.Open();
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = command;
				sqlCommand.CommandType = CommandType.Text;
				return sqlCommand.ExecuteScalar();
			}
			catch(Exception ex)
			{
				ex.log();
			}
			finally
			{
				sqlConnection.Close();
			}
			return null;
		}
		
		public static DataTable executeReader(this API_SqlServer sqlServer, string command)
		{
			SqlConnection sqlConnection = new SqlConnection(sqlServer.ConnectionString);
			sqlConnection.Open();
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = command;
				sqlCommand.CommandType = CommandType.Text;
				var reader =  sqlCommand.ExecuteReader();
				var dataTable = new DataTable();
				dataTable.Load(reader);
				return dataTable;
			}
			catch(Exception ex)
			{
				ex.log();
			}
			finally
			{
				sqlConnection.Close();
			}
			return null;
		}
	}
		
	public static class API_SqlServer_GUI_Controls
    {
		public static T add_ConnectionStringTester<T>(this API_SqlServer sqlServer , T control)
			where T : Control
		{
			var connectionString = control.add_GroupBox("Connection String").add_TextArea();
			var connectionStringSamples = connectionString.parent().insert_Left<Panel>(200).add_GroupBox("Sample Connection Strings")
														  .add_TreeView()
														  .afterSelect<string>((text)=> connectionString.set_Text(text));
			var connectPanel = connectionString.insert_Below<Panel>(200);
			var button = connectPanel.insert_Above<Panel>(25).add_Button("Connect").fill();  
			var response = connectPanel.add_GroupBox("Response").add_TextArea();						
			
			button.onClick(()=>{
									try
									{ 
										var text = connectionString.get_Text();
										sqlServer.ConnectionString = text;
										response.set_Text("Connecting using: {0}".format(text));
										var sqlConnection = new SqlConnection(text);
										sqlConnection.Open();
										response.set_Text("Connected ok");
									}
									catch(Exception ex)
									{
										response.set_Text("Error: {0}".format(ex.Message));
									}						
									
								});
			
			connectionString.set_Text(@"Data Source=.\SQLExpress;Trusted_Connection=True"); 
			var sampleConnectionStrings = new List<string>();
			//from http://www.connectionstrings.com/sql-server-2005
			sampleConnectionStrings.add(@"Data Source=.\SQLExpress;Trusted_Connection=True")
								   .add(@"Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI")												   
								   .add(@"Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;")
								   .add(@"Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;")
								   .add(@"Server=.\SQLExpress;AttachDbFilename=c:\mydbfile.mdf;Database=dbname; Trusted_Connection=Yes;")
								   .add(@"Server=.\SQLExpress;AttachDbFilename=|DataDirectory|mydbfile.mdf; Database=dbname;Trusted_Connection=Yes;")
								   .add(@"Data Source=.\SQLExpress;Integrated Security=true; AttachDbFilename=|DataDirectory|\mydb.mdf;User Instance=true;");
								   
			connectionStringSamples.add_Nodes(sampleConnectionStrings);  			
			return control;
		}
				
		
		public static API_SqlServer add_Viewer_QueryResult<T>(this API_SqlServer sqlServer , T control, string sqlQuery)
			where T : Control
		{	
			var dataTable = sqlServer.executeReader(sqlQuery);  
			var dataGridView = control.add_DataGridView();
			dataGridView.DataError+= (sender,e) => { " dataGridView error: {0}".error(e.Context);};
			dataGridView.invokeOnThread(()=> dataGridView.DataSource = dataTable );			
			return sqlServer;
		}
		
		public static API_SqlServer add_Viewer_DataBases<T>(this API_SqlServer sqlServer , T control)
			where T : Control
		{
			var sqlQuery = "select * from master..sysDatabases"; 
			return sqlServer.add_Viewer_QueryResult(control, sqlQuery);
		}
		
		public static API_SqlServer add_Viewer_Tables_Raw<T>(this API_SqlServer sqlServer , T control, string databaseName)
			where T : Control
		{
			var sqlQuery = "select * from {0}.Information_Schema.Tables".format(databaseName); 
			return sqlServer.add_Viewer_QueryResult(control, sqlQuery);
		}
		
		public static API_SqlServer add_Viewer_StoredProcedures_Raw<T>(this API_SqlServer sqlServer , T control, string databaseName)
			where T : Control
		{
		
			var sqlQuery = "select * from {0}.Information_Schema.Routines".format(databaseName); 
			return sqlServer.add_Viewer_QueryResult(control, sqlQuery);
		}
		
		public static API_SqlServer add_Viewer_StoredProcedures<T>(this API_SqlServer sqlServer , T control)
			where T : Control
		{
			var value = control.add_TextArea();	
			var storedProcedure_Names = value.insert_Left<Panel>(200).add_TreeView().sort();
			var database_Names = storedProcedure_Names.insert_Above<Panel>(100).add_TreeView().sort();
			database_Names.afterSelect<string>(
				(database_Name)=>{
									value.set_Text("");
									var database = new Database(sqlServer, database_Name);
									database.map_StoredProcedures();
									storedProcedure_Names.clear();						
									storedProcedure_Names.add_Nodes(database.StoredProcedures);
									storedProcedure_Names.selectFirst(); 
								 });
			
			storedProcedure_Names.afterSelect<StoredProcedure>(
				(storedProcedure) => value.set_Text(storedProcedure.Value) );
			
			database_Names.add_Nodes(sqlServer.database_Names());
			
			database_Names.selectFirst();
			return sqlServer;
		}
		
		public static API_SqlServer add_Viewer_Tables<T>(this API_SqlServer sqlServer , T control)
			where T : Control
		{		
			var value = control.add_TableList();	
			var tables_Names = value.insert_Left<Panel>(200).add_TreeView().sort();		 	
			var database_Names = tables_Names.insert_Above<Panel>(100).add_TreeView().sort();


			database_Names.afterSelect<string>(
				(database_Name)=>{
									tables_Names.backColor(Color.Salmon);
									O2Thread.mtaThread(
										()=>{
												value.set_Text("");
												var database = new Database(sqlServer, database_Name);									
												database.map_Tables()
														.map_Table_Columns();
												tables_Names.clear();						
												tables_Names.add_Nodes(database.Tables);
												tables_Names.selectFirst(); 
												tables_Names.backColor(Color.White);
											});	 
								 });
			
			tables_Names.afterSelect<Table>( 
				(table) => value.show(table.Columns) );
			
			database_Names.add_Nodes(sqlServer.database_Names());
			
			database_Names.selectFirst();
			return sqlServer;
		}
		
		public static API_SqlServer add_Viewer_TablesData<T>(this API_SqlServer sqlServer , T control)
			where T : Control
		{		
			var dataGridView = control.add_DataGridView();
			
			dataGridView.DataError+= (sender,e) => {}; //" dataGridView error: {0}".error(e.Context);};
			var tables_Names = dataGridView.insert_Left<Panel>(200).add_TreeView().sort();		 	
			var database_Names = tables_Names.insert_Above<Panel>(100).add_TreeView().sort();

			var rowData = dataGridView.insert_Below<Panel>(100).add_SourceCodeViewer(); 
			var rowDataField = rowData.insert_Left<Panel>(100).add_TreeView();
			var selectedField = "";
			rowDataField.afterSelect<DataGridViewCell>( 
				(cell)=>{
							selectedField = rowDataField.selected().get_Text();
							var fieldContent = cell.Value.str().fixCRLF();
							if (fieldContent.starts("<?xml"))
							{	
								"mapping xml".info(); 
								fieldContent = fieldContent.xmlFormat();
								rowData.set_Text(fieldContent,"a.xml");
							}
							else
								rowData.set_Text(fieldContent);
						});
			
			dataGridView.afterSelect(
				(row)=> {																					
							rowDataField.clear();
							//rowData.set_Text("");
							foreach(DataGridViewCell cell in row.Cells)
							{
								var fieldName = dataGridView.Columns[cell.ColumnIndex].Name;
								var node = rowDataField.add_Node(fieldName,cell);
								if (fieldName == selectedField)
									node.selected();
							}
							if (rowDataField.selected().isNull())
								rowDataField.selectFirst();																						
						});
						
			database_Names.afterSelect<string>(
				(database_Name)=>{
									tables_Names.backColor(Color.Salmon);
									O2Thread.mtaThread(
										()=>{
												var database = new Database(sqlServer, database_Name);									
												database.map_Tables()
														//.map_Table_Columns()
														.map_Table_Data();
												tables_Names.clear();						
												tables_Names.add_Nodes(database.Tables);
												tables_Names.selectFirst(); 
												tables_Names.backColor(Color.White);
											});
								 }); 
			
			tables_Names.afterSelect<Table>( 
				(table)=>{
							rowDataField.clear();
							rowData.set_Text("");	
							dataGridView.remove_Columns();							
							dataGridView.invokeOnThread(()=>dataGridView.DataSource= table.TableData);							
						 });
			
			database_Names.add_Nodes(sqlServer.database_Names());
			
			database_Names.selectFirst();  
			return sqlServer;
		}
	}		
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Data.SqlClient;
using System.Windows;
using System.Data.Common;
using System.Security.Cryptography;
using System.IO;

namespace DB_Lab1.ViewModel
{
    public class MyDataBase : IDisposable
    {        
        private OleDbConnection connection;
        private const string Path = @"C:\Users\pokro.MY-BOOBYLDA\Рабочий стол\БД лаба\Колледж(для студентов).mdb";
        private readonly string Connectionstring = 
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + 
            $@"{Path};Persist Security Info=False";

        public MyDataBase()
        {
            try
            {
                connection = new OleDbConnection(Connectionstring);
                connection.OpenAsync();
            }
            catch
            {
                throw;
            }
        }        

        public DataTable ExecuteQuery(string sqlCommand, params DbParameter[] parameters)
        {
            try
            {
                DataTable dataTable = new DataTable();
                OleDbCommand oleDbCommand = new OleDbCommand(sqlCommand, connection);
                oleDbCommand.Parameters.AddRange(parameters);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(oleDbCommand);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new DataTable();
            }
        }               

        public IEnumerable<string> GetTableNames()
        {                        
            DataTable a = connection.GetSchema("Tables", new string[4] { null, null, null, "TABLE" });                        
            return Enumerable.Range(0, a.Rows.Count).Select(i => a.DefaultView[i][2]).Cast<string>();
        }

        public IEnumerable<DataTable> GetTables()
        {
            List<string> tableNames = GetTableNames().ToList();

            if (tableNames.Contains("Users"))
            {
                tableNames.Remove("Users");
            }

            List<DataTable> res = new List<DataTable>();

            foreach (string tableName in tableNames)
            {
                string sql = $"Select * from {tableName};";               
                res.Add(ExecuteQuery(sql));
            }
            return res;
        }

        public void UpdateTable(DataTable dataTable, string tableName)
        {
            try
            {
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);
                dataAdapter.SelectCommand = new OleDbCommand($"Select * from {tableName};", connection);
                dataAdapter.InsertCommand = builder.GetInsertCommand(true);
                dataAdapter.UpdateCommand = builder.GetUpdateCommand(true);
                dataAdapter.DeleteCommand = builder.GetDeleteCommand(true);
                
                dataAdapter.Update(dataTable);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
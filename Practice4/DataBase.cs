using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Practice4
{
    internal class DataBase
    {
        private SQLiteConnection Connection;

        public string TableNameDb { get; set; }

        private readonly string FileNameDb;

        public DataBase(string fileNameDb, string nameTableDb)  
        {
            TableNameDb = nameTableDb;
            FileNameDb = fileNameDb;
            if (!File.Exists($"{fileNameDb}"))
            {
                SQLiteConnection.CreateFile($"{fileNameDb}.sqlite");
            }
            try
            {
                Connection = new SQLiteConnection($"Data Source={FileNameDb}.sqlite;Version=3");
                Connection.Open();
                SQLiteCommand command = Connection.CreateCommand();
                command.Connection = Connection;

                command.CommandText = $"CREATE TABLE IF NOT EXISTS '{TableNameDb}' (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT, email TEXT, password TEXT)";
                command.ExecuteNonQuery();

                MessageBox.Show("Connected");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Disconnected: {ex.Message}");
            }
        }

        public DataTable SelectData(string SQLCommand)
        {
            DataTable dataTable = new DataTable();
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = SQLCommand;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public void AddData(string SQLCommand)
        {            
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = SQLCommand;
            command.ExecuteNonQuery();
        }
    }
}

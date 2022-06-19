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
        private const string DatabaseDir = "Database";
        private readonly string FileDb;
        private readonly string TableNameDb;

        public DataBase(string fileDb, string nameTableDb)
        {            
            TableNameDb = nameTableDb;
            FileDb = $"{DatabaseDir}/{fileDb}";
            if (!File.Exists(FileDb))
            {
                SQLiteConnection.CreateFile($"{FileDb}");
            }
            try
            {
                Connection = new SQLiteConnection($"Data Source={FileDb};Version=3");
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

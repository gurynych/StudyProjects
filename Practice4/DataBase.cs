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
        public string NameTableDb { get; set; }

        public DataBase(string nameTableDb)            
        {
            NameTableDb = nameTableDb;
            if (!File.Exists("Practice4.sqlite"))
            {
                SQLiteConnection.CreateFile("Practice4.sqlite");
            }
            try
            {
                Connection = new SQLiteConnection($"Data Source=Practice4;Version=3");
                Connection.Open();
                SQLiteCommand command = Connection.CreateCommand();
                command.Connection = Connection;

                command.CommandText = $"CREATE TABLE IF NOT EXISTS '{NameTableDb}' (id INTEGER PRIMARY KEY AUTOINCREMENT, login TEXT, email TEXT, password TEXT)";
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

        public bool Contains(string login, string email)
        {
            DataTable dt_user = SelectData($"SELECT * FROM '{NameTableDb}' WHERE login = '{login}' AND email = '{email}'");
            return dt_user.Rows.Count > 0;
        }
    }
}

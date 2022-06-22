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
    public static class DataBase
    {
        static SQLiteConnection Connection;
        static string DatabaseDir = "Database";
        static readonly string FileDb = $"{DatabaseDir}/Main_DB.sqlite";

        static DataBase()
        {
            if (!Directory.Exists(DatabaseDir))
            {
                Directory.CreateDirectory(DatabaseDir);
            }
            if (!File.Exists(FileDb))
            {
                SQLiteConnection.CreateFile(FileDb);
            }
            try
            {
                Connection = new SQLiteConnection($"Data Source={FileDb};Version=3");
                Connection.Open();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Disconnected: {ex.Message}");
            }
        }        

        public static DataTable ExecuteRequest(string SQLCommand)
        {
            DataTable dataTable = new DataTable();
            SQLiteCommand command = Connection.CreateCommand();
            command.CommandText = SQLCommand;
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}

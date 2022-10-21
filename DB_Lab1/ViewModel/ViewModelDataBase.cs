using Microsoft.Office.Interop.Excel;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static System.Net.WebRequestMethods;
using Button = System.Windows.Controls.Button;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;
using File = System.IO.File;

namespace DB_Lab1.ViewModel
{
    public class ViewModelDataBase : ViewModelBase
    {
        private readonly string excelFilePath = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "DataBase.xlsx");
        private DataTable activeTable;
        private bool isStart;

        public ObservableCollection<DataTable> DataTables { get; set; }

        public bool IsStart
        {
            get => isStart;
            set
            {
                isStart = value;
                OnPropertyChanged();
            }
        }

        public DataTable ActiveTable
        {
            get => activeTable;
            set
            {
                activeTable = value;
                OnPropertyChanged();
            }
        }

        public ViewModelDataBase()
        {
            using (MyDataBase db = new MyDataBase())
            {
                DataTables = new ObservableCollection<DataTable>(db.GetTables());
                List<string> names = new List<string>(db.GetTableNames());

                if (names.Contains("Users"))
                {
                    names.Remove("Users");
                }

                int count = DataTables.Count;

                for (int i = 0; i < count; i++)
                {
                    DataTables[i].TableName = names[i];
                }

                ActiveTable = DataTables[0];
            }
        }

        public ICommand ChangeDataTable => new RelayCommand
            ((obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    db.UpdateTable(ActiveTable, ActiveTable.TableName);
                }

            }, (obj) => DataTables.Any() &&
                        ActiveTable != null);

        public ICommand ImportToExcel => new RelayCommand
            ((obj) =>
            {
                IsStart = true;
                Task.Factory.StartNew(() => SaveInExcel(ActiveTable, excelFilePath)).ContinueWith((t) =>
                {
                    IsStart = false;

                    (obj as Button).Dispatcher.Invoke(() => 
                        (obj as Button).GetBindingExpression(Button.CommandProperty)?.UpdateTarget());                   

                });


            }, (obj) => DataTables.Any() &&
                        ActiveTable != null &&
                        !IsStart);

        private void SaveInExcel(DataTable DataTable, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception();
            }
           
            Excel.Application excel = new Excel.Application();
            excel.Visible = false;
            Excel.Workbook workbook = null;
            Excel.Sheets sheets = null;
            Excel.Worksheet newSheet = null;
            bool isExists = File.Exists(path);

            try
            {
                if (!isExists)
                {
                    workbook = excel.Workbooks.Add();                    
                }
                else
                {
                    workbook = excel.Workbooks.Open(path, 0, false, 5, "", "",
                                                    true, XlPlatform.xlWindows, "",
                                                    true, false, 0, true, false, false);                    
                }

                sheets = workbook.Sheets;

                int index = 1;

                foreach (Excel.Worksheet sheet in sheets)
                {
                    if (sheet.Name == DataTable.TableName)
                    {
                        excel.DisplayAlerts = false;
                        excel.Application.ActiveWorkbook.Sheets[index].Delete();
                        excel.DisplayAlerts = true;
                    }

                    index++;
                }

                newSheet = sheets.Add(sheets[1], Type.Missing, Type.Missing, Type.Missing);
                newSheet.Name = DataTable.TableName;

                int columnsCount = DataTable.Columns.Count;
                string[] header = new string[columnsCount];
                for (int i = 0; i < columnsCount; i++)
                    {
                        header[i] = DataTable.Columns[i].ColumnName;
                    }

                Excel.Range headerRange = newSheet.Range[newSheet.Cells[1, 1], newSheet.Cells[1, columnsCount]];
                headerRange.Value = header;
                headerRange.Font.Bold = true;

                int rowsCount = DataTable.Rows.Count;
                string[,] cells = new string[rowsCount, columnsCount];

                for (int i = 0; i < rowsCount; i++)
                    {
                        for (int j = 0; j < columnsCount; j++)
                        {
                            cells[i, j] = DataTable.Rows[i][j].ToString();
                        }
                    }
                newSheet.Range[newSheet.Cells[2, 1], newSheet.Cells[rowsCount + 1, columnsCount]].Value = cells;                
            }
            catch
            {
                //MessageBox.Show("Ошибка импорта", "Ошибка");
            }
            finally
            {
                if (isExists)
                {
                    workbook.Save();
                }
                else 
                {
                    workbook.SaveAs(path);
                }

                workbook.Close();
                excel.Quit();
                GC.Collect();
            }
        }        
    }
}

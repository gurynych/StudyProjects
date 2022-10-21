using System;
using System.Data.OleDb;
using System.Data;
using System.Windows.Input;
using System.Data.Common;
using DB_Lab1.Validation;
using System.Windows;
using DB_Lab1.MVVM;
using DB_Lab1.Model;

namespace DB_Lab1.ViewModel
{
    public class ViewModelDataBasePrevious : DataValidation
    {
        private Messenger msgr;
        private User activeUser;
        private DataTable dataTable;
        private string studentLastName = string.Empty;
        private string studentGroup = string.Empty;
        private string studentYearOfBirth = string.Empty;
        private string studentNum = string.Empty;
        private string studentNewName = string.Empty;
        private DateTime calendar = new DateTime(1997, 1, 1);
        private DateTime firstDate = new DateTime(1997, 1, 1);
        private DateTime secondDate = new DateTime(1997, 1, 1);

        [StringIsLetterProperty]
        public string StudentLastName
        {
            get
            {
                return studentLastName;
            }

            set
            {
                studentLastName = value;
                OnPropertyChanged();
            }
        }
        
        [StringIsLetterOrDigitProperty]
        public string StudentGroup
        {
            get
            {
                return studentGroup;
            }

            set
            {
                studentGroup = value;
                OnPropertyChanged();
            }
        }

        [StringIsDigitProperty]
        public string StudentYearOfBirth
        {
            get
            {
                return studentYearOfBirth;
            }

            set
            {
                studentYearOfBirth = value;
                OnPropertyChanged();
            }
        }

        [StringIsDigitProperty]
        public string StudentNum
        {
            get
            {
                return studentNum;
            }

            set
            {
                studentNum = value;
                OnPropertyChanged();
            }
        }

        [StringIsLetterProperty]
        public string StudentNewName
        {
            get
            {
                return studentNewName;
            }

            set
            {
                studentNewName = value;
                OnPropertyChanged();
            }
        }

        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }

            private set
            {
                dataTable = value;
                OnPropertyChanged();
            }
        }

        public DateTime Calendar
        {
            get
            {
                return calendar;
            }

            set
            {
                calendar = value;
                OnPropertyChanged();
            }
        }

        public DateTime FirstDate
        {
            get
            {
                return firstDate;
            }

            set
            {
                firstDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime SecondDate
        {
            get
            {
                return secondDate;
            }

            set
            {
                secondDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectStudentsByLastname => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "select Familia, №gr, Budget " +
                                        "from students " +
                                        "Where Familia = @StudentLastName;";
                    DbParameter param = new OleDbParameter("@StudentLastname", StudentLastName);

                    DataTable = db.ExecuteQuery(sqlCommand, param);
                }

            }, 
            (obj) => DataTable != null && 
                    StudentLastName != string.Empty &&                    
                    !errors.Contains(nameof(StudentLastName)));

        public ICommand SelectStudentsByGroup => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "select Familia + ' ' + Left(Imya, 1) + '.' + Left(Otchestvo, 1) + '.' as FIO, №gr " +
                                        "from students " +
                                        "where №gr = @StudentGroup;";
                    DbParameter param = new OleDbParameter("@StudentGroup", StudentGroup);

                    DataTable = db.ExecuteQuery(sqlCommand, param);
                }

            },
            (obj) => DataTable != null &&
                     StudentGroup != string.Empty &&                     
                     !errors.Contains(nameof(StudentGroup)));

        public ICommand SelectStudentsByDateOfBirth => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "select Familia, №gr " +
                                        "from students " +
                                        "where Year(Datarogd) = @StudentDateOfBirth;";
                    DbParameter param = new OleDbParameter("@StudentDateOfBirth", StudentYearOfBirth);

                    DataTable = db.ExecuteQuery(sqlCommand, param);
                }

            }, 
            (obj) => DataTable != null && 
                     StudentYearOfBirth != string.Empty &&
                     !errors.Contains(nameof(StudentYearOfBirth)));

        public ICommand UpdateStudentsByStudentNum => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "update students set Imya = @StudentNewName " +
                                        "where №st = @StudentNum";

                    DbParameter paramNum = new OleDbParameter("@StudentNum", StudentNum);
                    DbParameter paramNewName = new OleDbParameter("@StudentNewName", StudentNewName);

                    _ = db.ExecuteQuery(sqlCommand, paramNewName, paramNum);
                    DataTable = db.ExecuteQuery("Select * from students");
                }

            },
            (obj) => DataTable != null &&
                    StudentNum != string.Empty &&
                    StudentNewName != string.Empty &&
                    !errors.Contains(nameof(StudentNum)) &&
                    !errors.Contains(nameof(StudentNewName)));

        public ICommand DeleteStudentsByStudentsNum => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "Delete from students " +
                                        "where №st = @StudentNum";

                    DbParameter paramNum = new OleDbParameter("@StudentNum", StudentNum);

                    _ = db.ExecuteQuery(sqlCommand, paramNum);
                    DataTable = db.ExecuteQuery("Select * from students");
                }

            },
            (obj) => DataTable != null && 
                     StudentNum != string.Empty &&
                     !errors.Contains(nameof(StudentNum)));

        public ICommand CalculateAges => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    DataTable = db.ExecuteQuery("select Familia, Imya, Otchestvo, Pol, Format(Date() - Datarogd, 'yy') as Age " +
                                                "from students " +
                                                "where Pol = 'М';");
                }

            },
            (obj) => DataTable != null);
        
        public ICommand SelectNonresidentStudents => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    DataTable = db.ExecuteQuery("select Familia, №gr, gorod " +
                                                "from students " +
                                                "where gorod is not null;");
                }

            },
            (obj) => DataTable != null);

        public ICommand SelectStudientsByCalendar => new RelayCommand(
            (obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {
                    string sqlCommand = "select *" +
                                        "from students " +
                                        "where Datarogd = @Calendar;";
                    DbParameter param = new OleDbParameter("@Calendar", Calendar);

                    DataTable = db.ExecuteQuery(sqlCommand, param);
                }

            }, 
            (obj) => DataTable != null && 
                     Calendar != default);

        public ICommand SelectStudientsByDateRange => new RelayCommand((obj) =>
        {
            using (MyDataBase db = new MyDataBase())
            {
                string sqlCommand = "select *" +
                                    "from students " +
                                    "where Datarogd >= @FirstDate and Datarogd <= @SecondDate;";
                DbParameter firstParam = new OleDbParameter("@FirstDate", FirstDate);
                DbParameter secondParam = new OleDbParameter("@SecondDate", SecondDate);

                DataTable = db.ExecuteQuery(sqlCommand, firstParam, secondParam);
            }

        }, (obj) => DataTable != null && 
                    FirstDate != default && 
                    SecondDate != default && 
                    FirstDate <= SecondDate);

        public ICommand ResetTable => new RelayCommand((obj) =>
        {
            using (MyDataBase db = new MyDataBase())
            {
                DataTable = db.ExecuteQuery("Select * from students;");
            }

        }, (obj) => DataTable != null);

        public ICommand ChangeDataTable => new RelayCommand
            ((obj) =>
            {
                using (MyDataBase db = new MyDataBase())
                {                
                    db.UpdateTable(DataTable, "Student");
                }

            }, (obj) => DataTable != null);

        public ViewModelDataBasePrevious(Messenger msgr)
        {
            this.msgr = msgr;

            msgr.Receive(GetType(),
                (message) =>
                {
                    activeUser = message as User;
                });

            DataTable = new MyDataBase()?.ExecuteQuery("Select * from students;");
        }

        private DataTable ResetTableIfTableEmpty(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                return dataTable;
            }

            MessageBox.Show("Записи не найдены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new MyDataBase().ExecuteQuery("Select * from students;");
        }
    }
}
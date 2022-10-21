using DB_Lab1.Model;
using DB_Lab1.MVVM;
using DB_Lab1.View;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DB_Lab1.ViewModel
{
    public class ViewModelSignIn : ViewModelBase
    {
        private Messenger msgr;
        private string login;
        private string password;
        private Description pbDescription;

        public string Login
        {
            get => login;

            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public Description PbDescription
        {
            get => pbDescription;
            set
            {
                pbDescription = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SignIn => new RelayCommand
            ((obj) =>
            {                
                User.ActiveUser = new User(Login, Password, DateTime.Now);

                if (IsUserContainsInDb(User.ActiveUser, out string role))
                {
                    if (role == "Admin")
                    {
                        msgr.Send<MainViewModel>(new UCDataBaseForAdmin());                        
                    }
                    else
                    {
                        msgr.Send<MainViewModel>(new UCDataBaseForCommonUser());                        
                    }
                }
                else
                {
                    PbDescription = new Description("Неверный логин или пароль", Description.RedDescription);
                }

            }, (obj) => !PbDescription.IsError &&
                        !string.IsNullOrEmpty(Login) &&
                        !string.IsNullOrEmpty(Password));

        public RelayCommand GoToSignUp => new RelayCommand
            ((obj) =>
            {
                msgr.Send<MainViewModel>(new UCSignUp());

            }, (obj) => true);

        public bool IsUserContainsInDb(User user, out string role)
        {
            using (MyDataBase db = new MyDataBase())
            {
                string sql = "select Role " +
                             "from users " +
                             "Where Login=@Login and PasswordHash=@PasswordHash;";

                DbParameter paramLogin = new OleDbParameter("@Login", user.Login);
                DbParameter paramPassw = new OleDbParameter("@PasswordHash", user.PasswordHash);                
                DataTable table = db.ExecuteQuery(sql, paramLogin, paramPassw);

                bool b = table.Rows.Count != 0;

                role = null;

                if (b)
                {
                    role = table.Rows[0].ItemArray[0] as string;
                }          

                return b;
                                
            }
        }

        public ViewModelSignIn(Messenger msgr)
        {
            this.msgr = msgr;
            CreateUsersTableIfNotExist();
        }

        public void CreateUsersTableIfNotExist()
        {
            using (MyDataBase db = new MyDataBase())
            {
                if (!db.GetTableNames().Contains("Users"))
                {
                    string sql = "CREATE TABLE Users" +
                                 "(" +
                                 "Login varchar(255)," +
                                 "PasswordHash varchar(255)," +
                                 "Role varchar(255)" +
                                 ");";

                    _ = db.ExecuteQuery(sql);
                }
            }
        }
    }
}

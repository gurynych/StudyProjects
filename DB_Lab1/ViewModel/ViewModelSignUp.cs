using DB_Lab1.Model;
using DB_Lab1.MVVM;
using DB_Lab1.View;
using System;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Lab1.ViewModel
{
    public class ViewModelSignUp : ViewModelBase
    {
        private const int CorrectPasswLength = 8;
        private Messenger msgr;
        private string login;
        private string password;
        private string passwordRepeat;
        private Description tbDescription;
        private Description pbDescription;
        private Description pbRepeatDescription;

        public string Login
        {
            get => login;

            set
            {
                login = value;
                TbDescription = new Description();

                Task.Factory.StartNew
                    (() =>
                    {
                        if (IsLoginContainsInDb(Login))
                        {
                            TbDescription = new Description("Такой логин уже зарегистрирован", Description.RedDescription, true);
                        }
                    });

                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                PbDescription = (Password != string.Empty) ? CheckPasswordByStrongConditions(password) : default;
                PasswordRepeat = PasswordRepeat;
                OnPropertyChanged();
            }
        }

        public string PasswordRepeat
        {
            get => passwordRepeat;
            set
            {
                passwordRepeat = value;
                PbRepeatDescription = new Description();

                if (PasswordRepeat != Password)
                {
                    PbRepeatDescription = new Description("Пароли не совпадают", Description.RedDescription, true);
                }

                OnPropertyChanged();
            }
        }

        public Description TbDescription
        {
            get => tbDescription;
            set
            {
                tbDescription = value;
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

        public Description PbRepeatDescription
        {
            get => pbRepeatDescription;
            set
            {
                pbRepeatDescription = value;
                OnPropertyChanged();
            }
        }

        public ViewModelSignUp(Messenger msgr)
        {
            this.msgr = msgr;
            CreateUsersTableIfNotExist();
        }

        public RelayCommand SignUp => new RelayCommand
            ((obj) =>
            {               
                User.ActiveUser = new User(Login, Password, DateTime.Now);

                if (IsLoginContainsInDb(User.ActiveUser.Login))
                {
                    return;
                }

                InsertUserInDb(User.ActiveUser);
                
                msgr.Send<MainViewModel>(new UCDataBaseForCommonUser());                

            }, (obj) => !TbDescription.IsError &&
                        !PbDescription.IsError &&
                        !PbRepeatDescription.IsError &&
                        !string.IsNullOrEmpty(Login) &&
                        !string.IsNullOrEmpty(Password) &&
                        !string.IsNullOrEmpty(PasswordRepeat));

        public RelayCommand GoToSingIn => new RelayCommand
            ((obj) =>
            {
                msgr.Send<MainViewModel>(new UCSignIn());

            }, (obj) => true);

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

        public bool IsLoginContainsInDb(string login)
        {
            using (MyDataBase db = new MyDataBase())
            {
                string sql = "select * " +
                             "from users " +
                             "Where Login=@Login;";

                DbParameter paramLogin = new OleDbParameter("@Login", login);

                return db.ExecuteQuery(sql, paramLogin).Rows.Count != 0;
            }
        }

        public void InsertUserInDb(User user)
        {
            using (MyDataBase db = new MyDataBase())
            {
                string sql = "insert into Users (Login, PasswordHash, Role) " +
                             "values(@Login, @PasswordHash, @Role);";

                DbParameter paramLogin = new OleDbParameter("@Login", user.Login);
                DbParameter paramPassw = new OleDbParameter("@PasswordHash", user.PasswordHash);
                DbParameter paramRole = new OleDbParameter("@Role", "Student");

                _ = db.ExecuteQuery(sql, paramLogin, paramPassw, paramRole);
            }
        }

        public Description CheckPasswordByStrongConditions(string password)
        {
            bool isBigLetterHere = false;
            bool isDigitHere = false;
            bool isSpecificSymbolHere = false;
            bool isSmallLetterHere = false;
            bool isMoreThan7 = password.Length >= 8;

            string specificSymbols = "!$#%";                        

            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    isSmallLetterHere = true;
                }

                if (char.IsUpper(c))
                {
                    isBigLetterHere = true;
                }

                if (char.IsDigit(c))
                {
                    isDigitHere = true;
                }

                if (specificSymbols.Contains(c))
                {
                    isSpecificSymbolHere = true;
                }
            }            

            if (isDigitHere && isBigLetterHere && isSmallLetterHere && isSpecificSymbolHere && isMoreThan7)
            {
                return new Description("Надежный пароль", Description.GreenDescription);
            }

            if (isDigitHere && isBigLetterHere && isSmallLetterHere && isMoreThan7)
            {
                return new Description("Хороший пароль", Description.YellowDescription);
            }

            if (isSmallLetterHere && isBigLetterHere && isMoreThan7)
            {
                return new Description("Нормальный пароль", Description.OrangeDescription);
            }

            if (isMoreThan7)
            {
                return new Description("Слабый пароль", Description.CoralDescription);
            }

            return new Description($"Минимальное количество символов в пароле: {CorrectPasswLength}", Description.RedDescription, true);
        }
    }
}

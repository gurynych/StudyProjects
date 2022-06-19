using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice4.UCs.Authorization
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : UserControl
    {
        int CorrectPasswLength = 8;

        public RegistrationPage()
        {
            InitializeComponent();            
        }      

        private void RegistrateNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0)
            {
                ShowNotify("Введите имя пользователя");
            }
            else if (!IsValidEmail(email.Text))
            {                
                ShowNotify("Введите корректный email");
            }
            else if (!IsValidPassword(password.Password))
            {
                Error.Visibility = Visibility.Visible;
                ShowNotify("Введите корректный пароль!");
            }
            else
            {
                Error.Visibility = Visibility.Hidden;
                DataBase dataBase = new DataBase("Accounts.sqlite", "Users");
                DataTable dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE username='{username.Text}'");
                if (dataTable.Rows.Count > 0)
                {
                    ShowNotify("Имя пользователя занято");                    
                }
                else
                {
                    dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE email='{email.Text}'");
                    if (dataTable.Rows.Count > 0)
                    {
                        ShowNotify("Адрес электронной почты уже зарегистрирован");
                    }
                    else 
                    {
                        dataBase.AddData($"INSERT INTO Users (username, email, password) VALUES ('{username.Text}', '{email.Text}', '{password.Password}')");
                        ShowNotify("Успешная регистрация!");
                    }
                }                
            }
        }

        private bool IsValidPassword(string password)
        {
            string errorText;
            errorText = "• Не менее 8 симолов\n" +
                        "• Как минимум одна заглавная и одна строчная буква\n" +
                        "• Как минимум одна цифра\n" +
                        "• Без пробелов\n" +
                        "• Допустимые символы:~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;";
            TextError.Text = errorText;
            if (password.Length < CorrectPasswLength)
            {
                return false;
            }

            errorText = "• Как минимум одна заглавная и одна строчная буква\n" +
                        "• Как минимум одна цифра\n" +
                        "• Допустимые символы:~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;";
            bool isCapitalLetter = false;
            bool isSmallLetter = false;
            bool isDigit = false;
            bool isNotSpace = true;
            bool isSpecialSymbol = false;
            bool isNotOtherSymbol = true;
            foreach (char c in password)
            {
                if (char.IsUpper(c) && !isCapitalLetter) isCapitalLetter = true;
                else if (char.IsLower(c) && !isSmallLetter) isSmallLetter = true;
                else if (char.IsDigit(c) && !isDigit) isDigit = true;
                else if (char.IsWhiteSpace(c) && !isNotSpace) isNotSpace = false;
                else if ("~!?@#$%^&*_-+()[]{}></\\|\"'.,:;".Contains(c) && !isSpecialSymbol) isSpecialSymbol = true;
                else isNotOtherSymbol = false;
            }
            TextError.Text = errorText;
            return isCapitalLetter && isSmallLetter && isDigit && isNotSpace && isSpecialSymbol && isNotOtherSymbol;
        }

        private void ShowNotify(string textNotify)
        {
            SnackbarNotify.MessageQueue?.Enqueue(textNotify, null, null, null, false, true, TimeSpan.FromSeconds(1.5));
            SnackbarNotify.IsActive = true;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                           + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                           + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private void Button_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbols = "№!#&()*:;<=>@[]^{|}~";
            e.Handled = symbols.Contains(e.Text);
        }        
    }
}

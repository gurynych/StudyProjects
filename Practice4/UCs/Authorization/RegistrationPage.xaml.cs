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
                MessageBox.Show("Ввдетие имя пользователя");
                return;
            }
            if (!IsValidEmail(email.Text))
            {
                MessageBox.Show("Ввдетие email");
                return;
            }
            if (password.Password.Length < CorrectPasswLength)
            {
                MessageBox.Show("Введите пароль\nНе менее 8 символов!");
                return;
            }

            DataBase dataBase = new DataBase("newDb.sqlite", "Users");
            DataTable dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE username='{username.Text}'");
            if (dataTable.Rows.Count > 0)
            {                            
                MessageBox.Show("Имя пользователя занято");
                return;
            }

            dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE email='{email.Text}'");
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Адрес электронной почты уже зарегистрирован");
                return;
            }

            dataBase.AddData($"INSERT INTO Users (username, email, password) VALUES ('{username.Text}', '{email.Text}', '{password.Password}')");            
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
            string symbols = "\"№!#$&()*./:;<=>@[]^{|}~ ";
            e.Handled = symbols.Contains(e.Text);
        }       
    }
}

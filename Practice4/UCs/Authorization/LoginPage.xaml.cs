using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {        
        public LoginPage()
        {
            InitializeComponent();                      
        }

        private void DataVerification_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text.Length == 0)
            {
                ShowNotify("Ввeдите имя пользователя");
            }
            else if (password.Password.Length == 0)
            {
                ShowNotify("Ввeдите пароль");             
            }
            else
            {
                DataBase dataBase = new DataBase("Accounts.sqlite", "Users");
                DataTable dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE username = '{username.Text}' AND password = '{password.Password}'");

                if (dataTable.Rows.Count > 0)
                {
                    ShowNotify("Успешный вход");
                }
                else
                {
                    ShowNotify("Неверный логин или пароль");
                }
            }
        }

        private void ShowNotify(string textNotify)
        {
            SnackbarNotify.MessageQueue?.Enqueue(textNotify, null, null, null, false, true, TimeSpan.FromSeconds(1.5));
            SnackbarNotify.IsActive = true;
        }

        private void Button_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbols = "\"№!#$&()*./:;<=>@[]^{|}~ ";
            e.Handled = symbols.Contains(e.Text);
        }
    }
}

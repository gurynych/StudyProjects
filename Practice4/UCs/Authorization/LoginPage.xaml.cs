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
                SnackbarThree.MessageQueue?.Enqueue("Ввeдетие имя пользователя", null, null, null, false, true, TimeSpan.FromSeconds(1.5));
                SnackbarThree.IsActive = true;                
                return;
            }
            if (password.Password.Length == 0)
            {
                SnackbarThree.MessageQueue?.Enqueue("Ввeдетие пароль", null, null, null, false, true, TimeSpan.FromSeconds(1.5));
                SnackbarThree.IsActive = true;
                MessageBox.Show("Введите пароль");
                return;
            }            

            DataBase dataBase = new DataBase("newDb.sqlite", "Users");
            DataTable dataTable = dataBase.SelectData($"SELECT * FROM Users WHERE username = '{username.Text}' AND password = '{password.Password}'");
            if (dataTable.Rows.Count > 0)
            {
                SnackbarThree.MessageQueue?.Enqueue("Успешный вход", null, null, null, false, true, TimeSpan.FromSeconds(1.5));
                SnackbarThree.IsActive = true;
            }
            else 
            {
                SnackbarThree.MessageQueue?.Enqueue("Неверный логин или пароль", null, null, null, false, true, TimeSpan.FromSeconds(1.5));
                SnackbarThree.IsActive = true;
            }

            if (password.Password.Length < 8)
            {
                password.BorderBrush = Brushes.Red;
            }

        }

        private void Button_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbols = "\"№!#$&()*./:;<=>@[]^{|}~ ";
            e.Handled = symbols.Contains(e.Text);
        }
    }
}

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
            if (login.Text.Length > 0) // проверяем введён ли логин     
            {
                if (password.Password.Length > 0) // проверяем введён ли пароль         
                {                                // ищем в базе данных пользователя с такими данными         
                    DataBase dataBase = new DataBase("Users");
                    DataTable dt_user = dataBase.SelectData($"SELECT * FROM Users WHERE login = '{login.Text}' AND password = '{password.Password}'");
                    if (dt_user.Rows.Count > 0) // если такая запись существует
                    {
                        MessageBox.Show("Пользователь авторизовался"); // говорим, что авторизовался       
                    }
                    else MessageBox.Show("Пользователь не найден"); // выводим ошибку  
                }
                else MessageBox.Show("Введите пароль"); // выводим ошибку    
            }
            else MessageBox.Show("Введите логин"); // выводим ошибку
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            MainWindow.Instance.Container.Content = null;
            MainWindow.Instance.Container.Content = registrationPage;
            
            //if (login.Text.Length == 0 && password.Password.Length == 0)
            //{
            //    MessageBox.Show("Пустой ввод");
            //    return;
            //}
            //DataBase dataBase = new DataBase("Users");
            //if (dataBase.Contains(login.Text, login.Text))
            //{
            //    MessageBox.Show("Уже содержится");
            //    return;
            //}            
            //dataBase.AddData($"INSERT INTO Users (login, email, password) VALUES ('{login.Text}', '{login.Text}', '{password.Password}')");
        }

        private void ButtonPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbol = e.Text;
            string symbols2 = "!#$&()*-./:;<=>@[]^_{|}~";
            if (!Regex.Match(symbol, @"[а-яА-Я]|[a-zA-Z]|[0-9]").Success && symbols2.IndexOf(symbol) < 0)
            {
                e.Handled = true;
            }
        }

        private void ButtonLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbol = e.Text;
            string symbols = @"/-()|_";
            if (!Regex.Match(symbol, @"[а-яА-Я]|[a-zA-Z]|[0-9]").Success && symbols.IndexOf(symbols) < 0)
            {
                e.Handled = true;
            }
        }
    }
}

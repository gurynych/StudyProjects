using MaterialDesignThemes.Wpf;
using Practice4.UCs.MainMenu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        private readonly ApplicationContext Db;
        
        public LoginPage()
        {
            InitializeComponent();
            Db = new ApplicationContext();
            //Db.DbUsers.Load();
        }

        private void DataVerification_Click(object sender, RoutedEventArgs e)
        {
            password.Password = (EyeIcon.Kind.ToString() == "EyeOutline") ? password.Password : HiddenTextBox.Text;
            if (username.Text.Length == 0)
            {
                TextBlockNotify.Text = "Ввeдите имя пользователя";
                BorderNotify.Visibility = Visibility.Visible;
                return;
            }
            if (password.Password.Length == 0)
            {
                TextBlockNotify.Text = "Ввeдите пароль";
                BorderNotify.Visibility = Visibility.Visible;
                return;
            }
            
            DbUser user = Db.DbUsers.Local.FirstOrDefault(u => u.Username == username.Text && u.Password == password.Password);
            if (user == null)
            {
                TextBlockNotify.Text = "Неверный логин или пароль";
                BorderNotify.Visibility = Visibility.Visible;
                return;
            }

            MainWindow.Instance.ActiveUser = user;
            MainWindow.Instance.SetPage(new UserPage());
        }        

        private void Username_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string symbols = "\"№!#$&()*./:;<=>@[]^{|}~ ";
            e.Handled = symbols.Contains(e.Text);
        }

        private void CloseNotify_Click(object sender, RoutedEventArgs e)
        {
            BorderNotify.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == string.Empty)
            {
                return;
            }
            if (EyeIcon.Kind.ToString() == "EyeOffOutline")
            {
                Binding bindingForeground = new Binding();
                bindingForeground.ElementName = password.Name;
                bindingForeground.Path = new PropertyPath("BorderBrush");
                EyeIcon.SetBinding(MaterialDesignThemes.Wpf.PackIcon.ForegroundProperty, bindingForeground);

                EyeIcon.Kind = PackIconKind.EyeOutline;
                if (HiddenTextBox.Text != string.Empty)
                {
                    password.Password = HiddenTextBox.Text;
                    password.Visibility = Visibility.Visible;
                    HiddenTextBox.Visibility = Visibility.Collapsed;
                }
                if (HiddenTextBox.Text == string.Empty)
                {
                    password.Password = string.Empty;
                    HiddenTextBox.Visibility = Visibility.Collapsed;
                    password.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Binding bindingForeground = new Binding();
                bindingForeground.ElementName = HiddenTextBox.Name;
                bindingForeground.Path = new PropertyPath("BorderBrush");
                EyeIcon.SetBinding(MaterialDesignThemes.Wpf.PackIcon.ForegroundProperty, bindingForeground);

                EyeIcon.Kind = PackIconKind.EyeOffOutline;
                if (password.Password != string.Empty)
                {
                    HiddenTextBox.Text = password.Password;
                    password.Visibility = Visibility.Collapsed;
                    HiddenTextBox.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

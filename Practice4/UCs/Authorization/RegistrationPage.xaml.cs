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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : UserControl
    {        
        private int CorrectPasswLength = 8;

        public RegistrationPage()
        {            
            InitializeComponent();           
        }

        private void RegistrateNewAccount_Click(object sender, RoutedEventArgs e)
        {
            //password.Password = (EyeIcon.Kind.ToString() == "EyeOutline") ? password.Password : HiddenTextBox.Text;
            //if (username.Text.Length == 0)
            //{
            //    OpenNotify("Введите имя пользователя");
            //    return;
            //}
            //if (!IsValidEmail(email.Text))
            //{
            //    OpenNotify("Введите корректный email");                
            //    return;
            //}
            //if (!IsValidPassword(password.Password))
            //{
            //    IconNotify.Visibility = Visibility.Visible;
            //    OpenNotify("Введите корректный пароль!");                
            //    return;
            //}

            //IconNotify.Visibility = Visibility.Collapsed;           
            
            //if (MainWindow.Instance.db.DbUsers.Any(u => u.Username == username.Text))
            //{
            //    OpenNotify("Имя пользователя занято");
            //    BorderNotify.Visibility = Visibility.Visible;
            //    return;
            //}

            //if (MainWindow.Instance.db.DbUsers.Any(u => u.Email == email.Text))
            //{
            //    OpenNotify("Email уже зарегистрирован");
            //    BorderNotify.Visibility = Visibility.Visible;
            //    return;
            //}

            DbUser user = new DbUser()
            {
                Username = username.Text,
                Email = email.Text,
                Password = password.Password,
            };

            MainWindow.Instance.db.DbUsers.Add(user);
            MainWindow.Instance.db.SaveChanges();

            MainWindow.Instance.ActiveUser = user;
            MainWindow.Instance.SetPage(new UserPage());
        }    

        private bool IsValidPassword(string password)
        {
            TextError.Text = "• Не менее 8 симолов и не более 24 символов\n" +
                             "• Как минимум одна заглавная и одна строчная буква\n" +
                             "• Как минимум одна цифра\n" +
                             "• Без пробелов\n" +
                             "• Дополнительные допустимые символы:~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;";            
            if (password.Length < CorrectPasswLength)
            {
                return false;
            }            
            bool isCapitalLetter = false;
            bool isSmallLetter = false;
            bool isDigit = false;
            bool isNotSpace = true;
            foreach (char c in password)
            {
                if (char.IsUpper(c) && !isCapitalLetter) isCapitalLetter = true;
                else if (char.IsLower(c) && !isSmallLetter) isSmallLetter = true;
                else if (char.IsDigit(c) && !isDigit) isDigit = true;
                else if (char.IsWhiteSpace(c) && !isNotSpace) isNotSpace = false;
            }            
            return isCapitalLetter && isSmallLetter && isDigit && isNotSpace;
        }        

        private bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                           + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                           + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private void OpenNotify(string error)
        {
            TextBlockNotify.Text = error;
            BorderNotify.Visibility = Visibility.Visible;
        }

        private void CloseNotify_Click(object sender, RoutedEventArgs e)
        {
            BorderNotify.Visibility = Visibility.Collapsed;
            IconNotify.Visibility = Visibility.Collapsed;
        }

        private void Username_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "№!#&()*:;<=>@[]^{|}~".Contains(e.Text);
        }

        private void Password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !("~!?@#$%^&*_-+()[]{}></\\|\"'.,:;".Contains(e.Text) || Regex.IsMatch(e.Text, @"[a-z0-9]", RegexOptions.IgnoreCase));
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

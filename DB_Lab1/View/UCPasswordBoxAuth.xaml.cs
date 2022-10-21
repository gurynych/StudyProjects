using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace DB_Lab1.View
{
    /// <summary>
    /// Логика взаимодействия для UCPasswordBoxAuth.xaml
    /// </summary>
    public partial class UCPasswordBoxAuth : UserControl
    {       
        private static readonly DependencyProperty PasswordToBindProperty =
            DependencyProperty.Register("PasswordToBind", typeof(string), typeof(UCPasswordBoxAuth),
                new FrameworkPropertyMetadata
                (
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                ));

        public string PasswordToBind
        {
            get => GetValue(PasswordToBindProperty) as string;
            set => SetValue(PasswordToBindProperty, value);
        }

        public UCPasswordBoxAuth()
        {
            InitializeComponent();
        }

        private void MainPbChanged(object sender, RoutedEventArgs args)
        {
            PasswordToBind = (sender as PasswordBox).Password;
        }
    }
}

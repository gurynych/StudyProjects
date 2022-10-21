using DB_Lab1.Model;
using System.Windows;
using System.Windows.Controls;

namespace DB_Lab1.View
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UCPasswordBox : UserControl
    {        
        private static readonly DependencyProperty PasswordToBindProperty =
            DependencyProperty.Register("PasswordToBind", typeof(string), typeof(UCPasswordBox),
                new FrameworkPropertyMetadata
                (
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                ));
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(Description), typeof(UCPasswordBox),
                new FrameworkPropertyMetadata
                (
                    new Description(),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(PropertyChanged)
                ));

        public string PasswordToBind
        {
            get => GetValue(PasswordToBindProperty) as string;
            set => SetValue(PasswordToBindProperty, value);
        }

        public Description Description
        {
            get => (Description)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public UCPasswordBox()
        {
            InitializeComponent();
        }

        private void PbChanged(object sender, RoutedEventArgs args)
        {
            PasswordToBind = (sender as PasswordBox).Password;
        }

        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as UCPasswordBox).UpdateDescription((Description)e.NewValue);
        }

        public void UpdateDescription(Description d)
        {
            infoText.Text = d.Text;
            infoText.Foreground = d.ColorDescription;
            border.BorderBrush = d.ColorDescription;
        }
    }
}

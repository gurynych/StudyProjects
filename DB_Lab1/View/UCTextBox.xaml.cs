using DB_Lab1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для UCTextBoxReg.xaml
    /// </summary>
    public partial class UCTextBox : UserControl
    {
        public static readonly DependencyProperty TextToBindProperty =
            DependencyProperty.Register("TextToBind", typeof(string), typeof(UCTextBox),
                new FrameworkPropertyMetadata
                (
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                ));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(Description), typeof(UCTextBox),
                new FrameworkPropertyMetadata
                (
                    new Description(),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(PropertyChanged)
                ));

        public string TextToBind
        {
            get => GetValue(TextToBindProperty) as string;
            set => SetValue(TextToBindProperty, value);
        }

        public Description Description
        {
            get => (Description)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public UCTextBox()
        {
            InitializeComponent();
        }

        private void TbChanged(object sender, TextChangedEventArgs e)
        {
            TextToBind = (sender as TextBox).Text;
        }

        private static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as UCTextBox).UpdateDescription((Description)e.NewValue);
        }

        public void UpdateDescription(Description d)
        {                     
            infoText.Text = d.Text;            
            infoText.Foreground = d.ColorDescription;
            border.BorderBrush = d.ColorDescription;
        }
    }
}

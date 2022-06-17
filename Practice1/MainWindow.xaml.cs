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

namespace Practice1
{    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InText.Focus();
            CalcButton.IsEnabled = false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,.-+".IndexOf(e.Text) < 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            Table.Items.Clear();
            Table.Columns.Clear();
            InText.Text.Trim();
            OutputText.Inlines.Clear();
            OutputText.Foreground = Brushes.Black;

            Equation equation = new Equation();
            equation.ConvertText(InText.Text);
            OutputText.Inlines.AddRange(equation.GetEquation());

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Начало отрезка";
            c1.Binding = new Binding("Begin");
            c1.Width = 190;
            Table.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Конец отрезка";
            c2.Width = 190;
            c2.Binding = new Binding("End");
            Table.Columns.Add(c2);

            foreach (Segment segment in equation.FindSegments())
            {
                Table.Items.Add(segment);
            }            
        }

        private void InText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InText.Text != string.Empty)
            {
                CalcButton.IsEnabled = true;
            }
            else
            {
                CalcButton.IsEnabled = false;
            }
            Equation equation = new Equation();
            Table.Items.Clear();
            Table.Columns.Clear();
            InText.Text.Trim();
            if (!equation.IsCorrect(InText.Text))
            {
                CalcButton.IsEnabled = false;
                Table.Items.Clear();
                OutputText.Foreground = Brushes.Red;
                OutputText.Text = "Ошибка ввода данных!";
            }
            else
            {
                CalcButton.IsEnabled = true;
                OutputText.Inlines.Clear();
                OutputText.Foreground = Brushes.Black;
            }
        }
    }        
}

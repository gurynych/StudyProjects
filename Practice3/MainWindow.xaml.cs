using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practice3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private List<StringBuilder> binaryMap;
        private List<TextBox> textBoxes;
        SolidColorBrush buttonBrush;
        int rows = 16, cols = 16;

        public MainWindow()
        {
            InitializeComponent();
            
            binaryMap = new List<StringBuilder>();            
            textBoxes = new List<TextBox>();
            buttonBrush = Brushes.Black;
            SolidColorBrush borderBrush = Brushes.Black;
            double thicknessBorder = 0.5;
            double columnWidth, rowHeight = columnWidth = 25;

            for (int i = 0; i <= rows; i++)
            {
                for (int j = 0; j <= cols; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        if (i == 0 && j == 0)
                        {
                            RowDefinition row = new RowDefinition();
                            row.Height = new GridLength(rowHeight);
                            ColumnDefinition column = new ColumnDefinition();
                            column.Width = new GridLength(columnWidth);
                            Table.RowDefinitions.Add(row);
                            Table.ColumnDefinitions.Add(column);
                        }
                        else if (i == 0)
                        {
                            ColumnDefinition column = new ColumnDefinition();
                            column.Width = new GridLength(columnWidth);
                            Table.ColumnDefinitions.Add(column);
                            TextBlock header = new TextBlock();
                            header.Text = j.ToString();
                            header.FontSize = 14;
                            header.FontWeight = FontWeights.Bold;
                            header.TextAlignment = TextAlignment.Center;

                            Border border = new Border();
                            border.BorderThickness = new Thickness(thicknessBorder);
                            border.BorderBrush = borderBrush;
                            border.Child = header;

                            Table.Children.Add(border);
                            Grid.SetRow(border, i);
                            Grid.SetColumn(border, j);
                        }
                        else
                        {
                            RowDefinition row = new RowDefinition();
                            row.Height = new GridLength(rowHeight);
                            Table.RowDefinitions.Add(row);
                            TextBlock header = new TextBlock();
                            header.Text = i.ToString();
                            header.FontSize = 14;
                            header.FontWeight = FontWeights.Bold;
                            header.TextAlignment = TextAlignment.Center;

                            Border border = new Border();
                            border.BorderThickness = new Thickness(thicknessBorder);
                            border.BorderBrush = borderBrush;
                            border.Child = header;

                            Table.Children.Add(border);
                            Grid.SetColumn(border, j);
                            Grid.SetRow(border, i);
                        }
                    }
                    else
                    {
                        Button btn = new Button();
                        btn.Width = columnWidth;
                        btn.Background = Brushes.White;
                        btn.BorderBrush = borderBrush;
                        btn.BorderThickness = new Thickness(thicknessBorder);
                        btn.Click += Button_Click;
                        binaryMap.Add(new StringBuilder(new string('0', cols)));

                        Table.Children.Add(btn);
                        Grid.SetRow(btn, i);
                        Grid.SetColumn(btn, j);
                    }
                }                
            }            
                                                
            ColumnDefinition columnLast = new ColumnDefinition();            
            Table.ColumnDefinitions.Add(columnLast);
            RowDefinition rowLast = new RowDefinition();
            rowLast.Height = new GridLength(rowHeight);
            Table.RowDefinitions.Add(rowLast);

            Button newTable = new Button();
            newTable.Content = "Таблица кодов";
            newTable.FontSize = 14;
            newTable.Width = 100;
            newTable.Height = 25;
            newTable.BorderBrush = borderBrush;
            newTable.BorderThickness = new Thickness(thicknessBorder);            
            newTable.Margin = new Thickness(5, 0, 0, 0);
            newTable.Click += NewTable_Click;
            Table.Children.Add(newTable);
            Grid.SetRow(newTable, 0);
            Grid.SetColumn(newTable, cols + 1);

            Button checkBtn = new Button();
            checkBtn.Content = "Проверить!";
            checkBtn.FontSize = 14;
            checkBtn.Width = 100;
            checkBtn.Height = 25;
            checkBtn.BorderThickness = new Thickness(thicknessBorder);
            checkBtn.BorderBrush = borderBrush;
            checkBtn.Margin = new Thickness(5, 0, 0, 0);
            checkBtn.Click += CheckTextBox_Click;
            Table.Children.Add(checkBtn);
            Grid.SetRow(checkBtn, rows + 1);
            Grid.SetColumn(checkBtn, cols + 1);

            for (int i = 1; i <= rows; i++)
            {
                TextBox textBox = new TextBox();
                textBox.FontSize = 14;
                textBox.Width = 100;
                textBox.Height = 25;
                textBox.Margin = new Thickness(5, 0, 0, 0);
                textBox.MaxLength = 8;
                textBox.Text = "0";
                textBox.TextAlignment = TextAlignment.Center;
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
                Table.Children.Add(textBox);
                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, cols + 1);
                textBoxes.Add(textBox);
            }
        }

        private void NewTable_Click(object sender, RoutedEventArgs e)
        {
            Information information = new Information();
            information.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int iBtn = Grid.GetRow(btn) - 1;
            int jBtn = Grid.GetColumn(btn) - 1;
            
            if (btn.Background == Brushes.White)
            {
                btn.Background = buttonBrush;
                binaryMap[iBtn][jBtn] = '1';
            }
            else 
            {
                btn.Background = Brushes.White;
                binaryMap[iBtn][jBtn] = '0';
            }
        }

        private void CheckTextBox_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < cols; i++)
            {
                int test = Convert.ToInt32(new string('0', 16), 2);
                int binaryString = Convert.ToInt32(binaryMap[i].ToString(), 2);
                string hexString = Convert.ToString(binaryString, 16).ToUpper();
                
                if (textBoxes[i].Text.ToUpper() != hexString)
                {
                    MessageBox.Show($"Неверный ответ в {i + 1} ячейке!", "Проверка ответов", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            MessageBox.Show($"Верный ответ!", "Проверка ответов", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            TextBlock colorTextBlock = menu.Icon as TextBlock;
            buttonBrush = (SolidColorBrush)colorTextBlock.Background;
            foreach (object item in Table.Children)
            {
                if (item is Button)
                {
                    Button btn = item as Button;
                    if (btn.Content == null && btn.Background != Brushes.White)
                    {
                        btn.Background = buttonBrush;
                    }
                }
            } 
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789ABCDEFabcdef".IndexOf(e.Text) < 0;
        }
    }
}

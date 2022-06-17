using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Practice3
{
    /// <summary>
    /// Логика взаимодействия для Information.xaml
    /// </summary>
    public partial class Information : Window
    {
        public Information()
        {
            InitializeComponent();                       
            
            for (int j = 0; j < 4; j++)
            {
                TextBlock header = new TextBlock();
                header.Text = "16";
                header.FontWeight = FontWeights.Bold;
                header.FontSize = 14;
                header.TextAlignment = TextAlignment.Center;

                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.Black;
                border.Child = header;

                if (j == 0 || j == 2)
                {
                    header.Text = "16";
                }
                else
                {
                    header.Text = "2";
                }

                Table.Children.Add(border);
                Grid.SetColumn(border, j);
                Grid.SetRow(border, 0);
            }

            bool flag = true;
            for (int i = 0; i < 8; i++)
            {
                TextBlock hexTextBlock = new TextBlock();
                hexTextBlock.FontSize = 14;
                hexTextBlock.TextAlignment = TextAlignment.Center;
                hexTextBlock.FontWeight = FontWeights.Bold;
                Border hexBorder = new Border();
                hexBorder.BorderThickness = new Thickness(0.5);
                hexBorder.BorderBrush = Brushes.Black;
                hexBorder.Child = hexTextBlock;

                TextBlock binaryTextBlock = new TextBlock();
                binaryTextBlock.FontSize = 14;
                binaryTextBlock.TextAlignment = TextAlignment.Center;                
                Border binaryBorder = new Border();
                binaryBorder.BorderThickness = new Thickness(0.5);
                binaryBorder.BorderBrush = Brushes.Black;
                binaryBorder.Child = binaryTextBlock;

                if (i < 8 && flag)
                {
                    hexTextBlock.Text = Convert.ToString(i, 16).ToUpper();
                    binaryTextBlock.Text = Convert.ToString(i, 2);
                    int binaryLength = binaryTextBlock.Text.Length;
                    binaryTextBlock.Text = binaryTextBlock.Text.Insert(0, new string('0', 4 - binaryLength));
                    Table.Children.Add(hexBorder);
                    Grid.SetColumn(hexBorder, 0);
                    Grid.SetRow(hexBorder, i + 1);
                    Table.Children.Add(binaryBorder);
                    Grid.SetColumn(binaryBorder, 1);
                    Grid.SetRow(binaryBorder, i + 1);
                    if (i == 7) { i = -1; flag = false; }
                }
                else
                {
                    hexTextBlock.Text = Convert.ToString(i + 8, 16).ToUpper();
                    binaryTextBlock.Text = Convert.ToString(i + 8, 2);
                    Table.Children.Add(hexBorder);
                    Grid.SetColumn(hexBorder, 2);
                    Grid.SetRow(hexBorder, i + 1);
                    Table.Children.Add(binaryBorder);
                    Grid.SetColumn(binaryBorder, 3);
                    Grid.SetRow(binaryBorder, i + 1);
                }
            }
        }            
    }
}

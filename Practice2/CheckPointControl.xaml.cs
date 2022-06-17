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

namespace Practice2
{
    /// <summary>
    /// Логика взаимодействия для CheckPointInfo.xaml
    /// </summary>
    public partial class CheckPointControl : UserControl
    {
        public CheckPointControl()
        {
            InitializeComponent();
        }

        private void CloseControl_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.CloseInfo();
        }

        public CheckPointControl AddInfo(CheckpointInfo checkpoint)
        {
            if (checkpoint == null)
            {
                return null;
            }

            CheckName.Text = checkpoint.Name;
            ImagesContainer.Children.Clear();

            if (checkpoint.Drinks)
            {
                ImagesContainer.Children.Add(GetImage(@"D:\Visual_Studio.project\Practice2\Icons\map-icon-drinks.jpg"));
            }
            if (checkpoint.EnergyBars)
            {
                ImagesContainer.Children.Add(GetImage(@"D:\Visual_Studio.project\Practice2\Icons\map-icon-energy-bars.jpg"));
            }
            if (checkpoint.Toilets)
            {
                ImagesContainer.Children.Add(GetImage(@"D:\Visual_Studio.project\Practice2\Icons\map-icon-toilets.jpg"));
            }
            if (checkpoint.Information)
            {
                ImagesContainer.Children.Add(GetImage(@"D:\Visual_Studio.project\Practice2\Icons\map-icon-information.jpg"));
            }
            if (checkpoint.Medical)
            {
                ImagesContainer.Children.Add(GetImage(@"D:\Visual_Studio.project\Practice2\Icons\map-icon-medical.jpg"));
            }
            return this;
        }

        private Image GetImage(string path)
        {
            Image img = new Image();
            img.Margin = new Thickness(5);
            img.Height = img.Width = 50;
            BitmapImage src = new BitmapImage();
            src.BeginInit();           
            src.UriSource = new Uri(path, UriKind.Absolute);
            src.EndInit();
            img.Source = src;
            img.Stretch = Stretch.Uniform;
            return img;
        }
    }
}

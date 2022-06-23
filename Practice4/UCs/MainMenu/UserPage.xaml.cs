using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Practice4.UCs.MainMenu
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : UserControl
    {        
        public UserPage()
        {
            InitializeComponent();
            if (MainWindow.Instance.MunuColorZone != null)
            {
                MainWindow.Instance.MunuColorZone.Visibility = Visibility.Visible;
            }

            DocxReader();
        }

        public void DocxReader()
        {
            string DocxPath = @"../../Blokcheyn.rtf";
            
            TextRange tr = new TextRange(
            Test.Document.ContentStart, Test.Document.ContentEnd);

            using (FileStream fs = File.Open(DocxPath, FileMode.Open))
            {
                tr.Load(fs, DataFormats.Rtf);
            };
        }
    }
}

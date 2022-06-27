using System;
using System.Collections.Generic;
using System.IO;
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

namespace Practice4.UCs.Theory
{
    /// <summary>
    /// Логика взаимодействия для TheoryPage.xaml
    /// </summary>
    public partial class TheoryPage : UserControl
    {
        public TheoryPage(DbTheory theory)
        {
            InitializeComponent();

            MainWindow.Instance.PageInfo.Text = theory.Topic;
            TextRange tr = new TextRange(TheoryRichText.Document.ContentStart, TheoryRichText.Document.ContentEnd);
            using (FileStream fs = File.Open(theory.FilePath, FileMode.Open))
            {
                tr.Load(fs, DataFormats.Rtf);
            };
        }
    }
}

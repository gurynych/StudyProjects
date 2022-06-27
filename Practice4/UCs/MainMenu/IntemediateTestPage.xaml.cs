using Practice4.UCs.Tests;
using Practice4.UCs.Theory;
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

namespace Practice4.UCs.MainMenu
{
    /// <summary>
    /// Логика взаимодействия для IntemediateTestPage.xaml
    /// </summary>
    public partial class IntermediateTestPage : UserControl
    {
        public IntermediateTestPage(List<DbTest> ts)
        {
            InitializeComponent();
            TheoryTree.ItemsSource = new List<object>()
            {
                new
                {
                    Names = ts,
                    Name = "Тесты"
                }
            };
        }


        private void TreeViewItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "Тесты") return;
            DbTest test = button.Tag as DbTest;
            MainWindow.Instance.SetPage(new TestPage(test));
        }        
    }
}

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
    /// Логика взаимодействия для IntemediateTheoryPage.xaml
    /// </summary>
    public partial class IntermediateTheoryPage : UserControl
    {
        public IntermediateTheoryPage(List<DbTheory> ts)
        {
            InitializeComponent();     
            TheoryTree.ItemsSource = new List<object>()
            {
                new
                {
                    Topics = ts,
                    Topic = "Теории"
                }
            };
        }                        

        private void TreeViewItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "Теории") return;
            DbTheory theory = button.Tag as DbTheory;
            MainWindow.Instance.SetPage(new TheoryPage(theory));
        }
    }
}

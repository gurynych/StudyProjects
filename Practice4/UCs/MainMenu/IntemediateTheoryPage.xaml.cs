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
        List<DbTheory> theories;

        public IntermediateTheoryPage(List<DbTheory> ts)
        {
            InitializeComponent();
            InitializeComponent();
            theories = ts.ToList();
            TheoryTree.ItemsSource = new List<object>()
            {
                new
                {
                    Topics = theories,
                    Topic = "Теории"
                }
            };
        }                 

        public void GoToTheory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DbTheory theory = button.Tag as DbTheory;
            MainWindow.Instance.SetPage(new TheoryPage(theory));
        }
    }
}

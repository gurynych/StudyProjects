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

            TheoryTree.ItemsSource = new List<object>()
            {
                new
                {
                    Topics = ts,
                    Topic = "Теории"
                }
            };
        }                 

        private void TheoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView button = sender as TreeView;
            DbTheory theory = button.SelectedItem as DbTheory;
            MainWindow.Instance.SetPage(new TheoryPage(theory));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : UserControl
    {
        public StatisticPage()
        {
            InitializeComponent();
            DbUser user = MainWindow.Instance.ActiveUser;            
            List<DbStatistic> stats = MainWindow.Instance.db.DbStatistics
                .Include(x => x.DbTest)
                .Include(x => x.DbTest.Questions)
                .Where(x => x.DbUserId == user.Id)
                .ToList();
            cardContainer.ItemsSource = stats;
        }
    }
}

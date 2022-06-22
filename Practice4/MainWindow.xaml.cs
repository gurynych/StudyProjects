using Practice4.UCs.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using MaterialDesignThemes.Wpf.Transitions;
using Practice4.UCs.Start;

namespace Practice4 
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {                      
        public static MainWindow Instance { get; private set; }

        public MainWindow() 
        {
            InitializeComponent();
            Container.Content = new AuthorizationSlides();
            Instance = this;            
            DataBase.ExecuteRequest($"CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT, email TEXT, password TEXT)");
            //using (ApplicationContext DB = new ApplicationContext())
            //{
            //}            
        }        
    }
}

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
using System.Data.Entity;
using Practice4.UCs.MainMenu;
using Practice4.UCs.Theory;

namespace Practice4 
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public DbUser ActiveUser;

        public static MainWindow Instance { get; private set; }

        public ApplicationContext db;

        public MainWindow() 
        {
            InitializeComponent();
            Instance = this;

            SetPage(new AuthorizationSlides());
            db = new ApplicationContext();
            db.Load();
            db.SaveChanges();
        }

        private bool IsFirstSlide = false;
        private RoutedCommand command;

        public void SetPage(UserControl control)
        {
            command = Transitioner.MoveNextCommand;
            TransitionerSlide slide = slide2;
            if (IsFirstSlide)
            {
                slide = slide1;
                command = Transitioner.MovePreviousCommand;
            }
            slide.Content = control;
            command.CanExecuteChanged += Command_CanExecuteChanged;
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            command.Execute(sender, Container);
            command.CanExecuteChanged -= Command_CanExecuteChanged;
            IsFirstSlide = !IsFirstSlide;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
        }

        private void GoUserPage_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new UserPage());
        }

        private void GoToTreeTheory_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new IntermediateTheoryPage(db.DbTheories.ToList()));
            MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, drawer);
        }

        private void GoToTreeTest_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new IntermediateTestPage(db.DbTests.Include(x => x.DbTheories).Include(x => x.Questions).ToList()));
            MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, drawer);
        }

        private void ExitFromApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }    
}

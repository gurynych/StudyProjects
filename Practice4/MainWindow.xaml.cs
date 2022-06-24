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

namespace Practice4 
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public DbUser ActiveUser;

        public static MainWindow Instance { get; private set; }

        public MainWindow() 
        {
            InitializeComponent();            

            //DbQuestion q = new DbQuestion() { Type = "1", QuestionText = "1"};
            //Db.DbQuestions.Add(q);
            //DbAnswer a = new DbAnswer() { Text = "123", IsCorrect = true, DbQuestion = q};
            //DbAnswer a1 = new DbAnswer() { Text = "456", DbQuestion = q};
            //Db.DbAnswers.AddRange(new List<DbAnswer>() { a, a1 });            
            //List<DbQuestion> test = db.DbQuestions.Include(q => q.DbAnswers).ToList();
            
            Instance = this;            
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
            SetPage(new AuthorizationSlides());
            ApplicationContext db = new ApplicationContext();
            db.Load();

            //TheoryTree.ItemsSource = new List<object>()
            //{
            //    new
            //    {
            //        Topics = db.DbTheories.ToList(),
            //        Topic = "Теории"
            //    }
            //};
        }

        private void GoUserPage_Click(object sender, RoutedEventArgs e)
        {
            //Scroll
            SetPage(new UserPage());
        }
    }    
}

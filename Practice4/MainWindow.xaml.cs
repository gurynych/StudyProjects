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

            ApplicationContext Db = new ApplicationContext();

                //Db.DbTheories.Add(new DbTheory() { Topic =})

            //DbQuestion q = new DbQuestion() { Type = "1", QuestionText = "1"};
            //Db.DbQuestions.Add(q);
            //DbAnswer a = new DbAnswer() { Text = "123", IsCorrect = true, DbQuestion = q};
            //DbAnswer a1 = new DbAnswer() { Text = "456", DbQuestion = q};
            //Db.DbAnswers.AddRange(new List<DbAnswer>() { a, a1 });
            //Db.SaveChanges();


            List<DbQuestion> test = Db.DbQuestions.Include(q => q.DbAnswers).ToList();
            

            Container.Content = new AuthorizationSlides();
            Instance = this;            
        }


        public void SetPage(UserControl control)
        {
            Container.Content = control;
        }
    }    
}

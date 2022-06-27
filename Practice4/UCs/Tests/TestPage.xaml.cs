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

namespace Practice4.UCs.Tests
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : UserControl
    {
        private readonly DbTest Test;
        private int currentQuestion;
        private List<IQuestionControl> controls;

        public TestPage(DbTest test)
        {
            InitializeComponent();

            currentQuestion = 0;         
            Test = test;

            controls = new List<IQuestionControl>();
            foreach (DbQuestion question in test.Questions)
            {
                controls.Add(GetControlForQuestionType(question));
            }

            ShowCurrentQuestion();
        }

        public void ShowCurrentQuestion()
        {
            UpdateButtons();
            testContainer.Content = controls[currentQuestion];
        }

        private void GoToPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion--;
            ShowCurrentQuestion();
        }

        private void GoToNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuestion == Test.Questions.Count - 1)
            {
                CalculateResults();
                MainWindow.Instance.SetPage(new ResultPage(Test));
                return;
            }

            currentQuestion++;
            ShowCurrentQuestion();
        }

        private void UpdateButtons()
        {
            GoToPrevious.IsEnabled = true;
            GoToNext.IsEnabled = true;
            GoToNext.ToolTip = "Следующий вопрос";
            var icon = GoToNext.Content as MaterialDesignThemes.Wpf.PackIcon;
            icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRight;

            if (currentQuestion == 0)
            {
                GoToPrevious.IsEnabled = false;
            }

            if (currentQuestion == Test.Questions.Count - 1)
            {
                icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Send;
                GoToNext.ToolTip = "Просмотр результата";
            }
        }        

        private static IQuestionControl GetControlForQuestionType(DbQuestion question)
        {
            string type = question.Type;
            if (type == "s")
            {
                return new SimpleChoice(question);
            }

            if (type == "i")
            {
                return new InputVar(question);
            }

            if (type == "m")
            {
                return new MultipleChoice(question);
            }

            return null;
        }

        private void CalculateResults()
        {
            DbStatistic statistic = MainWindow.Instance.ActiveUser.DbStatistics.FirstOrDefault(x => x.DbTestId == Test.Id);
            if (statistic == null)
            {
                statistic = new DbStatistic();
                statistic.DbTest = Test;
                statistic.DbUser = MainWindow.Instance.ActiveUser;

                MainWindow.Instance.db.DbStatistics.Add(statistic);
            }

            int score = 0;
            int i = 0;
            foreach (DbQuestion q in Test.Questions)
            {                
                List<DbAnswer> correctAnswers = q.DbAnswers.Where(a => a.IsCorrect).ToList();
                List<DbAnswer> userAnswer = controls[i].GetUserAnswers();
                if (correctAnswers.Count == userAnswer.Count && q.Type != "i")
                {
                    if (correctAnswers.All(userAnswer.Contains))
                    {
                        score++;
                    }
                }
                else if (q.Type == "i" && correctAnswers[0].Text.ToLower() == userAnswer[0].Text)
                {
                    score++;
                }
                q.DbAnswers.ForEach(x => x.IsUserSelected = false);
                i++;
            }

            statistic.Score = score;
            MainWindow.Instance.db.SaveChanges();
        }
    }   
}

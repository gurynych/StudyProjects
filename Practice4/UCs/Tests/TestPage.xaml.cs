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
        private IQuestionControl control;
        private MaterialDesignThemes.Wpf.PackIcon nextIcon;

        public TestPage(DbTest test)
        {
            InitializeComponent();

            currentQuestion = 0;
            nextIcon = GoToNext.Content as MaterialDesignThemes.Wpf.PackIcon;
            Test = test;

            UpdateButtons();
        }

        public void ShowCurrentQuestion()
        {
            DbQuestion question = Test.Questions.ElementAt(currentQuestion);
            control = GetControlForQuestionType(question);
            testContainer.Content = control;
        }

        private void UpdateButtons()
        {
            GoToPrevious.IsEnabled = true;
            GoToNext.IsEnabled = true;

            if (GoToNext.Content != nextIcon)
            {
                GoToNext.Content = nextIcon;
            }

            if (currentQuestion == 0)
            {
                GoToPrevious.IsEnabled = false;
            }

            if (currentQuestion == Test.Questions.Count - 1)
            {
                GoToNext.Content = "Результаты";
            }
        }

        private void GoToPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion--;
            ShowCurrentQuestion();
            UpdateButtons();
        }

        private void GoToNext_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion++;
            ShowCurrentQuestion();
            UpdateButtons();
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
    }   
}

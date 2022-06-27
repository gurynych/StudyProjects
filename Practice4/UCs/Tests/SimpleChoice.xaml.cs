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
    /// Логика взаимодействия для SimpleChoice.xaml
    /// </summary>
    public partial class SimpleChoice : UserControl, IQuestionControl
    {
        private readonly DbQuestion Question;
        public SimpleChoice(DbQuestion question)
        {   
            InitializeComponent();
            Question = question;
            RadioButtonConteiner.ItemsSource = question.DbAnswers;
            QuestionText.Text = question.QuestionText;
        }

        public List<DbAnswer> GetUserAnswers()
        {
            return new List<DbAnswer>() { Question.DbAnswers.FirstOrDefault(x => x.IsUserSelected) };
        }
    }
}

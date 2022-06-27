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
    /// Логика взаимодействия для InputVar.xaml
    /// </summary>
    public partial class InputVar : UserControl, IQuestionControl
    {
        public InputVar(DbQuestion question)
        {
            InitializeComponent();
            QuestionText.Text = question.QuestionText;
            UserEntryText.Focus();
        }

        public List<DbAnswer> GetUserAnswers()
        {            
            return new List<DbAnswer>() { new DbAnswer(UserEntryText.Text.Replace(" ","").ToLower())};
        }
    }
}

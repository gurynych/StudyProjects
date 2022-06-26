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
    public partial class InputVar : UserControl
    {
        public InputVar(List<DbAnswer> answers)
        {
            InitializeComponent();            
            int aId = answers[0].DbQuestionId;
            QuestionText.Text = MainWindow.Instance.db.DbQuestions.FirstOrDefault(q => q.Id == aId).QuestionText;
        }
    }
}

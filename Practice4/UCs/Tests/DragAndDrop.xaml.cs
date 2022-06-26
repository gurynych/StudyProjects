using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : UserControl
    {
        public DragAndDrop(List<DbAnswer> answers)
        {
            InitializeComponent();
            CardContainer.ItemsSource = answers;
            int aId = answers[0].DbQuestionId;
            QuestionText.Text = MainWindow.Instance.db.DbQuestions.FirstOrDefault(q => q.Id == aId).QuestionText;
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(sender as Card, sender as Card, DragDropEffects.Move);
            }
        }

        private void Card_DragOver(object sender, DragEventArgs e)
        {
            Point dragPosition = e.GetPosition(canvas);
            Canvas.SetLeft(sender as Card, dragPosition.X);
            Canvas.SetTop(sender as Card, dragPosition.Y);
        }
    }
}

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
using System.Windows.Shapes;

namespace Practice2
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        public Table(List<CheckpointInfo> checkpoints)
        {            
            InitializeComponent();           
            foreach (CheckpointInfo checkpoint in checkpoints)
            {
                Data.Items.Add(checkpoint);
            }
        }
    }
}

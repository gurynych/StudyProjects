using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace DbLab2_Individual.Views
{
    /// <summary>
    /// Логика взаимодействия для RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {
        public RequestsWindow()
        {
            InitializeComponent();
        }
        private void ChangingTableColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime?))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy HH:mm";
            }

            object? obj = (sender as DataGrid).ItemsSource;

            PropertyInfo? prop = obj?.GetType().GetGenericArguments().First().GetProperty(e.PropertyName);
            DescriptionAttribute? description = prop?.GetCustomAttribute<DescriptionAttribute>();
            ReadOnlyAttribute? readOnly = prop?.GetCustomAttribute<ReadOnlyAttribute>();

            if (description is null)
            {
                e.Cancel = true;
            }
            else
            {
                e.Column.Header = description.Description;
            }

            if (readOnly is not null && readOnly.IsReadOnly)
            {
                e.Column.IsReadOnly = true;
            }
        }
    }
}

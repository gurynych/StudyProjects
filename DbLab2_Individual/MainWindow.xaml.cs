using System;
using System.CodeDom;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DbLab2_Individual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangingTableColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime?))
            {
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy HH:mm";
            }

            object? obj = (sender as DataGrid)?.ItemsSource;

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

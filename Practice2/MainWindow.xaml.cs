using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Practice2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private List<CheckpointInfo> checkpoints;

        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            checkpoints = new List<CheckpointInfo>();
            Instance = this;
        }

        private void Check1_Click(object sender, RoutedEventArgs e)
        {            
            Button btn = sender as Button;       
            Container.Content = new CheckPointControl()
                .AddInfo(checkpoints
                .FirstOrDefault(x => x.Name.Contains(btn.Content.ToString())));
        }

        public void CloseInfo()
        {
            Container.Content = null;
        }

        private List<string> GetData(string filePath)
        {
            if (File.Exists(filePath))
            {                
                return File.ReadAllLines(filePath).ToList();
            }
            return new List<string>();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            checkpoints.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            //openFileDialog.InitialDirectory = @"C:\Users\pokro.MY-BOOBYLDA\Рабочий стол\УП 01_022\№10_Работа с текстовыми файлами и компонентом Image";
            openFileDialog.DefaultExt = "txt";

            if (openFileDialog.ShowDialog() == true)
            {
                Container.Content = null;
                string filePath = openFileDialog.FileName;
                List<string> file = GetData(filePath);

                if (file.Count == 0)
                {
                    MessageBox.Show("Неверный путь к файлу или пустое содержимое файла",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                foreach (string line in file)
                {
                    CheckpointInfo ch = new CheckpointInfo();
                    if (ch.Parse(line))
                    {
                        checkpoints.Add(ch);
                    }
                }
                if (checkpoints.Count == 0)
                {
                    MessageBox.Show("Содержимое файла не соответствует заданному формату",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void ShowTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = new Table(checkpoints);
            table.Show();
        }
    }
}

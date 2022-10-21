using DB_Lab1.Model;
using System;
using System.IO;
using System.Windows;

namespace DB_Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (User.ActiveUser != null)
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "history.txt");

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "Логин\tВремя входа\tВремя выхода");
                }

                File.AppendAllText(path, $"\n{User.ActiveUser.Login}\t{User.ActiveUser.EntryTime}\t{DateTime.Now}");
            }
        }
    }
}

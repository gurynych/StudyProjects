﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
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
using MaterialDesignThemes.Wpf;
using Practice4.UCs.Tests;
using Practice4.UCs.Theory;

namespace Practice4.UCs.MainMenu
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : UserControl
    {        
        public UserPage()
        {
            InitializeComponent();
            if (MainWindow.Instance.MunuColorZone != null)
            {
                MainWindow.Instance.MunuColorZone.Visibility = Visibility.Visible;
            }

            MainWindow.Instance.PageInfo.Text = MainWindow.Instance.ActiveUser.Username;
            CardConteiner.ItemsSource = MainWindow.Instance.db.DbTheories
                .Include(x => x.DbTest)
                .Include(x => x.DbTest.Questions)
                .Include(x => x.DbTest.Questions.Select(s => s.DbAnswers))
                .ToList();
        }
        
        public void GoToTheory_Click(object sender, RoutedEventArgs e)
        { 
            Button button = sender as Button;
            DbTheory theory = button.Tag as DbTheory;
            MainWindow.Instance.SetPage(new TheoryPage(theory));
        }

        public void GoToTest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.db.DbTests.Include(t => t.DbTheories).Include(t => t.Questions);
            Button button = sender as Button;
            DbTest test = button.Tag as DbTest;                 
            MainWindow.Instance.SetPage(new TestPage(test));
        }
    }
}

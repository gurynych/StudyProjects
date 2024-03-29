﻿using System;
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
    /// Логика взаимодействия для ResultPage.xaml
    /// </summary>
    public partial class ResultPage : UserControl
    {
        public ResultPage(DbTest test)
        {
            InitializeComponent();
            ResultText.Text = MainWindow.Instance.ActiveUser.DbStatistics[0].Score.ToString() + "/" + test.Questions.Count;
        }
    }
}

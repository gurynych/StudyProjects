using DbLab2_Individual.Models.FirstDatabase;
using DbLab2_Individual.MVVM;
using DbLab2_Individual.Views;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Components;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DbLab2_Individual.ViewModels
{
    public class RequestsViewModel : BaseViewModel
    {
        public DateTime Calendar { get; set; } = DateTime.Now;
     
        public List<Order> Orders { get; set; }

        public ObservableCollection<Order> OrdersToChange { get; set; }

        private Messenger msgr;

        public RequestsViewModel(Messenger msgr)
        {
            this.msgr = msgr;

            msgr.Receive(GetType(),
                (message) =>
                {
                    if (message is not IEnumerable<Order> orders)
                    {
                        throw new ArgumentException();
                    }

                    if (!orders.Any())
                    {
                        throw new ArgumentException();
                    }

                    Orders = new List<Order>(orders);
                    OrdersToChange = new ObservableCollection<Order>(orders);
                });
        }

        public ICommand FindOrdersByCalendar => new RelayCommand
            ((obj) =>
            {
                OrdersToChange.Clear();
                Orders.Where
                (
                    o => o.OrderDate.Value.Date.ToString("d") == Calendar.Date.ToString("d")
                )
                .ToList()
                .ForEach(o => OrdersToChange.Add(o));

            }, (obj) => true);

        public ICommand FindMostExpensiveOrders => new RelayCommand
            ((obj) =>
            {
                OrdersToChange.Clear();
                long? maxCost = Orders.Max(o => o.Cost);
                Orders.Where(o => o.Cost == maxCost).ToList().ForEach(o => OrdersToChange.Add(o));

            }, (obj) => true);

        public ICommand ResetDataGrid => new RelayCommand
            ((obj) =>
            {
                OrdersToChange.Clear();
                Orders.ForEach(o => OrdersToChange.Add(o));

            }, (obj) => true);

        public ICommand ShowChartWindow => new RelayCommand
            ((obj) =>
            {
                new ChartWindow().Show();
                msgr.Send<ChartViewModel>(Orders);

            }, (obj) => true);
    }
}

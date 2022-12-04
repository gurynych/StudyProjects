using DbLab2_Individual.Models.FirstDatabase;
using DbLab2_Individual.MVVM;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }

        public List<string> Labels { get; set; }

        public Func<double, string> Formatter { get; set; }

        public ChartViewModel(Messenger msgr)
        {
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

                    SeriesCollection = new();
                    Formatter = value => value.ToString("0");
                    Dictionary<string, int> dict = orders.GroupBy(o => o.OrderDate.Value.ToString("d"))
                        .ToDictionary(x => x.Key, x => x.Count());

                    Labels = dict.Select(x => x.Key).ToList();

                    SeriesCollection.Add
                    (
                        new ColumnSeries()
                        {
                            Title = string.Empty,
                            Values = new ChartValues<int>(dict.Select(x => x.Value).ToList())
                        }
                    );
                });
        }
    }
}

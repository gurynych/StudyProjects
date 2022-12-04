using DbLab2_Individual.Models;
using DbLab2_Individual.Models.Controls;
using DbLab2_Individual.Models.FirstDatabase;
using DbLab2_Individual.MVVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ControlTemplate = DbLab2_Individual.Models.Controls.ControlTemplate;

namespace DbLab2_Individual.ViewModels
{
    public class CreateRelationViewModel
    {
        private Messenger msgr;
        private List<DatabaseItem>? allItems;
        private DbContext context;

        public ObservableCollection<DatabaseItem> ConnectWith { get; set; }

        public ObservableCollection<ControlTemplate> Controls { get; set; }

        public CreateRelationViewModel(Messenger msgr, DbContext context)
        {
            this.msgr = msgr;
            this.context = context;

            Controls = new ObservableCollection<ControlTemplate>();

            msgr.Receive(GetType(),
                (message) =>
                {
                    if (message is not IRelationships obj)
                    {
                        throw new ArgumentException();
                    }                    

                    this.context = context;

                    List<SelectionWrapper>? allItems;

                    IEnumerable<PropertyInfo> dbSets = context.GetType().GetProperties()
                        .Where(p => p.PropertyType.IsGenericType);

                    foreach (PropertyInfo dbSet in dbSets)
                    {
                        Type dbSetType = dbSet.PropertyType.GetGenericArguments().First();

                        foreach (PropertyInfo property in obj.RelationshipsProperties)
                        {
                            if (property.PropertyType.IsGenericType &&
                                dbSetType.Equals(property.PropertyType.GetGenericArguments().First()))
                            {
                                allItems =
                                    (dbSet.GetValue(context) as IEnumerable<DatabaseItem>)
                                    ?.Select(x => new SelectionWrapper(x)).ToList();

                                IEnumerable<SelectionWrapper>? selectedItems = 
                                    (property.GetValue(obj) as IEnumerable<DatabaseItem>)
                                    ?.Select(x => new SelectionWrapper(x));

                                if (selectedItems is not null)
                                {
                                    allItems?.Where(x => selectedItems.ToList().Contains(x)).ToList().ForEach(x => x.IsSelected = true);
                                }                                

                                Controls.Add(new CheckBoxControl(allItems));
                            }
                            else if (dbSetType.Equals(property.PropertyType))
                            {
                                
                                SelectionWrapper selectedItem = new(property.GetValue(obj) as DatabaseItem);
                                allItems =
                                    (dbSet.GetValue(context) as IEnumerable<DatabaseItem>)
                                    ?.Select(x => new SelectionWrapper(x)).ToList();

                                SelectionWrapper? temp = allItems?.FirstOrDefault(x => x == selectedItem);
                                
                                if (temp is not null)
                                {
                                    temp.IsSelected = true;
                                }

                                Controls.Add(new RadioBtnControl(allItems));
                            }
                        }
                    }

                    if (Controls.Any(x => x is null))
                    {
                        MessageBox.Show("Отсутствуют данные в других таблицах, сначала добавьте их", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                });
        }

        public ICommand SendSelectedItems => new RelayCommand
            ((obj) =>
            {
                IEnumerable<DatabaseItem?> selectedItems =
                    Controls.SelectMany(c => c.Data.Where(d => d.IsSelected).Select(d => d.Item));                

                Dictionary<Type, List<DatabaseItem>> dataDict = new();

                foreach (DatabaseItem item in selectedItems)
                {
                    if (!dataDict.ContainsKey(item.GetType()))
                    {
                        dataDict.Add(item.GetType(), new List<DatabaseItem>());
                    }

                    dataDict[item.GetType()].Add(item);
                }
                
                msgr.Send<MainViewModel>(dataDict);
                (obj as Window)?.Close();
                 
            }, (obj) => true);
    }
}

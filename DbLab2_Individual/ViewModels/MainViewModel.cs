using System;
using DbLab2_Individual.Models.FirstDatabase;
using DbLab2_Individual.MVVM;
using DbLab2_Individual.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;
using DbLab2_Individual.Models;
using System.ComponentModel;

namespace DbLab2_Individual.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Messenger msgr;
        private readonly DbContext context;

        public int KolichestvoKnopok => 4;

        public ObservableCollection<Table> Tables { get; set; }

        public DatabaseItem ActiveItem { get; set; }

        public Table ActiveTable { get; set; }

        public MainViewModel(Messenger msgr, DbContext context)
        {
            this.msgr = msgr;
            this.context = context;

            Tables = new();
            IEnumerable<PropertyInfo> dbSets = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType);

            foreach (PropertyInfo prop in dbSets)
            {
                object? value = prop.GetValue(context);

                Type? valueType = value?.GetType().GetGenericArguments().First();

                object? objTable = typeof(Table<>)
                    ?.MakeGenericType(valueType)
                    ?.GetConstructor(new[] { prop.PropertyType })
                    ?.Invoke(new[] { value });

                if (objTable is Table table)
                {
                    Tables.Add(table);
                }
            }

        }

        public ICommand SaveTable => new RelayCommand
            ((obj) =>
            {
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Ошибка обновления БД!\n{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (DbException ex)
                {
                    MessageBox.Show($"Внутрення ошибка БД!\n{ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    throw new Exception();
                }

            }, (obj) => true);

        public ICommand AddRelation => new RelayCommand
            ((obj) =>
            {
                new CreateRelationWindow().Show();
                msgr.Send<CreateRelationViewModel>(ActiveItem);

                msgr.Receive(GetType(),
                    (message) =>
                    {
                        if (message is Dictionary<Type, List<DatabaseItem?>> dataDict)
                        {
                            foreach (PropertyInfo? property in ActiveItem.RelationshipsProperties)
                            {
                                if (property.PropertyType.IsGenericType &&
                                    dataDict.ContainsKey(property.PropertyType.GetGenericArguments().First()))
                                {
                                    Type propGenericType = property.PropertyType.GetGenericArguments().First();

                                    List<DatabaseItem> items = dataDict[propGenericType];

                                    Type type = items.First().GetType();

                                    object? castItems = typeof(Enumerable).GetMethod("Cast")
                                        ?.MakeGenericMethod(propGenericType)
                                        ?.Invoke(items, new[] { items });

                                    object? list = typeof(List<>)
                                        .MakeGenericType(type)
                                        .GetConstructor(new[] { castItems?.GetType() })
                                        ?.Invoke(new[] { castItems });

                                    object? collection = typeof(Collection<>)
                                        .MakeGenericType(type)
                                        .GetConstructor(new[] { list.GetType() })
                                        ?.Invoke(new[] { list });

                                    property.SetValue(ActiveItem, collection);
                                }
                                else if (dataDict.ContainsKey(property.PropertyType))
                                {
                                    property.SetValue(ActiveItem, dataDict[property.PropertyType].First());
                                }
                            }
                        }
                    });

                SaveTable.Execute(null);

            }, (obj) => ActiveItem is not null);


        //Часть моего учебного задания, поэтому контекст данных берется напрямую
        public ICommand OpenRequestWindow => new RelayCommand
            ((obj) =>
            {
                new RequestsWindow().Show();
                msgr.Send<RequestsViewModel>((context as Database_ex3_var1Context).Orders);

            }, (obj) => context is Database_ex3_var1Context && Tables.Any());

        public ICommand SaveAsJson => new RelayCommand
            ((obj) =>
            {
                object? value = context.GetType().GetProperties()
                    .Where(p => p.PropertyType.IsGenericType)
                    .First(p => p.PropertyType.GetGenericArguments().First() == ActiveTable
                                                                                    .GetType()
                                                                                    .GetProperty("Data")
                                                                                    .GetValue(ActiveTable)
                                                                                    .GetType()
                                                                                    .GetGenericArguments()
                                                                                    .First())
                    .GetValue(context);

                string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{ActiveTable.Name}.txt";
                string json = JsonConvert.SerializeObject(value, Formatting.Indented);
                File.WriteAllText(path, json);

            }, (obj) => true);
    }
}
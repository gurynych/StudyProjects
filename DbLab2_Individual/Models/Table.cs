using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.Models
{
    public abstract class Table
    {
        public abstract string Name { get; set; }
    }

    public class Table<T> : Table
        where T : class
    {
        public override string Name { get; set; }

        public IEnumerable<T> Data { get; set; }

        public Table(DbSet<T> data)
        {
            LoadDependencies(data);
            Data = data.Local.ToObservableCollection();
            DescriptionAttribute? classDescAttr = typeof(T).GetCustomAttribute<DescriptionAttribute>();
            Name = classDescAttr?.Description ?? typeof(T).GetType().Name;
        }

        public void LoadDependencies(DbSet<T> data)
        {
            data.Load();

            if (typeof(T).BaseType.Equals(typeof(DatabaseItem)))
            {
                if (data.Local.Any())
                {
                    IEnumerable<string> navigations = (data.Local.First() as DatabaseItem)
                        ?.RelationshipsProperties
                        .Where(p => p.PropertyType.IsGenericType)
                        .Select(p => p.Name);

                    foreach (var navigation in navigations)
                    {
                        data.Include(navigation).Load();
                    }
                }
            }
        }     
    }
}

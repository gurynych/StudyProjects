using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.Models
{
    public abstract class DatabaseItem : IRelationships, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public abstract IEnumerable<PropertyInfo?>? RelationshipsProperties { get; }        

        public abstract override string ToString();
    }
}

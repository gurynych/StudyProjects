using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DbLab2_Individual.Models.FirstDatabase
{
    [Description("Пол")]
    public class Gender : DatabaseItem
    {
        private IEnumerable<PropertyInfo> relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Gender).GetProperty(nameof(Employees))
        };

        public long Id { get; set; }

        [Description("Название")]
        public string? GenderName { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
        
        [NotMapped]
        public override IEnumerable<PropertyInfo> RelationshipsProperties => relationshipsPropertyInfos;

        public override string ToString() => GenderName;
    }
}

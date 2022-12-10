using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DbLab2_Individual.Models.FirstDatabase
{
    [Description("Должности")]
    public partial class Post : DatabaseItem
    {
        private readonly IEnumerable<PropertyInfo?>? relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Post).GetProperty(nameof(Employees))
        };

        public long Id { get; set; }

        [Description("Наименование должности")]
        public string? PostName { get; set; }

        [Description("Оклад")]
        public double? Salary { get; set; }

        [Description("Обязанности")]
        public string? Responsibilities { get; set; }

        [Description("Требования")]
        public string? Requirements { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override IEnumerable<PropertyInfo?>? RelationshipsProperties => relationshipsPropertyInfos;

        public Post() => Employees = new HashSet<Employee>();

        public override string ToString() => PostName;
    }
}

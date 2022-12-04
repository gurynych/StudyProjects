using DbLab2_Individual.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DbLab2_Individual.Models.FirstDatabase
{
    [Description("Сотрудники")]
    public partial class Employee : DatabaseItem
    {
        private Post? post;
        private readonly IEnumerable<PropertyInfo> relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Employee).GetProperty(nameof(Orders)),
            typeof(Employee).GetProperty(nameof(Gender)),
            typeof(Employee).GetProperty(nameof(Post))
        };

        public long Id { get; set; }

        [Description("ФИО")]
        public string FullName { get; set; }

        [Description("Возраст")]
        public long? Age { get; set; }

        [Description("Адрес")]
        public string? Address { get; set; }

        [Description("Телефон")]
        public string? PhoneNumber { get; set; }

        [Description("Пасспортные данные")]
        public string? PassportData { get; set; }

        public long? PostId { get; set; }

        [Description("Должность")]
        [ReadOnly(true)]
        public virtual Post? Post { get; set; }

        public long? GenderId { get; set; }

        [Description("Пол")]
        [ReadOnly(true)]
        public virtual Gender? Gender { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
       
        [NotMapped]
        public override IEnumerable<PropertyInfo> RelationshipsProperties => relationshipsPropertyInfos;

        public Employee() => Orders = new HashSet<Order>();

        public override string ToString() => FullName;        
    }
}

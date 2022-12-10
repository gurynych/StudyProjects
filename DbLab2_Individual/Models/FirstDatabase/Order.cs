using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DbLab2_Individual.Models.FirstDatabase
{
    [Description("Заказы")]
    public partial class Order : DatabaseItem
    {
        private readonly IEnumerable<PropertyInfo> relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Order).GetProperty(nameof(Employee))
        };

        public long Id { get; set; }        

        [Description("Дата")]
        public DateTime? OrderDate { get; set; }


        [Description("ФИО заказчика")]
        public string? CustomersFullName { get; set; }


        [Description("Телефон")]
        public string? PhoneNumber { get; set; }


        [Description("Стоимость")]
        public long? Cost { get; set; }


        [Description("Отметка о выполнении")]
        public bool IsDone { get; set; }
        
        [Description("Код сотрудника")]
        [ReadOnly(true)]
        public long? EmployeeId { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override IEnumerable<PropertyInfo> RelationshipsProperties => relationshipsPropertyInfos;

        public override string ToString()
        {
            return $"Заказ номер {Id}";
        }
    }
}

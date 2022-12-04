using DbLab2_Individual.Models;
using DbLab2_Individual.Models.FirstDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DbLab2_Individual.Models.SecondDatabase
{
    [Description("Авторы")]
    public partial class Auth : DatabaseItem
    {
        private readonly IEnumerable<PropertyInfo> relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Auth).GetProperty(nameof(Books))
        };

        public long Id { get; set; }

        [Description("Имя")]
        public string Name { get; set; } = null!;

        [Description("Возраст")]
        public long? Age { get; set; }
        
        public virtual ICollection<Book>? Books { get; set; }

        [NotMapped]
        public override IEnumerable<PropertyInfo?>? RelationshipsProperties => relationshipsPropertyInfos;

        public Auth() => Books = new HashSet<Book>();

        public override string ToString() => Name;
    }
}

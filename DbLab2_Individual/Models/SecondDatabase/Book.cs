using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Reflection.Metadata.BlobBuilder;

namespace DbLab2_Individual.Models.SecondDatabase
{

    [Description("Книги")]
    public partial class Book : DatabaseItem
    {
        private readonly IEnumerable<PropertyInfo> relationshipsPropertyInfos = new List<PropertyInfo>()
        {
            typeof(Book).GetProperty(nameof(Auths))
        };

        public long Id { get; set; }

        [Description("Название")]
        public string Title { get; set; } = null!;

        [Description("Кол-во страниц")]
        public long CountPage { get; set; }

        [Description("Цена")]
        public double? Price { get; set; }
        
        public virtual ICollection<Auth>? Auths { get; set; }

        [NotMapped]
        public override IEnumerable<PropertyInfo?>? RelationshipsProperties => relationshipsPropertyInfos;

        public Book() => Auths = new HashSet<Auth>();

        public override string ToString() => Title;
    }
}

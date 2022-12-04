using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.Models
{
    public interface IRelationships
    {
        [NotMapped]
        IEnumerable<PropertyInfo> RelationshipsProperties { get; }

        //IEnumerable<DatabaseItem> SelfRelationshipsData { get; set; }

        //Dictionary<PropertyInfo, IEnumerable<DatabaseItem>> KeyValuePairs { get; set; }
    }        
}

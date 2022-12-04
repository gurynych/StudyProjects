using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.Models
{
    public record class SelectionWrapper
    {
        public DatabaseItem? Item { get; set; }
        
        public bool IsSelected { get; set; }

        public SelectionWrapper(DatabaseItem? item)
        {
            Item = item;
        }

        public override string? ToString()
        {
            return Item?.ToString();
        }
    }
}

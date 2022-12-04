using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DbLab2_Individual.Models.Controls
{
    public abstract class ControlTemplate
    {
        public IEnumerable<SelectionWrapper>? Data { get; set; }

        public ControlTemplate(IEnumerable<SelectionWrapper>? data)
        {
            Data = data;
        }

        public override string? ToString()
        {
            Type? type = Data?.FirstOrDefault()?.Item?.GetType();
            string? typeName = type?.Name;

            return type?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? typeName;
        }
    }

}

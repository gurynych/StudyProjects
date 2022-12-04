using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DbLab2_Individual.Models.Controls
{
    public class CheckBoxControl : ControlTemplate
    {
        public CheckBoxControl(IEnumerable<SelectionWrapper>? data) : base(data)
        {
        }
    }
}

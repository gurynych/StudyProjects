using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLab2_Individual.Models.Controls
{
    public class RadioBtnControl : ControlTemplate
    {
        public RadioBtnControl(IEnumerable<SelectionWrapper>? data) : base(data)
        {
        }
    }
}

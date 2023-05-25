using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class ValueItem
    {
        public string LabelText { get; set; }
        public string ValueText { get; set; }
        public string Icon { get; set; }
        public string DateText { get; set; }

        public ValueItem(string label, string value, string date, string icon) 
        { 
            LabelText = label;
            ValueText = value;
            Icon = icon;
            DateText = date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class PortfolioAsset
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string HtmlColor { get; set; }
        public decimal Amount { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}

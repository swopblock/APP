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
        public Color HtmlColor { get; set; }
        public string Image { get; set; }
        public string Stars { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public string StringPercentage { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Price { get; set; }
        public float StartAngle { get; set; }
    }
}

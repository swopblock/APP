using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code.Data.Orders
{
    public enum OrderStatus { Preview, Filled, Pending, Canceled, Failed }
    public enum OrderType { Buy, Sell, Swap}
    public class OrderDetail
    {
        public OrderType Type;
        public OrderStatus Status;

        public string ShowStatus { get { return Status.ToString(); } private set { } }
        public string ShowType { get { return Type.ToString(); } private set { } }
        public string Symbol { get; set; }
        public string SwapToSymbol { get; set; }
        public string SymbolName { get; set; }
        public string SwapToName { get; set; }
        public string SymbolExchangeRate { get; set; }
        public string SwapToExchangeRate { get; set; }
        public string RewardsEarnedText { get; set; }

        public string Value { get; set; }

        public decimal Amount { get; set; }
        public decimal SwapAmount { get; set; }

        public DateTime SubmittedTime { get; set; }
        public DateTime FilledTime { get; set; }

        public decimal SymbolPrice { get; set; }
        public decimal SwapToPrice { get; set; }
        public decimal SwapPrice { get; set; }
        public decimal RewardsEarned { get; set; }

        public string GetName()
        {
            return "";
        }

        public decimal GetAccountAmount(string symbol)
        {
            return 0;
        }
    }
}

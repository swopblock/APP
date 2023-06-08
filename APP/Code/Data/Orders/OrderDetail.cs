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

        public string Symbol;
        public string SwapToSymbol;
        public decimal Amount;

        public DateTime SubmittedTime;
        public DateTime FilledTime;

        public decimal SymbolPrice;
        public decimal SwopPrice;
        public decimal RewardsEarned;

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

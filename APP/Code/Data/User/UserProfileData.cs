using APP.Code.Data.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code.Data.User
{
    public class UserProfileData
    {
        public UserProfileData()
        {

        }

        public static List<OrderDetail> GetDemoOrders()
        {
            return new List<OrderDetail>
            {
                new OrderDetail
                {
                    Type = OrderType.Swap,
                    Status = OrderStatus.Filled,
                    Symbol = "BTC",
                    SymbolName = "Bitcoin",
                    SwapToSymbol = "ETH",
                    SwapToName = "Ethereum",
                    Amount = 0.043245m,
                    SwapAmount = 0.373212m,
                    SwapPrice = 5733.55m,
                    SymbolPrice = 24453.02m,
                    RewardsEarned = 0.0000032452m,
                    FilledTime = DateTime.Now,
                    SubmittedTime = DateTime.Now,
                    RewardsEarnedText = 0.0000032452m + " SWOBL",
                    SymbolExchangeRate = "1 " + "BTC" + " = $" + 24453.02m,
                    SwapToExchangeRate = "1 " + "ETH" + " = $" + 5733.55m,
                    Value =  (5733.55m *  0.043245m).ToString("$#.##")
                },
                 new OrderDetail
                {
                    Type = OrderType.Swap,
                    Status = OrderStatus.Filled,
                    Symbol = "BTC",
                    SymbolName = "Bitcoin",
                    SwapToSymbol = "ETH",
                    SwapToName = "Ethereum",
                    Amount = 0.043245m,
                    SwapAmount = 0.373212m,
                    SwapPrice = 5733.55m,
                    SymbolPrice = 24453.02m,
                    RewardsEarned = 0.0000032452m,
                    FilledTime = DateTime.Now,
                    SubmittedTime = DateTime.Now,
                    RewardsEarnedText = 0.0000032452m + " SWOBL",
                    SymbolExchangeRate = "1 " + "BTC" + " = $" + 24453.02m,
                    SwapToExchangeRate = "1 " + "ETH" + " = $" + 5733.55m,
                    Value =  (5733.55m *  0.043245m).ToString("$#.##")
                },
                  new OrderDetail
                {
                    Type = OrderType.Swap,
                    Status = OrderStatus.Filled,
                    Symbol = "BTC",
                    SymbolName = "Bitcoin",
                    SwapToSymbol = "ETH",
                    SwapToName = "Ethereum",
                    Amount = 0.043245m,
                    SwapAmount = 0.373212m,
                    SwapPrice = 5733.55m,
                    SymbolPrice = 24453.02m,
                    RewardsEarned = 0.0000032452m,
                    FilledTime = DateTime.Now,
                    SubmittedTime = DateTime.Now,
                    RewardsEarnedText = 0.0000032452m + " SWOBL",
                    SymbolExchangeRate = "1 " + "BTC" + " = $" + 24453.02m,
                    SwapToExchangeRate = "1 " + "ETH" + " = $" + 5733.55m,
                    Value =  (5733.55m *  0.043245m).ToString("$#.##")
                }

            };
        }
    }
}

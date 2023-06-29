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

        public static List<PortfolioAsset> PortfolioAssets = null;

        public static List<PortfolioAsset> LoadDemo()
        {
            return new List<PortfolioAsset>
                    {
                new PortfolioAsset
                {
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Amount = 30.98765m,
                    PurchasePrice = 1000,
                    HtmlColor = Color.FromArgb("#6f89ff"),
                    Image = "ethereum.png",
                    Stars = ""
                },
                new PortfolioAsset
                {
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Amount = 1.56784m,
                    PurchasePrice = 10000,
                    HtmlColor = Color.FromArgb("#f2a900"),
                    Image = "bitcoin.png",
                    Stars = ""
                },
                new PortfolioAsset
                {
                    Name = "Swobble",
                    Symbol = "SWOBL",
                    Amount = 10000,
                    PurchasePrice = 0.5m,
                    HtmlColor = Color.FromArgb("#00dd00"),
                    Image = "swopblock.png",
                    Stars = ""                    
                },
                new PortfolioAsset
                {
                    Name = "Swobble Rewards",
                    Symbol = "YOUR REWARDS",
                    Amount = 2000,
                    PurchasePrice = 0.1m,
                    HtmlColor = Color.FromArgb("#ff00bc"),
                    Image = "reward_swopblock.png",
                    Stars = "reward_stars.png"
                }
            };
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
                    Value =  (5733.55m *  0.043245m).ToString("$#.##"),
                    Icon = "bitcoin.png",
                    IconStars = "",
                    RewardIcon = "swopblockreward.png",
                    SwapIcon = "ethereum.png",
                    LabelText = "Market Buy",
                    ValueText = "$1000.00",
                    DateText = "Jun 22, 2023"
                },
                 new OrderDetail
                {
                    Type = OrderType.Swap,
                    Status = OrderStatus.Filled,
                    Symbol = "SWOBL",
                    SymbolName = "Swobble",
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
                    Value =  (5733.55m *  0.043245m).ToString("$#.##"),
                    Icon = "swopblock.png",
                    IconStars = "",
                    RewardIcon = "reward_swopblock.png",
                    SwapIcon = "ethereum.png",
                    LabelText = "Market Sell",
                    ValueText = "$2,000.00",
                    DateText = "Jun 13, 2023"
                },
                  new OrderDetail
                {
                    Type = OrderType.Swap,
                    Status = OrderStatus.Filled,
                    Symbol = "ETH",
                    SymbolName = "Ethereum",
                    SwapToSymbol = "BTC",
                    SwapToName = "Bitcoin",
                    Amount = 1000,
                    SwapAmount = 0.373212m,
                    SwapPrice = 5733.55m,
                    SymbolPrice = 24453.02m,
                    RewardsEarned = 0.0000032452m,
                    FilledTime = DateTime.Now,
                    SubmittedTime = DateTime.Now,
                    RewardsEarnedText = 0.0000032452m + " SWOBL",
                    SymbolExchangeRate = "1 " + "BTC" + " = $" + 24453.02m,
                    SwapToExchangeRate = "1 " + "ETH" + " = $" + 5733.55m,
                    Value =  (5733.55m *  0.043245m).ToString("$#.##"),
                    Icon = "ethereum.png",
                    IconStars = "",
                    RewardIcon = "swopblockreward.png",
                    SwapIcon = "ethereum.png",
                    LabelText = "Market Buy",
                    ValueText = "$2,000.00",
                    DateText = "Jun 6, 2023"
                }

            };
        }
    }
}

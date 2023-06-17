using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class ChartData
    {
        public ConcurrentQueue<Pack> packs = new ConcurrentQueue<Pack>();

        public string Symbol = "";

        public int Time = 3600;
        public int Len = 42;

        public double max = 0;
        public double min = 0;
        public double price = 0;

        private bool sourceExchange = true;

        public ChartData(string symbol, int time = 3600, int len = 42, bool SourceIsExchange = true)
        {
            Symbol = symbol;
            Time = time;
            Len = len;

            sourceExchange = SourceIsExchange;

            GetData();
        }

        public void SetTime(ExchangeData.CoinTimeScale Time)
        {
            SetTime((int)Time);
        }

        public void SetTime(int Time)
        {
            this.Time = Time;
        }
        public double GetPrice()
        {
            return price;
        }
        public void GetData()
        {        
            if (Symbol != "")
            {
                Pack pack = new Pack();

                if (sourceExchange)
                {
                    pack.candles = ExchangeData.GetCandleData(Symbol, Time, Len);
                    pack.Symbol = Symbol;
                }
                else
                {
                    pack.candles = ExchangeData.GetLocalValues(Symbol, Time, Len);
                    pack.Symbol = Symbol;
                }

                if (pack.candles != null)
                {
                    if (pack.candles.Count > 0)
                    {
                        max = pack.candles.Max(x => x.hi);
                        min = pack.candles.Min(x => x.low);
                        price = pack.candles.OrderBy(x => x.TimeStamp).Last().close;

                        packs.Enqueue(pack);
                    }
                }
            }
        }
    }
}

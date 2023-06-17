using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class ChartContainer
    {
        public enum ChartSymbols 
        { 
            BTC,
            ETH,
            SWOBL,
            SWOBLR
        }

        private List<Pack> packs = new List<Pack>();

        public List<ChartData> Charts = new List<ChartData>
        {
            new ChartData("BTC", (int) ExchangeData.CoinTimeScale.FiveMinute, 200),
            new ChartData("ETH", (int) ExchangeData.CoinTimeScale.FiveMinute, 200),
            new ChartData("SWOBL", (int) ExchangeData.CoinTimeScale.FifteenMinute, 200, false),
            new ChartData("SWOBLR", (int) ExchangeData.CoinTimeScale.FifteenMinute, 200, false)
        };

        public void StartData()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    CheckData();
                    Thread.Sleep(3000);
                }

            }).Start();
        }
        public void CheckData()
        {
            foreach(ChartData chart in Charts) 
            {
                chart.GetData();            
            }
        }
        public List<Pack> CombinePacks(List<string> Symbols, int Time = 0, int Length = 0)
        {
            List<ChartData> data = new List<ChartData>();
            List<Pack> pksCandle = new List<Pack>();

            for (int i = 0; i < Charts.Count; i++)
            {
                if(Symbols.Contains(Charts[i].Symbol))
                {
                    data.Add(Charts[i]);

                    if(Time == 0)
                    {
                        Time = Charts[i].Time;
                    }

                    if(Length == 0)
                    {
                        Length = Charts[i].Len;
                    }
                }
            }

            if (Time != 0 && Length != 0)
            {
                double ctime = ExchangeData.ConvertToEpochTime(DateTime.Now);

                List<Pack> pks = new List<Pack>();

                for (int i = 0; i < data.Count; i++)
                {
                    Pack pk = GetPack(data[i]);

                    if(pk != null) pks.Add(pk);
                }

                for (int i = 0; i < packs.Count; i++)
                {
                    if (data.Where(x => x.Symbol == pks[i].Symbol).FirstOrDefault() != null)
                    {
                        Pack npak = new Pack();

                        npak.Symbol = packs[i].Symbol;

                        for (double tm = ctime; tm > (ctime - (Time * Length)); tm -= Time)
                        {
                            Candle can = new Candle();

                            can.TimeStamp = (long)tm;

                            Candle temp = pollNearest(tm, packs[i].candles);

                            double dif = temp.TimeStamp - tm;

                            npak.candles.Add(temp);
                        }

                        pksCandle.Add(npak);
                    }
                }
            }

            return pksCandle;
        }

        public List<Candle> SumTotal(List<PortfolioAsset> assets, List<Pack> packs)
        {
            List<Candle> pks = new List<Candle>();

            List<double> amounts = new List<double>();

            foreach(Pack p in packs)
            {
                PortfolioAsset asset = assets.Where(x => x.Symbol == p.Symbol).FirstOrDefault();

                if(asset != null)
                {
                    amounts.Add((double)asset.Amount);
                }
                else
                {
                    amounts.Add(0);
                }
            }

            for (int j = 0; j < packs.First().candles.Count; j++)
            {
                Candle can = new Candle();

                double hi = 0, lw = 0, op = 0, cl = 0;

                int total = 0;

                for (int i = 0; i < packs.Count; i++)
                {
                    if (j < packs[i].candles.Count)
                    {
                        Candle tmp = packs[i].candles[j];

                        if (tmp != null)
                        {
                            hi += (tmp.hi * amounts[i]);
                            lw += (tmp.low * amounts[i]);
                            op += (tmp.open * amounts[i]);
                            cl += (tmp.close * amounts[i]);

                            total++;
                        }
                    }
                }

                hi /= total;
                lw /= total;
                op /= total;
                cl /= total;

                can.open = op;
                can.close = cl;
                can.low = lw;
                can.hi = hi;

                pks.Add(can);
            }

            return pks;
        }

        private Candle pollNearest(double time, List<Candle> candles)
        {
            List<Candle> cndl = candles.OrderBy(
                x=> MathUtil.AbsoluteValue(x.TimeStamp - (long)time)
                ).ToList();

            return cndl.FirstOrDefault().Copy();
        }

        public Pack GetPack(ChartData data)
        {
            List<Pack> mpacks = new List<Pack>();

            while (!data.packs.IsEmpty)
            {
                Pack pk;
                
                if(data.packs.TryDequeue(out pk))
                {
                    mpacks.Add(pk);
                }
            }

            updatePacks(mpacks);

            Pack slctPk = packs.Where(x => x.Symbol == data.Symbol).FirstOrDefault();

            return slctPk;
        }

        public double GetPrice(string symbol)
        {
            ChartData cd = Charts.Where(x => x.Symbol.ToUpper() == symbol.ToUpper()).FirstOrDefault();

            if(cd != null)
            {
                return cd.GetPrice();
            }

            return -1;
        }

        private void updatePacks(List<Pack> pks)
        {
            if(pks != null)
            {
                if(pks.Count > 0)
                {
                    for(int i = 0; i < pks.Count; i++)
                    {
                        Pack pk = packs.Where(x => x.Symbol == pks[i].Symbol).FirstOrDefault();

                        if(pk != null)
                        {
                            pk.candles.AddRange(pks[i].candles);

                            pk.candles = pks[i].candles.DistinctBy(x=>x.TimeStamp).ToList();
                        }
                        else
                        {
                            packs.Add(pks[i]);
                        }
                    }

                }
            }
        }
    }
}

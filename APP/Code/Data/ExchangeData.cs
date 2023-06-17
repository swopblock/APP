using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class ExchangeData
    {
        public enum CoinTimeScale { OneMinute = 60, FiveMinute = 300, FifteenMinute = 900, OneHour = 3600, SixHour = 21600, OneDay = 86400 }
        public static List<Candle> GetCandleData(string symbol = "BTC", int TimeIncrement = 60, int NumberOfCandles = 20, int PastDataOffset = 0)
        {
            List<Candle> cdl = new List<Candle>();

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);

            int secondsSinceEpoch = (int)t.TotalSeconds;

            if (secondsSinceEpoch > 0)
            {
                long secondVal = secondsSinceEpoch - (TimeIncrement * (NumberOfCandles + PastDataOffset));
                long endVal = secondsSinceEpoch - (TimeIncrement * PastDataOffset);

                DateTime start = GetUnixTime(secondVal);

                string end = GetUnixTime(endVal).ToString("o");

                string req = "https://api.exchange.coinbase.com/products/" + symbol + "-USD/candles?start=" + start.ToString("o") + "&end=" + end + "&granularity=" + TimeIncrement.ToString();

                string dt = CoinData(req);

                List<jsonCandle> jc = GetJsonCandles(dt);

                foreach (jsonCandle jsc in jc)
                {
                    Candle cd = jsc.GetCandle();
                    cdl.Add(cd);
                }
            }

            return cdl;
        }

        private static List<jsonCandle> GetJsonCandles(string data)
        {
            string dt = data.Replace("[", string.Empty);

            string[] splt = dt.Split(']');

            List<jsonCandle> candls = new List<jsonCandle>();

            for (int i = 0; i < splt.Length; i++)
            {
                jsonCandle candle = new jsonCandle();

                if (splt[i] != string.Empty)
                {
                    if (splt[i][0] == ',')
                    {
                        splt[i] = splt[i].Substring(1);
                    }

                    string[] splTme = splt[i].Split(',');

                    if (splTme.Length > 5)
                    {
                        candle.time = splTme[0];
                        candle.low = splTme[1];
                        candle.high = splTme[2];
                        candle.open = splTme[3];
                        candle.close = splTme[4];
                        candle.volume = splTme[5];
                    }

                    candls.Add(candle);
                }
            }

            return candls;
        }

        public static List<Candle> GetLocalValues(string symbol = "BTC", int TimeIncrement = 60, int NumberOfCandles = 20, int PastDataOffset = 0)
        {
            List<Candle> candls = new List<Candle>();

            #region TEMPORARY WHILE NETWORK ISNT LIVE

            if (symbol.ToUpper() == "SWOBL")
            {
                candls.Add(new Candle
                {
                    close = 1,
                    open = 1,
                    hi = 1,
                    low = 1,
                    TimeStamp = (long)ConvertToEpochTime(DateTime.Now),
                    volume = 1
                });
            }
            else if (symbol.ToUpper() == "SWOBLR")
            {
                candls.Add(new Candle
                {
                    close = 1,
                    open = 1,
                    hi = 1,
                    low = 1,
                    TimeStamp = (long)ConvertToEpochTime(DateTime.Now),
                    volume = 1
                });
            }

            #endregion

            return candls;
        }

        public static DateTime GetUnixTime(long Seconds)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return time.ToLocalTime().AddSeconds(Seconds);
        }
        public static double ConvertToEpochTime(DateTime Time)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            TimeSpan span = Time - time;

            return span.TotalSeconds;
        }

        private static string CoinData(string url)
        {
            string info = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = ".NET Framework Test Client";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    info = reader.ReadToEnd();
                }

            }
            catch (WebException ex)
            {
                System.Threading.Thread.Sleep(100);
                HttpWebResponse xyz = ex.Response as HttpWebResponse;
                if (xyz == null) return info;
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(xyz.GetResponseStream(), encoding))
                {
                    info = reader.ReadToEnd();
                }
            }

            return info;
        }

        public class jsonCandle
        {
            public string time { get; set; }
            public string low { get; set; }
            public string high { get; set; }
            public string open { get; set; }
            public string close { get; set; }
            public string volume { get; set; }

            public Candle GetCandle()
            {
                return new Candle
                {
                    TimeStamp = Convert.ToInt64(time),
                    low = Convert.ToDouble(low),
                    hi = Convert.ToDouble(high),
                    open = Convert.ToDouble(open),
                    close = Convert.ToDouble(close),
                    volume = Convert.ToDouble(volume)
                };
            }
        }
    }
}

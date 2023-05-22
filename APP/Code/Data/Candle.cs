using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class Candle
    {
        public double hi, low, open, close, volume = 0;
        public long TimeStamp = 0;

        public Candle Copy()
        {
            return new Candle
            {
                hi = hi,
                low = low,
                open = open,
                close = close,
                volume = volume,
                TimeStamp = TimeStamp
            };
        }
    }
}

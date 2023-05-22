using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class CurveSet
    {
        private string curve = "";
        private string space = "";

        private bool Error = false;

        public CurveSet(string curve, string space)
        {
            this.curve = curve;
            this.space = space;
        }

        public string GetCurve() { return curve; }
        public string GetSpace() { return space; }

        public bool Failed() { return Error; } 
        public void SetError(bool error) { Error = error;}
    }
}

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
        private string lineDown = "";
        private string arc = "";

        private bool Error = false;

        public CurveSet(string curve, string space, string down = "", string arc = "")
        {
            this.curve = curve;
            this.space = space;
            this.lineDown = down;
            this.arc = arc;
        }

        public string GetCurve() { return curve; }
        public string GetSpace() { return space; }

        public string GetLineDown() { return lineDown; }
        public string GetArc() { return arc; }

        public bool Failed() { return Error; } 
        public void SetError(bool error) { Error = error;}
    }
}

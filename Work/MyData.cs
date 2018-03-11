using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work
{
    class MyData
    {
        public string rwmc { get; set; }
        public int tpdj { get; set; }
        public string scmc { get; set; }
        public int xysl { get; set; }

        public MyData(string r, int t, string s, int x)
        {
            rwmc = r;
            tpdj = t;
            scmc = s;
            xysl = x;
        }
    }
}

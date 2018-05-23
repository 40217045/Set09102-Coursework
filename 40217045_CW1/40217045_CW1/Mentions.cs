using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Mentions
    {
        private string handle;
        private int trending;

        public string Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        public int Trending
        {
            get { return trending; }
            set { trending = value; }
        }

        public Mentions(string t, int tre)
        {
            Handle = t;
            Trending = tre;
        }
    }
}

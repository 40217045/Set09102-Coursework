using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Hashtags
    {

        private string tag;
        private int trending;

        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public int Trending
        {
            get { return trending; }
            set { trending = value; }
        }

        public Hashtags(string t, int tre)
        {
            Tag = t;
            Trending = tre;
        }
    }
}

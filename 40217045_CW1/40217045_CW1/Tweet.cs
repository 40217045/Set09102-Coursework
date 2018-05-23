using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Tweet
    {
        private string tweetID;
        private string from;
        private string message;

        public string TweetID
        {
            get { return tweetID; }
            set { tweetID = value; }
        }
        public string From
        {
            get { return from; }
            set { from = value; }
        }
        
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}

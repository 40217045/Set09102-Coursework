using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Sms
    {
        private string smsID;
        private double from;
        private double to;
        private string message;

        public string SmsID
        {
            get { return smsID; }
            set { smsID = value; }
        }
        public double From
        {
            get { return from; }
            set { from = value; }
        }
        public double To
        {
            get { return to; }
            set { to = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}

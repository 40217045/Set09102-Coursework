using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class User
    {
        private string username;
        private string name;
        private string emailAdd;
        private string twitter;
        private double phonenumber;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Name {
            get { return name; }
            set { name = value; }
        }
        public string EmailAdd
        {
            get { return emailAdd; }
            set { emailAdd = value; }
        }
        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }
        public double Phonenumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }
    }
}

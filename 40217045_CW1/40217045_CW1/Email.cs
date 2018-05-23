using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _40217045_CW1
{
    class Email
    {
        private string messageID;
        private string sender;
        private string recipient;
        private string subject;
        private string incident;
        private string centreCode;
        private string message;


        public string MessageID
        {
            get { return messageID; }
            set { messageID = value; }
        }
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Recipient
        {
            get { return recipient; }
            set { recipient = value; }
        }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Incident
        {
            get { return incident; }
            set { incident = value; }
        }
        public string CentreCode
        {
            get { return centreCode; }
            set { centreCode = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}

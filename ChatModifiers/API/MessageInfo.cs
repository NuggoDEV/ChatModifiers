using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModifiers.API
{
    public class MessageInfo
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public string Channel { get; set; }
        public DateTime TimeStamp { get; set; }

        public MessageInfo(string message, string author, string channel, DateTime timeStamp)
        {
            Message = message;
            Author = author;
            Channel = channel;
            TimeStamp = timeStamp;
        }
    }
}

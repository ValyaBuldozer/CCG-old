using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture.Common
{
    public class Message
    {
        public string Content { set; get; }
        public string Length { set; get; }
        public MessageType Type { set; get; }
        public Encoder Encoder { set; get; }

        public Message()
        {
            Encoder = new Encoder();
        }

        public Message(string content)
        {
            Encoder = new Encoder();
            Content = content;
        }

        public Message(string content,MessageType type)
        {
            Content = content;
            Type = type;
            Encoder = new Encoder();
        }
    }
}

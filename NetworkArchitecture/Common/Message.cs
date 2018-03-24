using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture.Common
{
    class Message
    {
        public string Content { set; get; }
        public string Length { set; get; }
        public MessageType Type { set; get; }

        public byte[] ContentBytes
        {
            get => Encoding.UTF8.GetBytes(Content);
            set => Content = Encoding.UTF8.GetString(value);
        }

        public byte[] LengthBytes => Encoding.UTF8.GetBytes(Length);

        public byte[] TypeBytes { get; set; }

        public Message() { }

        public Message(string content,MessageType type)
        {
            Content = content;
            Type = type;
            
        }
    }
}

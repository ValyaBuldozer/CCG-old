using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture
{
    public class EnumsService
    {
        public static Dictionary<byte,MessageType> MessageTypeConverDictionary = new Dictionary<byte, MessageType>()
        {
            //KeyValuePair<byte,MessageType>, 
        };
    }

    public enum MessageType
    {
        Default
    }
}

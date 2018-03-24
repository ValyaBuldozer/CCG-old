using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkArchitecture
{
    class EnumsService
    {
        public static Dictionary<byte,MessageType> MessageTypeConverDictionary = new Dictionary<byte, MessageType>()
        {
            //KeyValuePair<byte,MessageType>, 
        };
    }

    enum MessageType
    {
        Default
    }
}

using Osaru.MessagePack;
using System;


namespace Osaru
{
    public static partial class ArraySegmentExtensions
    {
        public static MessagePackParser ParseAsMessagePack(this ArraySegment<Byte> bytes)
        {
            return MessagePackParser.Parse(bytes);
        }
    }
}

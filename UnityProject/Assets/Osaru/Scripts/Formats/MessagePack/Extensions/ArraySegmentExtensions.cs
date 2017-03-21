using System;


namespace Osaru.MessagePack
{
    public static class ArraySegmentExtensions
    {
        public static MessagePackParser ParseAsMessagePack(this ArraySegment<Byte> bytes)
        {
            return MessagePackParser.Parse(bytes);
        }
    }
}

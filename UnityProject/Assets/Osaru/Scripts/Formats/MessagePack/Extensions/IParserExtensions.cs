using System;


namespace Osaru.MessagePack
{
    public static class IParserExtensions
    {
        public static ArraySegment<Byte> ToMessagePack<PARSER>(this PARSER parser)
            where PARSER : IParser<PARSER>
        {
            var formatter = new MessagePackFormatter();
            parser.Convert(formatter);
            return formatter.GetStore().Bytes;
        }
    }
}

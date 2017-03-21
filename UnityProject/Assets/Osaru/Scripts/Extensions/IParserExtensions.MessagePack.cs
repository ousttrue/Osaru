using Osaru.MessagePack;
using System;


namespace Osaru
{
    public static partial class IParserExtensions
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

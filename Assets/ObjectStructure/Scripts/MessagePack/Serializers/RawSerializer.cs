using System;
using System.Collections.Generic;


namespace NMessagePack.Serializers {
    public class RawSerializer<T> : SerializerBase<T>
        where T: IList<Byte>
    {
        protected override void NonNullSerialize(MsgPackWriter w, T t)
        {
            w.MsgPack(t);
        }
    }
}

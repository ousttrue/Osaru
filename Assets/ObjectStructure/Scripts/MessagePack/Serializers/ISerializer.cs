using System;


namespace NMessagePack.Serializers
{
    public interface ISerializer
    {
        void Serialize(MsgPackWriter w, Object o);
    }
}

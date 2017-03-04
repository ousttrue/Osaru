using System;


namespace ObjectStructure.MessagePack.Serializers
{
    public interface ISerializer
    {
        void Serialize(MsgPackWriter w, Object o);
    }
}

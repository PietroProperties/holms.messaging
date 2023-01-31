using Google.Protobuf;
using System;

namespace HOLMS.Messaging
{
    public interface IMessageChannel : IDisposable
    {
        void Publish(string topic, IMessage msg);
        IMessageListener BindSharedQueue(MessageListener.MessageReceivedHandler h,
            string[] topics, string queueName);
        IMessageListener BindPrivateQueue(MessageListener.MessageReceivedHandler h,
            string[] topics);
        void Close();
    }
}

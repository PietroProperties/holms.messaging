using Google.Protobuf;

namespace HOLMS.Messaging {
    public interface IMessageChannel {
        void Publish(string topic, IMessage msg);
        IMessageListener CreateListenerForTopics(MessageListener.MessageReceivedHandler h,
            string[] topics);
        void Close();
    }
}

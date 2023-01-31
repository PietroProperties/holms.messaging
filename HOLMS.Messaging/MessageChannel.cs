using Google.Protobuf;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace HOLMS.Messaging
{
    public class MessageChannel : IMessageChannel
    {
        private readonly ILogger _l;
        private readonly IModel _m;

        internal MessageChannel(ILogger l, IModel m)
        {
            _l = l;
            _m = m;
        }

        public void Publish(string topic, IMessage msg)
        {
            _m.BasicPublish(MessageConnection.ExchangeName,
                topic, null, msg.ToByteArray());
        }

        public IMessageListener BindSharedQueue(MessageListener.MessageReceivedHandler h, string[] topics,
                string queueName)
        {
            var ml = new MessageListener(_l, _m, topics, queueName);
            ml.MessageReceived += h;

            return ml;
        }

        public IMessageListener BindPrivateQueue(MessageListener.MessageReceivedHandler h,
                string[] topics)
        {
            var ml = new MessageListener(_l, _m, topics);
            ml.MessageReceived += h;

            return ml;
        }

        public void Close()
        {
            _m.Close();
        }
        public void Dispose()
        {
            _m.Dispose();
        }
    }
}

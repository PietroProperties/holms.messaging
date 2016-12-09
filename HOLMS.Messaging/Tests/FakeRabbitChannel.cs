using System.Collections.Generic;
using Google.Protobuf;

namespace HOLMS.Messaging.Tests {
    public class FakeRabbitChannel : IMessageChannel {
        public delegate void PublicationArgs (string topic, object msg);

        public event PublicationArgs Publication;
        public List<RecordedPublication> Publications { get; protected set; }
        public bool IsOpen;

        public FakeRabbitChannel() {
            Publications = new List<RecordedPublication>();
        }

        public void Publish(string topic, IMessage msg) {
            Publications.Add(new RecordedPublication(topic, msg));
            Publication?.Invoke(topic, msg);
        }

        public IMessageListener BindSharedQueue(MessageListener.MessageReceivedHandler h, string[] topics, string queueName) {
            return new FakeMessageListener();
        }

        public IMessageListener BindPrivateQueue(MessageListener.MessageReceivedHandler h, string[] topics) {
            return new FakeMessageListener();
        }

        public void Close() {
            IsOpen = false;
        }
    }
}

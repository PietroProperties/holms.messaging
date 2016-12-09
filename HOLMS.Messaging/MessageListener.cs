using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HOLMS.Messaging {
    public class MessageListener : IMessageListener {
        private readonly ILogger _log;
        private readonly IModel _m;
        private readonly string[] _topics;
        private readonly string _queueName;

        private EventingBasicConsumer _bc;

        public delegate void MessageReceivedHandler(string routingKey, byte[] msg);
        public event MessageReceivedHandler MessageReceived;

        internal MessageListener(ILogger l, IModel m, string[] topics, string queueName = "") {
            _log = l;
            _m = m;
            _topics = topics;
            _queueName = queueName;
        }

        public void Start() {
            QueueDeclareOk queue;
            if (_queueName == string.Empty) {
                // Private, throwaway queue
                queue = _m.QueueDeclare(string.Empty, false, true, false);
            } else {
                // Shared queue. Durable, used for HA
                queue = _m.QueueDeclare(_queueName, true, false, false);
            }

            foreach (var topic in _topics) {
                _m.QueueBind(queue.QueueName, MessageConnection.ExchangeName, topic);
            }

            // Note: this starts a thread
            // (1) Do not share the channel between threads
            // (2) Be sure the connection is disposed and stopped, otherwise the thread
            // will continue running even when it should stop!
            _bc = new EventingBasicConsumer(_m);
            _bc.Received += OnMessage;
            _m.BasicConsume(queue.QueueName, true, _bc);
        }   

        public void Stop() {
            _log.LogInformation("Closing RabbitMQ connection");
            _bc.Received -= OnMessage;
        }

        private void OnMessage(object sender, BasicDeliverEventArgs ea) {
            MessageReceived?.Invoke(ea.RoutingKey, ea.Body);
        }
    }
}

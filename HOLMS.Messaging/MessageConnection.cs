using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace HOLMS.Messaging {
    public class MessageConnection : IMessageConnection {
        public const string ExchangeName = "holms";

        private readonly ILogger _log;
        private readonly IConnection _cn;

        internal MessageConnection(ILogger l, IConnection cn) {
            _cn = cn;
            _log = l;
        }

        public IMessageChannel GetChannel() {
            var m = _cn.CreateModel();
            m.ExchangeDeclare(ExchangeName, ExchangeType.Topic);

            return new MessageChannel(_log, m);
        }

        public void Close() {
            _cn.Close();
        }
    }
}

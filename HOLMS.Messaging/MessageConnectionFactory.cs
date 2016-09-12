using log4net;
using RabbitMQ.Client;

namespace HOLMS.Messaging {
    public class MessageConnectionFactory : IMessageConnectionFactory {
        private readonly ILog _l;
        private readonly ConnectionFactory _rabbitcf;
        public string Hostname { get; }

        public MessageConnectionFactory(ILog l, string hostname) {
            _l = l;
            Hostname = hostname;
            _rabbitcf = new ConnectionFactory { HostName = hostname };
        }

        public IMessageConnection OpenConnection() {
            var cn = _rabbitcf.CreateConnection();
            return new MessageConnection(_l, cn);
        }
    }
}

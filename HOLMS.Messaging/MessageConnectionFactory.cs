using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace HOLMS.Messaging {
    public class MessageConnectionFactory : IMessageConnectionFactory {
        private readonly ILogger _l;
        private readonly ConnectionFactory _rabbitcf;
        public string Hostname { get; }

        public MessageConnectionFactory(ILogger l, string hostname, string username, string password) {
            _l = l;
            Hostname = hostname;
            _rabbitcf = new ConnectionFactory {
                HostName = hostname,
                UserName = username,
                Password = password,
            };
        }

        public IMessageConnection OpenConnection() {
            var cn = _rabbitcf.CreateConnection();
            return new MessageConnection(_l, cn);
        }
    }
}

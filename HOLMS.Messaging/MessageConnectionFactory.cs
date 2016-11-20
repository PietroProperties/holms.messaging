using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace HOLMS.Messaging {
    public class MessageConnectionFactory : IMessageConnectionFactory {
        private readonly ILogger _l;
        private readonly ConnectionFactory _rabbitcf;
        public string Hostname { get; }

        public MessageConnectionFactory(ILogger l, string hostname, string username = null, string password = null) {
            _l = l;
            Hostname = hostname;
            _rabbitcf = new ConnectionFactory {
                HostName = hostname,
            };
            //It appears that using the setter at all, even if the argument is null,
            //attempts to use the credentials. Instead, we ensure that the properties
            //are only set when they are not null
            if (username != null) {
                _rabbitcf.UserName = username;
            }
            if (password != null) {
                _rabbitcf.Password = password;
            }
        }

        public IMessageConnection OpenConnection() {
            var cn = _rabbitcf.CreateConnection();
            return new MessageConnection(_l, cn);
        }
    }
}

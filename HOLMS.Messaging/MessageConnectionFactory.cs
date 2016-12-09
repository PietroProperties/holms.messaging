using System.Net.Security;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace HOLMS.Messaging {
    public class MessageConnectionFactory : IMessageConnectionFactory {
        private readonly ILogger _l;
        private readonly ConnectionFactory _rabbitcf;
        public string Hostname { get; }

        public MessageConnectionFactory(ILogger l, MessagingConfiguration connectionConfig) {
            _l = l;
            Hostname = connectionConfig.Host;
            _rabbitcf = new ConnectionFactory {
                HostName = connectionConfig.Host,
                Port = connectionConfig.Port,
            };
            //It appears that using the setter at all, even if the argument is null,
            //attempts to use the value. Instead, we ensure that the properties
            //are only set when they are not null
            if (connectionConfig.User != null) {
                _rabbitcf.UserName = connectionConfig.User;
            }
            if (connectionConfig.Password != null) {
                _rabbitcf.Password = connectionConfig.Password;
            }
            if (connectionConfig.VHost != null) {
                _rabbitcf.VirtualHost = connectionConfig.VHost;
            }
            if (connectionConfig.Protocol == RabbitProtocol.AmqpS) {
                _rabbitcf.Ssl = new SslOption(connectionConfig.Host, enabled: true);
                _rabbitcf.Ssl.AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNotAvailable |
                                                       SslPolicyErrors.RemoteCertificateNameMismatch;
            }
        }

        public IMessageConnection OpenConnection() {
            var cn = _rabbitcf.CreateConnection();
            return new MessageConnection(_l, cn);
        }
    }
}

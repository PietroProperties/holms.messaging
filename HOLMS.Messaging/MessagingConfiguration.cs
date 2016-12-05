using System;

namespace HOLMS.Messaging {
    public enum RabbitProtocol {
        Amqp,
        AmqpS
    }

    public class MessagingConfiguration {
        public string Host { get; protected set; }
        public ushort Port { get; protected set; }
        public string User { get; protected set; }
        public string Password { get; protected set; }
        public string VHost { get; protected set; }
        public RabbitProtocol Protocol { get; protected set; }

        public MessagingConfiguration(string connectionString) {
            //example: amqp://user:pass@hostname:port/vhost
            var protocolSplit = connectionString.Split(new string[] { "://" }, 2, StringSplitOptions.None);
            if (protocolSplit.Length != 2) {
                throw new ArgumentException($"No protocol specified in string {connectionString}");
            }

            var protocol = protocolSplit[0].ToLower();
            if (protocol == "amqp") {
                Protocol = RabbitProtocol.Amqp;
            } else if (protocol == "amqps") {
                Protocol = RabbitProtocol.AmqpS;
            } else {
                throw new ArgumentException($"Invalid protocol {protocol}");
            }

            var connection = protocolSplit[1];
            if (connection.Contains("@")) {
                var allCredentials = connection.Split('@')[0].Split(new string[] { ":" }, 2, StringSplitOptions.None);
                //Allow colons in password, just not in username
                User = allCredentials[0];
                if (allCredentials.Length > 1) {
                    Password = allCredentials[1];
                }
                //strip the credentials from the string. Assumes only one "@" in the entire string, and it can't be in the password
                connection = connection.Split('@')[1];
            }
            var lastColon = connection.LastIndexOf(':');
            Host = connection.Substring(0, lastColon);
            var portAndVHost = connection.Substring(lastColon + 1).Split('/');
            
            ushort tcpPort;
            if (!ushort.TryParse(portAndVHost[0], out tcpPort)) {
                throw new ArgumentException($"Failed to parse messaging port {tcpPort} as valid port number (integer between 1 and 65535");
            }
            Port = tcpPort;

            if (portAndVHost.Length > 1) {
                VHost = portAndVHost[1];
            }
        }
    }
}

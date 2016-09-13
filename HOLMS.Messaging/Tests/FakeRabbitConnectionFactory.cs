using System.Collections.Generic;

namespace HOLMS.Messaging.Tests {
    public class FakeRabbitConnectionFactory : IMessageConnectionFactory {
        public string Hostname { get; }
        public List<FakeRabbitConnection> Connections;

        public FakeRabbitConnectionFactory() {
            Hostname = "FAKE";
            Connections = new List<FakeRabbitConnection>();
        }

        public IMessageConnection OpenConnection() {
            var rc = new FakeRabbitConnection();
            Connections.Add(rc);

            return rc;
        }
    }
}

using NUnit.Framework;
using System;

namespace HOLMS.Messaging {
    class MessagingConfigurationTests {
        [Test]
        public void MissingProtocolThrows() {
            Assert.Throws<ArgumentException>(() => new MessagingConfiguration("pqma/user:pass@localhost:1234/vhost"));
        }

        [Test]
        public void InvalidProtocolThrows() {
            Assert.Throws<ArgumentException>(() => new MessagingConfiguration("pqma://user:pass@localhost:1234/vhost"));
        }

        [Test]
        public void ConnectionStringWithoutCredentialsDoesNotPopulateUsernameOrPassword() {
            var config = new MessagingConfiguration("amqp://localhost:1234/myVHost");
            Assert.IsNull(config.User);
            Assert.IsNull(config.Password);
            Assert.AreEqual("localhost", config.Host);
            Assert.AreEqual(1234, config.Port);
            Assert.AreEqual("myVHost", config.VHost);
        }

        [Test]
        public void ConnectionStringWithJustUsernameDoesNotPopulatePassword() {
            var config = new MessagingConfiguration("amqp://username@localhost:1234/myVHost");
            Assert.AreEqual("username", config.User);
            Assert.IsNull(config.Password);
            Assert.AreEqual("localhost", config.Host);
            Assert.AreEqual(1234, config.Port);
            Assert.AreEqual("myVHost", config.VHost);
        }

        [Test]
        public void ConnectionStringWithoutVHostDoesNotPopulateVHost() {
            var config = new MessagingConfiguration("amqp://localhost:1234");
            Assert.AreEqual("localhost", config.Host);
            Assert.IsNull(config.VHost);
        }

        [Test]
        public void PasswordWithColonParsesCorrectly() {
            var config = new MessagingConfiguration("amqp://username:pass:word@localhost:1234/myVHost");
            Assert.AreEqual("username", config.User);
            Assert.AreEqual("pass:word", config.Password);
            Assert.AreEqual("localhost", config.Host);
            Assert.AreEqual(1234, config.Port);
            Assert.AreEqual("myVHost", config.VHost);
        }

        [Test]
        public void ConnectionStringWithAllFieldsParsesCorrectly() {
            var config = new MessagingConfiguration("amqp://username:password@localhost:1234/myVHost");
            Assert.AreEqual("username", config.User);
            Assert.AreEqual("password", config.Password);
            Assert.AreEqual("localhost", config.Host);
            Assert.AreEqual(1234, config.Port);
            Assert.AreEqual("myVHost", config.VHost);
        }
    }
}

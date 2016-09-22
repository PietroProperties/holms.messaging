using System.Collections.Generic;
using System.Linq;

namespace HOLMS.Messaging.Tests {
    public class FakeRabbitConnection : IMessageConnection {
        private readonly List<FakeRabbitChannel> _allocatedChannels;

        public FakeRabbitConnection() {
            _allocatedChannels = new List<FakeRabbitChannel>();
        }

        public int AllocatedChannelCount => _allocatedChannels.Count;
        public int OpenChannelCount => _allocatedChannels.Count(x => x.IsOpen);

        public FakeRabbitChannel GetChannel(int allocatedIdx) {
            return _allocatedChannels[allocatedIdx];
        }

        public List<FakeRabbitChannel> GetChannels() {
            return _allocatedChannels;
        }

        public IMessageChannel GetChannel() {
            var model = new FakeRabbitChannel();
            _allocatedChannels.Add(model);

            return model;
        }

        public void Close() { }
    }
}

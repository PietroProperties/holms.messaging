using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HOLMS.Messaging {
    // This is very non-portable. I don't care for now, this is
    // just an initial prototype implementation and these messages
    // are transient, not long-lived.
    internal static class MessageSerializer {
        public static byte[] Serialize(object o) {
            var ms = new MemoryStream();
            var encoder = new BinaryFormatter();
            encoder.Serialize(ms, o);

            return ms.ToArray();
        }

        public static object Deserialize(byte[] msg) {
            var ms = new MemoryStream(msg);
            var decoder = new BinaryFormatter();
            return decoder.Deserialize(ms);
        }
    }
}

namespace HOLMS.Messaging.Tests {
    public class RecordedPublication {
        public string Routingkey;
        public object Message;

        public RecordedPublication(string rk, object msg) {
            Routingkey = rk;
            Message = msg;
        }
    }
}

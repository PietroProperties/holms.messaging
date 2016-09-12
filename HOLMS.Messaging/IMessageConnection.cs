namespace HOLMS.Messaging {
    public interface IMessageConnection {
        IMessageChannel GetChannel();
        void Close();
    }
}

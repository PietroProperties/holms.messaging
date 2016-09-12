namespace HOLMS.Messaging {
    public interface IMessageConnectionFactory {
        IMessageConnection OpenConnection();
        string Hostname { get; }
    }
}

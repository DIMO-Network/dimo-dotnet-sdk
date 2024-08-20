namespace Dimo.Client.Streamr
{
    public abstract class InternalEvents
    {
        public abstract void MessagePublished(StreamMessage message);
        public abstract void StreamPartSubscribed();
    }

    public class StreamCreationEvent
    {
        public StreamId StreamId { get; set; }
        public StreamMetadata Metadata { get; set; }
        public long BlockNumber { get; set; }
    }

    public abstract class StreamrClientEvents : InternalEvents
    {
        public abstract void StreamCreated(StreamCreationEvent payload);
        public abstract void StreamAddedToStorageNode();
        public abstract void StreamRemovedFromStorageNode();
        public abstract void EncryptionKeyToLocalStore(string keyId);
        public abstract void ContractTransationConfirmed(string methodName, object contractTransactionReceipt);
    }

    public class StreamMetadata
    {
        public int Partitions { get; set; }
        public string Description { get; set; }
        public Config Config { get; set; }
        public int StorageDays { get; set; }
        public int InactivityThresholdHours { get; set; }
    }

    public class Config
    {
        public Field[] Fields { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
    }
    
    public enum FieldType
    {
        Int,
        String,
        Bool,
        List,
        Object
    }

    public class Stream
    {
        
    }

    public class StreamrClientEventEmitter : ObservableEventEmitter<StreamrClientEvents>
    {
        
    }
}
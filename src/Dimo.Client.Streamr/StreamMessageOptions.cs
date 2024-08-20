using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Dimo.Client.Streamr.Helpers;

namespace Dimo.Client.Streamr
{
    public class MessageId
    {
        public StreamId StreamId { get; private set; }
        public int StreamPartition { get; private set; }
        public long Timestamp { get; private set; }
        public int SequenceNumber { get; private set; }
        public EthereumAddress PublisherId { get; private set; }
        public string MessageChainId { get; private set; }

        public MessageId(StreamId streamId, int streamPartition, long timestamp, int sequenceNumber, EthereumAddress publisherId, string messageChainId)
        {
            if (streamPartition < 0) throw new ArgumentOutOfRangeException(nameof(streamPartition));
            if (timestamp < 0) throw new ArgumentOutOfRangeException(nameof(timestamp));
            if (sequenceNumber < 0) throw new ArgumentOutOfRangeException(nameof(sequenceNumber));
            
            StreamId = streamId;
            StreamPartition = streamPartition;
            Timestamp = timestamp;
            SequenceNumber = sequenceNumber;
            PublisherId = publisherId;
            MessageChainId = messageChainId;
        }

        public StreamPartitionId GetStreamPartitionId()
        {
            if (!NumberUtils.IsSafeInteger(StreamPartition) || StreamPartition < 0 || StreamPartition > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(StreamPartition));
            }
            
            return $"{StreamId}/#/{StreamPartition}";
        }
        
        public MessageReference ToMessageRef()
        {
            return new MessageReference(Timestamp, SequenceNumber);
        }
    }

    public class StreamMessage : StreamMessageOptions
    {
        public StreamId StreamId => MessageId.StreamId;
        public int StreamPartition => MessageId.StreamPartition;
        public StreamPartitionId StreamPartitionId => MessageId.GetStreamPartitionId();
        public long Timestamp => MessageId.Timestamp;
        public int SequenceNumber => MessageId.SequenceNumber;
        public EthereumAddress PublisherId => MessageId.PublisherId;
        public string MessageChainId => MessageId.MessageChainId;
        
        public StreamMessage(
            MessageId messageId, 
            MessageReference previousMessageReference, 
            StreamMessageType messageType, 
            uint[] content, 
            ContentType contentType, 
            uint[] signature, 
            SignatureType signatureType, 
            EncryptionType encryptionType, 
            string groupKeyId, 
            EncryptedGroupKey newGroupKey
            ) 
            : base(messageId, 
                previousMessageReference, 
                messageType, 
                content, 
                contentType, 
                signature, 
                signatureType, 
                encryptionType, 
                groupKeyId, 
                newGroupKey)
        { }
    }
    
    public abstract class StreamMessageOptions
    {
        public MessageId MessageId { get; private set; }
        public MessageReference PreviousMessageReference { get; private set; }
        public StreamMessageType MessageType { get; private set; }
        public uint[] Content { get; private set; }
        public ContentType ContentType { get; private set; }
        public uint[] Signature { get; private set; }
        public SignatureType SignatureType { get; private set; }
        public EncryptionType EncryptionType { get; private set; }
        public string GroupKeyId { get; private set; }
        public EncryptedGroupKey NewGroupKey { get; private set; }

        protected StreamMessageOptions(
            MessageId messageId, 
            MessageReference previousMessageReference, 
            StreamMessageType messageType, 
            uint[] content, 
            ContentType contentType, 
            uint[] signature, 
            SignatureType signatureType, 
            EncryptionType encryptionType, 
            string groupKeyId, 
            EncryptedGroupKey newGroupKey)
        {
            ValidateSequence(messageId, previousMessageReference);
            if (encryptionType == EncryptionType.Aes)
            {
                if(string.IsNullOrEmpty(groupKeyId))
                    throw new ArgumentNullException(nameof(groupKeyId));
            }
            
            MessageId = messageId;
            PreviousMessageReference = previousMessageReference;
            MessageType = messageType;
            Content = content;
            ContentType = contentType;
            Signature = signature;
            SignatureType = signatureType;
            EncryptionType = encryptionType;
            GroupKeyId = groupKeyId;
            NewGroupKey = newGroupKey;
        }

        private void ValidateSequence(MessageId messageId, MessageReference previousMessageRef)
        {
            if (previousMessageRef == null) return;
            
            var comparison = messageId.ToMessageRef().CompareTo(previousMessageRef);

            if (comparison == 0)
                throw new ArgumentOutOfRangeException();

            if (comparison < 0)
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public class EncryptedGroupKey : ValueObject
    {
        public string Id { get; private set; }
        public uint[] Data { get; private set; }
        
        public EncryptedGroupKey(string id, uint[] data)
        {
            Id = id;
            Data = data;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Data;
        }
    }

    public enum StreamMessageType
    {
        Message = 27,
        GroupKeyRequest = 28,
        GroupKeyResponse = 29,
    }

    public enum EncryptionType
    {
        None = 0,
        Aes = 2,
    }

    public enum SignatureType
    {
        LegacySecp256k1 = 0,
        Secp256k1 = 1,
        Erc1271 = 2,
    }

    public class MessageReference : ValueObject
    {
        public long TimeStamp { get; private set; }
        public int SequenceNumber { get; private set; }
        
        public MessageReference(long timeStamp, int sequenceNumber)
        {
            TimeStamp = timeStamp;
            SequenceNumber = sequenceNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TimeStamp;
            yield return SequenceNumber;
        }
    }

    public class StreamPartitionId : ValueObject
    {
        private readonly string _value;
        
        public StreamPartitionId(string value)
        {
            _value = value;
        }
        
        public override string ToString() => _value;
        public static implicit operator string(StreamPartitionId streamPartitionId) => streamPartitionId._value;
        public static implicit operator StreamPartitionId(string value) => new StreamPartitionId(value);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }

    public class EthereumAddress : ValueObject
    {
        private readonly string _value;
        
        public EthereumAddress(string value)
        {
            _value = value;
        }
        
        public override string ToString() => _value;
        public static implicit operator string(EthereumAddress ethereumAddress) => ethereumAddress._value;
        public static implicit operator EthereumAddress(string value) => new EthereumAddress(value);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }

    public class StreamId : ValueObject
    {
        private readonly string _value;
        
        public StreamId(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;
        public static implicit operator string(StreamId streamId) => streamId._value;
        public static implicit operator StreamId(string value) => new StreamId(value);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
    
    public abstract class ValueObject : IComparable, IComparable<ValueObject>
    {
        private int _cachedHashCode;
        
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetUnproxiedType(this) != GetUnproxiedType(obj)) return false;
            
            var valueObject = (ValueObject) obj;
            
            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }
        
        public override int GetHashCode()
        {
            if (_cachedHashCode != 0) return _cachedHashCode;
            
            var hashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
            
            _cachedHashCode = hashCode;
            
            return hashCode;
        }

        public int CompareTo(object obj)
        {
            var thisType = GetUnproxiedType(this);
            var otherType = GetUnproxiedType(obj);
            
            if(thisType != otherType)
                return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);
            
            var other = (ValueObject) obj;
            
            var components = GetEqualityComponents().ToArray();
            var otherComponents = other.GetEqualityComponents().ToArray();

            return components.Select((t, i) => CompareComponents(t, otherComponents[i])).FirstOrDefault(comparison => comparison != 0);
        }

        public int CompareTo(ValueObject other)
        {
            return CompareTo(other as object);
        }
        
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            
            return a.Equals(b);
        }
        
        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
        
        private int CompareComponents(object o1, object o2)
        {
            if (o1 == null && o2 == null) return 0;
            if (o1 == null) return -1;
            if (o2 == null) return 1;
            
            if(o1 is IComparable c1 && o2 is IComparable c2)
                return c1.CompareTo(c2);
            
            return o1.Equals(o2) ? 0 : -1;
        }
        internal static Type GetUnproxiedType(object obj)
        {
            const string efCoreProxy = "Castle.Proxies.";
            const string nHibernateProxy = "Proxy";
            
            var type = obj.GetType();
            
            var typeString = type.ToString();
            
            if (typeString.Contains(efCoreProxy) || typeString.Contains(nHibernateProxy))
            {
                type = type.BaseType;
            }
            
            return type;
        }
    }

}
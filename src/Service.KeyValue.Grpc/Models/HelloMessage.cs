using System.Runtime.Serialization;
using Service.KeyValue.Domain.Models;

namespace Service.KeyValue.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}
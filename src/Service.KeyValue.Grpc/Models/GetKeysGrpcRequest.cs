using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class GetKeysGrpcRequest
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }
	}
}
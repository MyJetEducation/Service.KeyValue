using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsPutGrpcRequest
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public KeyValueGrpcModel[] Items { get; set; }
	}
}
using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsGrpcResponse
	{
		[DataMember(Order = 1)]
		public KeyValueGrpcModel[] Items { get; set; }
	}
}
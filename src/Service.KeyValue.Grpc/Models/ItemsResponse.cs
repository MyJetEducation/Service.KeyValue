using System.Runtime.Serialization;
using Service.KeyValue.Domain.Models;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsResponse
	{
		[DataMember(Order = 1)]
		public KeyValueModel[] Items { get; set; }
	}
}
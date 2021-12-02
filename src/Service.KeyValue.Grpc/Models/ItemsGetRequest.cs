using System;
using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsGetRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public string[] Keys { get; set; }
	}
}
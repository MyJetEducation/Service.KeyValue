using System;
using System.Runtime.Serialization;
using Service.KeyValue.Domain.Models;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsPutRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public KeyValueModel[] Items { get; set; }
	}
}
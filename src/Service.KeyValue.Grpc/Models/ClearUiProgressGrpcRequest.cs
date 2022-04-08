using System;
using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class ClearUiProgressGrpcRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }
	}
}
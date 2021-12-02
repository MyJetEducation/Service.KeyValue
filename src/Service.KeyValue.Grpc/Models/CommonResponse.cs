using System.Runtime.Serialization;

namespace Service.KeyValue.Grpc.Models
{
	[DataContract]
	public class CommonResponse
	{
		[DataMember(Order = 1)]
		public bool IsSuccess { get; set; }

		public static CommonResponse Success => new() {IsSuccess = true};
		public static CommonResponse Fail => new();
	}
}
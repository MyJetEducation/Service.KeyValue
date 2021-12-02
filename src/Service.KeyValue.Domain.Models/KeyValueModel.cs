using System.Runtime.Serialization;

namespace Service.KeyValue.Domain.Models
{
	[DataContract]
	public class KeyValueModel
	{
		[DataMember(Order = 1)]
		public string Key { get; set; }

		[DataMember(Order = 2)]
		public string Value { get; set; }
	}
}
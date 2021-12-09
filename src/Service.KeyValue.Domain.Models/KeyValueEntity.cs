using System;

namespace Service.KeyValue.Domain.Models
{
	public class KeyValueEntity
	{
		public string Id { get; set; }
		public Guid? UserId { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
namespace Service.KeyValue.Postgres.Models
{
	public class KeyValueEntity
	{
		public string Id { get; set; }
		public Guid? UserId { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }

		public KeyValueEntity(Guid? userId, string key, string value)
		{
			Id = $"{userId}-{key}";
			UserId = userId;
			Key = key;
			Value = value;
		}
	}
}
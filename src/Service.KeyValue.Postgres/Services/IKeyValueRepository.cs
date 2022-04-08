using Service.KeyValue.Postgres.Models;

namespace Service.KeyValue.Postgres.Services
{
	public interface IKeyValueRepository
	{
		ValueTask<KeyValueEntity[]> GetEntities(string userId, string[] keys);

		ValueTask<bool> SaveEntities(string userId, KeyValueEntity[] entities);
		
		ValueTask<bool> DeleteEntities(string userId, string[] keys);

		ValueTask<string[]> GetKeys(string userId);
	}
}
using Service.KeyValue.Postgres.Models;

namespace Service.KeyValue.Postgres.Services
{
	public interface IKeyValueRepository
	{
		ValueTask<KeyValueEntity[]> GetEntities(Guid? userId, string[] keys);

		ValueTask<bool> SaveEntities(Guid? userId, KeyValueEntity[] entities);
		
		ValueTask<bool> DeleteEntities(Guid? userId, string[] keys);

		ValueTask<string[]> GetKeys(Guid? userId);
	}
}
using System;
using System.Threading.Tasks;

namespace Service.KeyValue.Domain.Models
{
	public interface IKeyValueRepository
	{
		ValueTask<KeyValueEntity[]> GetEntities(Guid? userId, string[] keys);

		ValueTask<bool> SaveEntities(Guid? userId, KeyValueEntity[] entities);
		
		ValueTask<bool> DeleteEntities(Guid? userId, string[] keys);
	}
}
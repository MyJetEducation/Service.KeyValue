using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Postgres;

namespace Service.KeyValue.Domain
{
	public class KeyValueRepository : IKeyValueRepository
	{
		private readonly ILogger<KeyValueRepository> _logger;
		private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

		public KeyValueRepository(ILogger<KeyValueRepository> logger, DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
		{
			_logger = logger;
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		public async ValueTask<KeyValueEntity[]> GetEntities(Guid? userId, string[] keys)
		{
			try
			{
				return await GetContext()
					.KeyValues
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return await ValueTask.FromResult<KeyValueEntity[]>(null);
		}

		public async ValueTask<bool> SaveEntities(Guid? userId, KeyValueEntity[] entities)
		{
			string[] keys = entities.Select(model => model.Key).ToArray();
			List<KeyValueEntity> newEntitiesList = entities.ToList();

			DatabaseContext context = GetContext();
			DbSet<KeyValueEntity> dbSet = context.KeyValues;

			try
			{
				KeyValueEntity[] existingEntities = await dbSet
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();

				if (existingEntities.Any())
				{
					UpdateExistingEntities(existingEntities, newEntitiesList);
					dbSet.UpdateRange(existingEntities);
				}

				if (newEntitiesList.Any())
					await dbSet.AddRangeAsync(newEntitiesList);

				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return false;
		}

		private static void UpdateExistingEntities(IEnumerable<KeyValueEntity> existingEntities, List<KeyValueEntity> newEntitiesList)
		{
			foreach (KeyValueEntity entity in existingEntities)
			{
				KeyValueEntity requestItem = newEntitiesList.First(model => model.Key == entity.Key);
				entity.Value = requestItem.Value;
				newEntitiesList.Remove(requestItem);
			}
		}

		public async ValueTask<bool> DeleteEntities(Guid? userId, string[] keys)
		{
			DatabaseContext context = GetContext();
			DbSet<KeyValueEntity> dbSet = context.KeyValues;

			try
			{
				KeyValueEntity[] items = await dbSet
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();

				if (!items.Any())
					return false;

				dbSet.RemoveRange(items);

				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return false;
		}

		private DatabaseContext GetContext() => DatabaseContext.Create(_dbContextOptionsBuilder);
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.KeyValue.Postgres;
using Service.KeyValue.Postgres.Models;

namespace Service.KeyValue.Services
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

		public async ValueTask<ItemsResponse> Get(ItemsGetRequest request)
		{
			try
			{
				KeyValueModel[] items = await GetContext()
					.KeyValues
					.Where(entity => entity.UserId == request.UserId)
					.Where(entity => request.Keys.Contains(entity.Key))
					.Select(entity => new KeyValueModel
					{
						Key = entity.Key,
						Value = entity.Value
					})
					.ToArrayAsync();

				return new ItemsResponse {Items = items};
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				return new ItemsResponse();
			}
		}

		public async ValueTask<CommonResponse> Put(ItemsPutRequest request)
		{
			string[] keys = request.Items.Select(model => model.Key).ToArray();
			Guid? userId = request.UserId;
			List<KeyValueModel> requestItemList = request.Items.ToList();

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
					UpdateExistingEntities(existingEntities, requestItemList);
					dbSet.UpdateRange(existingEntities);
				}

				if (requestItemList.Any())
				{
					KeyValueEntity[] entities = requestItemList
						.Select(model => new KeyValueEntity(userId, model.Key, model.Value))
						.ToArray();

					await dbSet.AddRangeAsync(entities);
				}

				await context.SaveChangesAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				return CommonResponse.Fail;
			}

			return CommonResponse.Success;
		}

		private static void UpdateExistingEntities(IEnumerable<KeyValueEntity> existingEntities, List<KeyValueModel> requestItemList)
		{
			foreach (KeyValueEntity entity in existingEntities)
			{
				KeyValueModel requestItem = requestItemList.First(model => model.Key == entity.Key);
				entity.Value = requestItem.Value;
				requestItemList.Remove(requestItem);
			}
		}

		public async ValueTask<CommonResponse> Delete(ItemsDeleteRequest request)
		{
			DatabaseContext context = GetContext();
			DbSet<KeyValueEntity> dbSet = context.KeyValues;

			try
			{
				KeyValueEntity[] items = await dbSet
					.Where(entity => entity.UserId == request.UserId)
					.Where(entity => request.Keys.Contains(entity.Key))
					.ToArrayAsync();

				if (!items.Any())
					return CommonResponse.Fail;

				dbSet.RemoveRange(items);

				await context.SaveChangesAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				return CommonResponse.Fail;
			}

			return CommonResponse.Success;
		}

		private DatabaseContext GetContext() => DatabaseContext.Create(_dbContextOptionsBuilder);
	}
}
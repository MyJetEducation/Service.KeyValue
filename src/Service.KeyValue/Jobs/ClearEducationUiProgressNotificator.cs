using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Extensions;
using Service.KeyValue.Postgres.Models;
using Service.KeyValue.Postgres.Services;
using Service.ServiceBus.Models;

namespace Service.KeyValue.Jobs
{
	public class ClearEducationUiProgressNotificator
	{
		private readonly ILogger<ClearEducationUiProgressNotificator> _logger;
		private readonly IKeyValueRepository _keyValueRepository;

		public ClearEducationUiProgressNotificator(ILogger<ClearEducationUiProgressNotificator> logger,
			ISubscriber<IReadOnlyList<ClearEducationUiProgressServiceBusModel>> subscriber, IKeyValueRepository keyValueRepository)
		{
			_logger = logger;
			_keyValueRepository = keyValueRepository;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<ClearEducationUiProgressServiceBusModel> events)
		{
			foreach (ClearEducationUiProgressServiceBusModel message in events)
			{
				string userId = message.UserId;

				_logger.LogInformation("Clear education UI progress for user: {userId}", userId);

				bool cleared = await ClearUiProgress(userId);
				if (!cleared)
					_logger.LogError("Can't clear UI progress for user: {userId}, request: {@request}", userId, message);
			}
		}

		public async ValueTask<bool> ClearUiProgress(string userId)
		{
			string[] keys = (await _keyValueRepository.GetKeys(userId)) ?? Array.Empty<string>();

			string[] menuKeys = keys.Where(s => s.StartsWith("progressMenu")).ToArray();
			if (menuKeys.IsNullOrEmpty())
				return true;

			KeyValueEntity[] items = await _keyValueRepository.GetEntities(userId, menuKeys);
			if (items.IsNullOrEmpty())
				return true;

			foreach (KeyValueEntity item in items)
				item.Value = item.Value.Replace("\"valid\":true", "\"valid\":false");

			return await _keyValueRepository.SaveEntities(userId, items);
		}
	}
}
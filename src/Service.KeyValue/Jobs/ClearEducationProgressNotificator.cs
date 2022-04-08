using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.ServiceBus.Models;

namespace Service.KeyValue.Jobs
{
	public class ClearEducationProgressNotificator
	{
		private readonly IKeyValueService _keyValueService;
		private readonly ILogger<ClearEducationProgressNotificator> _logger;

		public ClearEducationProgressNotificator(ILogger<ClearEducationProgressNotificator> logger,
			ISubscriber<IReadOnlyList<ClearEducationProgressServiceBusModel>> subscriber, IKeyValueService keyValueService)
		{
			_logger = logger;
			_keyValueService = keyValueService;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<ClearEducationProgressServiceBusModel> events)
		{
			foreach (ClearEducationProgressServiceBusModel message in events)
			{
				Guid? userId = message.UserId;

				_logger.LogInformation("Clear full education progress for user: {userId}", userId);

				await _keyValueService.ClearUiProgress(new ClearUiProgressGrpcRequest {UserId = userId});
			}
		}
	}
}
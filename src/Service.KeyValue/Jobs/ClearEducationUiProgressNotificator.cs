using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Core.Client.Models;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.ServiceBus.Models;

namespace Service.KeyValue.Jobs
{
	public class ClearEducationUiProgressNotificator
	{
		private readonly IKeyValueService _keyValueService;
		private readonly ILogger<ClearEducationUiProgressNotificator> _logger;

		public ClearEducationUiProgressNotificator(ILogger<ClearEducationUiProgressNotificator> logger,
			ISubscriber<IReadOnlyList<ClearEducationUiProgressServiceBusModel>> subscriber, IKeyValueService keyValueService)
		{
			_logger = logger;
			_keyValueService = keyValueService;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<ClearEducationUiProgressServiceBusModel> events)
		{
			foreach (ClearEducationUiProgressServiceBusModel message in events)
			{
				string userId = message.UserId;

				_logger.LogInformation("Clear education UI progress for user: {userId}", userId);

				CommonGrpcResponse response = await _keyValueService.ClearUiProgress(new ClearUiProgressGrpcRequest {UserId = userId});
				if (response.IsSuccess != true)
					_logger.LogError("Can't clear UI progress for user: {userId}, request: {@request}", userId, message);
			}
		}
	}
}
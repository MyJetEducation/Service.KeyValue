﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Service.Core.Grpc.Models;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Grpc;
using Service.KeyValue.Grpc.Models;
using Service.KeyValue.Mappers;

namespace Service.KeyValue.Services
{
	public class KeyValueService : IKeyValueService
	{
		private readonly IKeyValueRepository _keyValueRepository;

		public KeyValueService(IKeyValueRepository keyValueRepository) => _keyValueRepository = keyValueRepository;

		public async ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest grpcRequest)
		{
			KeyValueEntity[] entities = await _keyValueRepository.GetEntities(grpcRequest.UserId, grpcRequest.Keys);

			return new ItemsGrpcResponse
			{
				Items = entities?.Select(entity => entity.ToGrpcModel()).ToArray()
			};
		}

		public async ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest grpcRequest)
		{
			Guid? userId = grpcRequest.UserId;

			bool saved = await _keyValueRepository.SaveEntities(userId, grpcRequest.Items.Select(model => model.ToEntity(userId)).ToArray());

			return CommonGrpcResponse.Result(saved);
		}

		public async ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest grpcRequest)
		{
			bool deleted = await _keyValueRepository.DeleteEntities(grpcRequest.UserId, grpcRequest.Keys);

			return CommonGrpcResponse.Result(deleted);
		}
	}
}
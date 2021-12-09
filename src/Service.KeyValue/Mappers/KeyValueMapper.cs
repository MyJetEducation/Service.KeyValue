using System;
using Service.KeyValue.Domain.Models;
using Service.KeyValue.Grpc.Models;

namespace Service.KeyValue.Mappers
{
	public static class KeyValueMapper
	{
		public static KeyValueGrpcModel ToGrpcModel(this KeyValueEntity entity) => new KeyValueGrpcModel
		{
			Key = entity.Key,
			Value = entity.Value
		};

		public static KeyValueEntity ToEntity(this KeyValueGrpcModel grpcModel, Guid? userId) => new KeyValueEntity
		{
			Id = $"{userId}-{grpcModel.Key}",
			UserId = userId,
			Key = grpcModel.Key,
			Value = grpcModel.Value
		};
	}
}
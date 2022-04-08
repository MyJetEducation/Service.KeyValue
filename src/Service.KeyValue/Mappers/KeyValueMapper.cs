using Service.KeyValue.Grpc.Models;
using Service.KeyValue.Postgres.Models;

namespace Service.KeyValue.Mappers
{
	public static class KeyValueMapper
	{
		public static KeyValueGrpcModel ToGrpcModel(this KeyValueEntity entity) => new KeyValueGrpcModel
		{
			Key = entity.Key,
			Value = entity.Value
		};

		public static KeyValueEntity ToEntity(this KeyValueGrpcModel grpcModel, string userId) => new KeyValueEntity
		{
			Id = $"{userId}-{grpcModel.Key}",
			UserId = userId,
			Key = grpcModel.Key,
			Value = grpcModel.Value
		};
	}
}
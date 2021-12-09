using System.ServiceModel;
using System.Threading.Tasks;
using Service.KeyValue.Grpc.Models;

namespace Service.KeyValue.Grpc
{
	[ServiceContract]
	public interface IKeyValueService
	{
		[OperationContract]
		ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest grpcRequest);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest grpcRequest);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest grpcRequest);
	}
}
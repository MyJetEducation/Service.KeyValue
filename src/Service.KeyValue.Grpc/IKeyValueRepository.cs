using System.ServiceModel;
using System.Threading.Tasks;
using Service.KeyValue.Grpc.Models;

namespace Service.KeyValue.Grpc
{
	[ServiceContract]
	public interface IKeyValueRepository
	{
		[OperationContract]
		ValueTask<ItemsResponse> Get(ItemsGetRequest request);

		[OperationContract]
		ValueTask<CommonResponse> Put(ItemsPutRequest request);

		[OperationContract]
		ValueTask<CommonResponse> Delete(ItemsDeleteRequest request);
	}
}
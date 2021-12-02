using System.ServiceModel;
using System.Threading.Tasks;
using Service.KeyValue.Grpc.Models;

namespace Service.KeyValue.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}
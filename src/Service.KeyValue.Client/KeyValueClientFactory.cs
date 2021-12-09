using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.KeyValue.Grpc;

namespace Service.KeyValue.Client
{
	[UsedImplicitly]
	public class KeyValueClientFactory : MyGrpcClientFactory
	{
		public KeyValueClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
		{
		}

		public IKeyValueService GetKeyValueRepository() => CreateGrpcService<IKeyValueService>();
	}
}
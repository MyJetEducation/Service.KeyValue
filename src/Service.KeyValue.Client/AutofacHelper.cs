using Autofac;
using Service.KeyValue.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.KeyValue.Client
{
	public static class AutofacHelper
	{
		public static void RegisterKeyValueClient(this ContainerBuilder builder, string grpcServiceUrl)
		{
			var factory = new KeyValueClientFactory(grpcServiceUrl);

			builder.RegisterInstance(factory.GetKeyValueRepository()).As<IKeyValueRepository>().SingleInstance();
		}
	}
}
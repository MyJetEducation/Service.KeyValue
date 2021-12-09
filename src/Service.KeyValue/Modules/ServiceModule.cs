using Autofac;
using Service.KeyValue.Domain;

namespace Service.KeyValue.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder) => builder.RegisterType<KeyValueRepository>().AsImplementedInterfaces().SingleInstance();
	}
}
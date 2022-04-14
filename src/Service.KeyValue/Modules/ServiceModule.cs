using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.KeyValue.Jobs;
using Service.KeyValue.Postgres.Services;
using Service.ServiceBus.Models;

namespace Service.KeyValue.Modules
{
	public class ServiceModule : Module
	{
		private const string QueueName = "MyJetEducation-KeyValue";

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<KeyValueRepository>().AsImplementedInterfaces().SingleInstance();

			MyServiceBusTcpClient serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.ServiceBusReader), Program.LogFactory);
			builder.RegisterMyServiceBusSubscriberBatch<ClearEducationUiProgressServiceBusModel>(serviceBusClient, ClearEducationUiProgressServiceBusModel.TopicName, QueueName, TopicQueueType.Permanent);

			builder.RegisterType<ClearEducationUiProgressNotificator>().AutoActivate().SingleInstance();
		}
	}
}
using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.KeyValue.Settings
{
	public class SettingsModel
	{
		[YamlProperty("KeyValueService.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("KeyValueService.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("KeyValueService.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("KeyValueService.PostgresConnectionString")]
		public string PostgresConnectionString { get; set; }
	}
}
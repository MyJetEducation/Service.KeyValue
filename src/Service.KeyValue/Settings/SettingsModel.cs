using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.KeyValue.Settings
{
    public class SettingsModel
    {
        [YamlProperty("KeyValue.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("KeyValue.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("KeyValue.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("KeyValue.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }
    }
}

using MyJetWallet.Sdk.Postgres;

namespace Service.KeyValue.Postgres.DesignTime
{
	public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
	{
		public ContextFactory() : base(options => new DatabaseContext(options))
		{
		}
	}
}
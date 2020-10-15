using Ninject.Modules;
using DataAccess.Repositories;

namespace BusinessLogic.Infrastructure
{
	class ServiceModule: NinjectModule
	{
		private string connectionString;
		public ServiceModule(string connection)
		{
			connectionString = connection;
		}
		public override void Load()
		{
			Bind<EFUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
		}
	}
}

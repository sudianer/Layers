using Ninject.Modules;
using DataAccess.Interfaces;
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
			Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
		}
	}
}

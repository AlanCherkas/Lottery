using Game.Services.Abstract;
using Game.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services
{
	internal static class ServiceBuilder
	{
		public static ServiceProvider? ServiceProvider
		{
			get
			{
				if (_serviceProvider == null)
				{
					Build();
				}

				return _serviceProvider;
			}
		}

		private static ServiceProvider? _serviceProvider;

		private static void Build()
		{
			var serviceCollection = new ServiceCollection();

			var configurationBuilder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", false, true);
			serviceCollection.AddSingleton<IConfiguration>(configurationBuilder.Build());

			serviceCollection.AddSingleton<ILotteryService, LotteryService>();
			serviceCollection.AddSingleton<IGameSetupService, GameSetupService>();
			serviceCollection.AddSingleton<IMessageDisplayService, ConsoleMessageDisplayService>();
			serviceCollection.AddSingleton<ITicketService, TicketService>();
			serviceCollection.AddSingleton<IPrizeCalculationService, PrizeCalculationService>();

			_serviceProvider = serviceCollection.BuildServiceProvider();
		}
	}
}


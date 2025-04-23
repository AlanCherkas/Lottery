using Game.Services;
using Game.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Game
{
	internal class Program
	{
		private static ILotteryService LotteryService => ServiceBuilder.ServiceProvider?.GetService<ILotteryService>() ?? throw new InvalidOperationException("LotteryService is not initialized.");

		static void Main()
		{
			GlobalExceptionHandler.Register();

			LotteryService.StartLottery();
			
			Console.WriteLine();
			Console.WriteLine("Press enter to exit...");
			Console.ReadLine();
		}
	}
}

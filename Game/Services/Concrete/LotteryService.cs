using Game.DTOs;
using Game.Players.Abstract;
using Game.Players.Constants;
using Game.Services.Abstract;
using Game.Tickets.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services.Concrete
{
	public class LotteryService : ILotteryService
	{
		private IGameSetupService GameSetupService => ServiceBuilder.ServiceProvider?.GetService<IGameSetupService>() ?? throw new InvalidOperationException("GameSetupService is not initialized.");
		private IMessageDisplayService MessageDisplayService => ServiceBuilder.ServiceProvider?.GetService<IMessageDisplayService>() ?? throw new InvalidOperationException("MessageDisplayService is not initialized.");
		private IConfiguration Configuration => ServiceBuilder.ServiceProvider?.GetService<IConfiguration>() ?? throw new InvalidOperationException("Configuration is not initialized.");
		private ITicketService TicketService => ServiceBuilder.ServiceProvider?.GetService<ITicketService>() ?? throw new InvalidOperationException("TicketService is not initialized.");
		private IPrizeCalculationService PrizeCalculationService => ServiceBuilder.ServiceProvider?.GetService<IPrizeCalculationService>() ?? throw new InvalidOperationException("PrizeCalculationService is not initialized.");

		public void StartLottery()
		{
			List<BasePlayer> players = GameSetupService.GeneratePlayers();
			MessageDisplayService.DisplayWelcomeMessage(players.FirstOrDefault(p => p.Number == PlayersConstants.DefaultUserPlayerNumber), float.Parse(Configuration["TicketPrice"] ?? TicketsConstants.DefaultTicketPrice));
			
			TicketService.PurchaseTickets(players);

			List<WinnerDto> winners = PrizeCalculationService.CalculatePrize(players);
			float houseProfit = PrizeCalculationService.CalculateHouseProfit(players, winners);

			MessageDisplayService.DisplayWinners(winners);
			MessageDisplayService.DisplayHouseProfit(houseProfit);
		}
	}
}

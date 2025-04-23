using Game.Players.Abstract;
using Game.Players.Constants;
using Game.Services.Abstract;
using Game.Tickets.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Game.Tickets.Abstract;

namespace Game.Services.Concrete
{
	public class TicketService : ITicketService
	{
		private IConfiguration Configuration => ServiceBuilder.ServiceProvider?.GetService<IConfiguration>() ?? throw new InvalidOperationException("Configuration is not initialized.");
		private IMessageDisplayService MessageDisplayService => ServiceBuilder.ServiceProvider?.GetService<IMessageDisplayService>() ?? throw new InvalidOperationException("MessageDisplayService is not initialized.");

		public void PurchaseTickets(List<BasePlayer> players)
		{
			if (players == null || players.Count == 0)
			{
				throw new ArgumentException("Players list cannot be null or empty.", nameof(players));
			}
			int allowedTicketsAmountToPurchaseMinimum = int.Parse(
				Configuration["AllowedTicketsAmountToPurchaseMinimum"] ??
				PlayersConstants.DefaultAllowedTicketsAmountToPurchaseMinimum);

			int allowedTicketsAmountToPurchaseMaximum = int.Parse(
				Configuration["AllowedTicketsAmountToPurchaseMaximum"] ??
				PlayersConstants.DefaultAllowedTicketsAmountToPurchaseMaximum);

			int ticketsAmount;
			while (!int.TryParse(Console.ReadLine(), out ticketsAmount) ||
				   ticketsAmount < allowedTicketsAmountToPurchaseMinimum ||
				   ticketsAmount > allowedTicketsAmountToPurchaseMaximum)
			{
				MessageDisplayService.DisplayPurchaseTicketsMessage(allowedTicketsAmountToPurchaseMinimum, allowedTicketsAmountToPurchaseMaximum);
			}

			GenerateTicketsForPlayer(players.FirstOrDefault(p => p.Number == PlayersConstants.DefaultUserPlayerNumber), ticketsAmount);

			Random random = new Random();
			List<BasePlayer> cpuPlayers = players.Where(p => p.Number != PlayersConstants.DefaultUserPlayerNumber).ToList();
			cpuPlayers.ForEach(
				p =>
				{
					int randomTicketsAmount = random.Next(allowedTicketsAmountToPurchaseMinimum,
						allowedTicketsAmountToPurchaseMaximum + PlayersConstants.CpuPlayerTicketGenerationOffset);
					GenerateTicketsForPlayer(p, randomTicketsAmount);
				});

			MessageDisplayService.DisplayCpuPlayerPurchaseTicketsMessage(cpuPlayers);
		}

		private void GenerateTicketsForPlayer(BasePlayer? player, int ticketsAmount)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player), "Player cannot be null.");
			}

			float ticketPrice = float.Parse(Configuration["TicketPrice"] ?? TicketsConstants.DefaultTicketPrice);

			int maximumTickets = Math.Min((int)(player.Balance / ticketPrice), ticketsAmount);
			if (maximumTickets <= 0)
			{
				throw new Exception("You don't have enough balance to purchase tickets.");
			}

			List<BaseTicket> tickets = new List<BaseTicket>();
			for (int i = 0; i < maximumTickets; i++)
			{
				tickets.Add(new BaseTicket(Guid.NewGuid(), ticketPrice));
			}

			player.SetTickets(tickets);

			player.Balance -= maximumTickets * ticketPrice;
		}
	}
}

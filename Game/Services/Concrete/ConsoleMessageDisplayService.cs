using Game.Players.Abstract;
using Game.Services.Abstract;
using Game.DTOs;
using Game.Enums;

namespace Game.Services.Concrete
{
	public class ConsoleMessageDisplayService : IMessageDisplayService
	{
		public void DisplayWelcomeMessage(BasePlayer? player, float ticketPrice)
		{
			if (player == null)
			{
				throw new ArgumentNullException(nameof(player), "Player cannot be null.");
			}

			Console.WriteLine($"Welcome to the Bede Lottery, {player.Name}!");
			Console.WriteLine();
			Console.WriteLine($"* Your digital balance: ${player.Balance:#,0.00}");
			Console.WriteLine($"* Ticket Price: ${ticketPrice:#,0.00} each");
			Console.WriteLine();
			Console.WriteLine($"How many tickets do you want to buy, {player.Name}?");
		}

		public void DisplayPurchaseTicketsMessage(int allowedTicketsAmountToPurchaseMinimum,
			int allowedTicketsAmountToPurchaseMaximum)
		{
			Console.WriteLine($"Please enter a number between {allowedTicketsAmountToPurchaseMinimum} and {allowedTicketsAmountToPurchaseMaximum}.");
		}

		public void DisplayCpuPlayerPurchaseTicketsMessage(List<BasePlayer>? players)
		{
			if (players == null)
			{
				throw new ArgumentNullException(nameof(players), "Players list cannot be null.");
			}

			Console.WriteLine();
			Console.WriteLine($"{players.Count} other CPU players also have purchased tickets.");
		}

		public void DisplayWinners(List<WinnerDto> winners)
		{
			Console.WriteLine();
			Console.WriteLine("Ticket Draw Results:");

			var grandPrizeWinner = winners.FirstOrDefault(w => w.PrizeType == PrizeType.GrandPrize && w.Player != null);
			var secondTierWinners = winners.Where(w => w.PrizeType == PrizeType.SecondTier && w.Player != null).ToList();
			var thirdTierWinners = winners.Where(w => w.PrizeType == PrizeType.ThirdTier && w.Player != null).ToList();

			if (grandPrizeWinner?.Player != null)
			{
				Console.WriteLine();
				Console.WriteLine($"* Grand Prize: Player {grandPrizeWinner.Player.Number} wins ${grandPrizeWinner.Prize:#,0.00}!");
			}

			if (secondTierWinners.Any())
			{
				var secondTierGroups = secondTierWinners
					.GroupBy(w => w.Prize)
					.Select(g => $"Players: {string.Join(", ", g.Select(w => w.Player!.Number))} win ${g.Key} each!");
				foreach (var group in secondTierGroups)
				{
					Console.WriteLine($"* Second Tier: {group}");
				}
			}

			if (thirdTierWinners.Any())
			{
				var thirdTierGroups = thirdTierWinners
					.GroupBy(w => w.Prize)
					.Select(g => $"Players: {string.Join(", ", g.Select(w => w.Player?.Number))} win ${g.Key} each!");
				foreach (var group in thirdTierGroups)
				{
					Console.WriteLine($"* Third Tier: {group}");
				}
			}

			Console.WriteLine();
			Console.WriteLine("\nCongratulations to the winners");
		}


		public void DisplayHouseProfit(float profit)
		{
			Console.WriteLine();
			Console.WriteLine($"House Profit: ${profit:#,0.00}");
		}
	}
}

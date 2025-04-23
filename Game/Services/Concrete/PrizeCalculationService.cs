using Game.DTOs;
using Game.Enums;
using Game.Players.Abstract;
using Game.Services.Abstract;
using Game.Tickets.Abstract;
using Game.Tickets.Constants;

namespace Game.Services.Concrete
{
	public class PrizeCalculationService : IPrizeCalculationService
	{
		public List<WinnerDto> CalculatePrize(List<BasePlayer> players)
		{
			double revenue = players.Sum(player =>
			{
				if (player.Tickets == null)
				{
					throw new ArgumentNullException(nameof(player.Tickets), "Tickets cannot be null");
				}
				return player.Tickets.Sum(t => t.Price);
			});

			List<WinnerDto> winners = new List<WinnerDto>();

			List<BasePlayer> participatingPlayers = players.Where(p => p.Tickets?.Count > 0).ToList();
			List<BaseTicket> eligibleTickets = participatingPlayers.SelectMany(p =>
			{
				if (p.Tickets == null)
				{
					throw new ArgumentNullException(nameof(p.Tickets), "Tickets cannot be null");
				}

				return p.Tickets;
			}).ToList();

			CalculateGrandPrizeWinners(eligibleTickets, participatingPlayers, winners, revenue);
			CalculateSecondTierWinners(eligibleTickets, participatingPlayers, winners, revenue);
			CalculateThirdTierWinners(eligibleTickets, participatingPlayers, winners, revenue);

			return winners;
		}


		private void CalculateGrandPrizeWinners(List<BaseTicket> eligibleTickets, List<BasePlayer> participatingPlayers, List<WinnerDto> winners, double revenue)
		{
			int grandPrizeAmount = (int)(revenue * TicketsConstants.DefaultGrandPrizeCoefficient);
			var random = new Random();
			var winnerTicket = eligibleTickets[random.Next(eligibleTickets.Count)];
			var winner = participatingPlayers.First(p => (bool)p.Tickets?.Contains(winnerTicket));

			var existentWinner = winners.FirstOrDefault(w => w.Player?.Number == winner.Number && w.PrizeType == PrizeType.GrandPrize);
			if (existentWinner != null)
			{
				existentWinner.Prize += grandPrizeAmount;
			}
			else
			{
				winners.Add(new WinnerDto { Player = winner, Prize = grandPrizeAmount, PrizeType = PrizeType.GrandPrize });
			}
			
			eligibleTickets.Remove(winnerTicket);
		}

		private void CalculateSecondTierWinners(List<BaseTicket> eligibleTickets, List<BasePlayer> participatingPlayers,
			List<WinnerDto> winners, double revenue)
		{
			int secondTierPrizeAmount = (int)(revenue * TicketsConstants.DefaultSecondTierCoefficient);
			int secondTierWinnersAmount = (int)Math.Round(eligibleTickets.Count * TicketsConstants.DefaultSecondTierTickets);
			var random = new Random();

			if (secondTierWinnersAmount > 0 && eligibleTickets.Count > 0)
			{
				int individualPrize = Math.Max(secondTierPrizeAmount / secondTierWinnersAmount, 1);

				for (int i = 0; i < secondTierWinnersAmount && eligibleTickets.Count > 0; i++)
				{
					var winnerTicket = eligibleTickets[random.Next(eligibleTickets.Count)];
					var winner = participatingPlayers.First(p => (bool)p.Tickets?.Contains(winnerTicket));

					var existentWinner = winners.FirstOrDefault(w => w.Player?.Number == winner.Number && w.PrizeType == PrizeType.SecondTier);
					if (existentWinner != null)
					{
						existentWinner.Prize += individualPrize;
					}
					else
					{
						winners.Add(new WinnerDto { Player = winner, Prize = individualPrize, PrizeType = PrizeType.SecondTier });
					}

					eligibleTickets.Remove(winnerTicket);
				}
			}
		}

		private void CalculateThirdTierWinners(List<BaseTicket> eligibleTickets, List<BasePlayer> participatingPlayers,
			List<WinnerDto> winners, double revenue)
		{
			int thirdTierPrizeAmount = (int)(revenue * TicketsConstants.DefaultThirdTierCoefficient);
			int thirdTierWinnersAmount = (int)Math.Round(eligibleTickets.Count * TicketsConstants.DefaultThirdTierTickets);
			var random = new Random();

			if (thirdTierWinnersAmount > 0 && eligibleTickets.Count > 0)
			{
				int individualPrize = Math.Max(thirdTierPrizeAmount / thirdTierWinnersAmount, 1);

				for (int i = 0; i < thirdTierWinnersAmount && eligibleTickets.Count > 0; i++)
				{
					var winnerTicket = eligibleTickets[random.Next(eligibleTickets.Count)];
					var winner = participatingPlayers.First(p => (bool)p.Tickets?.Contains(winnerTicket));

					var existentWinner = winners.FirstOrDefault(w => w.Player?.Number == winner.Number && w.PrizeType == PrizeType.ThirdTier);
					if (existentWinner != null)
					{
						existentWinner.Prize += individualPrize;
					}
					else
					{
						winners.Add(new WinnerDto { Player = winner, Prize = individualPrize, PrizeType = PrizeType.ThirdTier });
					}

					eligibleTickets.Remove(winnerTicket);
				}
			}
		}

		public double CalculateHouseProfit(List<BasePlayer> players, List<WinnerDto> winners)
		{
			double revenue = players.Sum(player =>
			{
				if (player.Tickets == null)
				{
					throw new ArgumentNullException(nameof(player.Tickets), "Tickets cannot be null");
				}
				return player.Tickets.Sum(t => t.Price);
			});

			double totalPrize = winners.Sum(w => w.Prize);

			return revenue - totalPrize;
		}
	}
}

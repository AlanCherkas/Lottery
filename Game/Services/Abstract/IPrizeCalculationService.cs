using Game.DTOs;
using Game.Players.Abstract;

namespace Game.Services.Abstract
{
	public interface IPrizeCalculationService
	{
		public List<WinnerDto> CalculatePrize(List<BasePlayer> players);
		public double CalculateHouseProfit(List<BasePlayer> players, List<WinnerDto> winners);
	}
}

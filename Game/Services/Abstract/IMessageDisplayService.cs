using Game.DTOs;
using Game.Players.Abstract;

namespace Game.Services.Abstract
{
	public interface IMessageDisplayService
	{
		public void DisplayWelcomeMessage(BasePlayer? player, float ticketPrice);
		public void DisplayPurchaseTicketsMessage(int allowedTicketsAmountToPurchaseMinimum, int allowedTicketsAmountToPurchaseMaximum);
		public void DisplayCpuPlayerPurchaseTicketsMessage(List<BasePlayer>? players);
		public void DisplayWinners(List<WinnerDto> winners);
		public void DisplayHouseProfit(float profit);
	}
}

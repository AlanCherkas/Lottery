using Game.Players.Abstract;

namespace Game.Services.Abstract
{
	public interface ITicketService
	{
		public void PurchaseTickets(List<BasePlayer> players);
	}
}

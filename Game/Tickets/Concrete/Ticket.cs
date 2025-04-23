using Game.Tickets.Abstract;

namespace Game.Tickets.Concrete
{
	internal class Ticket : BaseTicket
	{
		public Ticket(Guid id, float price) : base(id, price)
		{
		}
	}
}

namespace Game.Tickets.Abstract
{
	public class BaseTicket
	{
		public Guid Id { get; set; }
		public double Price { get; set; }

		public BaseTicket(Guid id, double price)
		{
			Id = id;
			Price = price;
		}
	}
}

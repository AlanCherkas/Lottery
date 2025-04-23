namespace Game.Tickets.Abstract
{
	public class BaseTicket
	{
		public Guid Id { get; set; }
		public float Price { get; set; }

		public BaseTicket(Guid id, float price)
		{
			Id = id;
			Price = price;
		}
	}
}

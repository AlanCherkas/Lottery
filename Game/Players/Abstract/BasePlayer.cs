using Game.Tickets.Abstract;

namespace Game.Players.Abstract
{
	public class BasePlayer
	{
		public Guid Id { get; }
		public string Name { get; }
		public int Number { get; }
		public double Balance { get; set; }
		public List<BaseTicket>? Tickets { get; set; }

		public BasePlayer(Guid id, string name, double balance, int number)
		{
			Id = id;
			Name = name;
			Balance = balance;
			Number = number;
		}

		public void SetTickets(List<BaseTicket> tickets)
		{
			Tickets = tickets;
		}
	}
}

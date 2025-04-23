using Game.Services.Concrete;
using Game.Players.Abstract;
using Xunit;

namespace GameTests.ServiceTests
{
	public class TicketServiceTests
	{
		[Fact]
		public void ShouldPurchaseTicketsPlayersThrowException()
		{
			// Arrange
			var ticketService = new TicketService();

			// Act
			Action act = () => ticketService.PurchaseTickets(new List<BasePlayer>());

			// Assert
			Assert.Throws<ArgumentException>(act);
		}
	}
}

using Game.DTOs;
using Game.Services.Concrete;
using Xunit;

namespace GameTests.ServiceTests
{
	public class ConsoleMessageDisplayServiceTests
	{
		[Fact]
		public void ShouldCallDisplayWelcomeMessageWithoutPlayerThrowException()
		{
			// Arrange
			var messageDisplayService = new ConsoleMessageDisplayService();

			// Act
			Action act = () => messageDisplayService.DisplayWelcomeMessage(null, 100);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void ShouldCallDisplayPurchaseTicketsMessageNotThrowException()
		{
			// Arrange
			var messageDisplayService = new ConsoleMessageDisplayService();

			// Act
			var exception = Record.Exception(() => messageDisplayService.DisplayPurchaseTicketsMessage(50, 100));

			// Assert
			Assert.Null(exception);
		}

		[Fact]
		public void ShouldCallDisplayCpuPlayerPurchaseTicketsMessageWithoutPlayersThrowException()
		{
			// Arrange
			var messageDisplayService = new ConsoleMessageDisplayService();

			// Act
			Action act = () => messageDisplayService.DisplayCpuPlayerPurchaseTicketsMessage(null);

			// Assert
			Assert.Throws<ArgumentNullException>(act);
		}

		[Fact]
		public void ShouldCallDisplayWinnersNotThrowException()
		{
			// Arrange
			var messageDisplayService = new ConsoleMessageDisplayService();

			// Act
			var exception = Record.Exception(() => messageDisplayService.DisplayWinners(new List<WinnerDto>()));

			// Assert
			Assert.Null(exception);
		}

		[Fact]
		public void ShouldCallDisplayHouseProfitNotThrowException()
		{
			// Arrange
			var messageDisplayService = new ConsoleMessageDisplayService();

			// Act
			var exception = Record.Exception(() => messageDisplayService.DisplayHouseProfit(10));

			// Assert
			Assert.Null(exception);
		}
	}
}

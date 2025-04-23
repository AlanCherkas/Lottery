using Game.Services.Concrete;
using Xunit;

namespace GameTests.ServiceTests
{
	public class LotteryServiceTests
	{
		[Fact]
		public void ShouldCallStartLotteryNotThrowException()
		{
			// Arrange
			var lotteryService = new LotteryService();
			
			var input = new StringReader("5");
			Console.SetIn(input);

			// Act
			var exception = Record.Exception(() => lotteryService.StartLottery());

			// Assert
			Assert.Null(exception);
		}
	}
}

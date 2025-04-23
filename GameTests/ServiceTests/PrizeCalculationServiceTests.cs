using Game.Players.Abstract;
using Game.Services.Concrete;
using Game.DTOs;
using Xunit;

namespace GameTests.ServiceTests
{
	public class PrizeCalculationServiceTests
	{
		[Fact]
		public void ShouldCalculatePrizeWithoutPlayersThrowException()
		{
			// Arrange
			var prizeCalculationService = new PrizeCalculationService();

			// Act
			Action act = () => prizeCalculationService.CalculatePrize(new List<BasePlayer>());

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(act);
		}

		[Fact]
		public void ShouldCalculateHouseProfitEqualZero()
		{
			// Arrange
			var prizeCalculationService = new PrizeCalculationService();

			// Act
			double profit = prizeCalculationService.CalculateHouseProfit(new List<BasePlayer>(), new List<WinnerDto>());

			// Assert
			Assert.Equal(0, profit);
		}
	}
}

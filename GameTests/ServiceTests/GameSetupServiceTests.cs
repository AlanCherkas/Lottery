using Game.Services.Concrete;
using Game.Players.Abstract;
using Xunit;

namespace GameTests.ServiceTests
{
	public class GameSetupServiceTests
	{
		[Fact]
		public void ShouldGeneratePlayersReturnNotNullList()
		{
			// Arrange
			var gameSetupService = new GameSetupService();

			// Act
			List<BasePlayer> players = gameSetupService.GeneratePlayers();

			// Assert
			Assert.NotNull(players);
		}
	}
}

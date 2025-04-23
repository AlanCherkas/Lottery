using Game.Players.Abstract;
using Game.Services.Abstract;
using Game.Players.Concrete;
using Game.Players.Constants;

namespace Game.Services.Concrete
{
	public class GameSetupService : IGameSetupService
	{
		public List<BasePlayer> GeneratePlayers()
		{
			List<BasePlayer> players = new List<BasePlayer>
			{
				new UserPlayer(Guid.NewGuid(),
					$"{PlayersConstants.DefaultUserPlayerName}{PlayersConstants.DefaultUserPlayerNumber}",
					PlayersConstants.DefaultUserPlayerBalance, PlayersConstants.DefaultUserPlayerNumber)
			};

			Random random = new Random();
			int cpuPlayersAmount = random.Next(
				PlayersConstants.DefaultTotalPlayersMinimum,
				PlayersConstants.DefaultTotalPlayersMaximum);

			for (int i = PlayersConstants.CpuPlayerNameNumberOffsetFirst; i < cpuPlayersAmount + PlayersConstants.CpuPlayerNameNumberOffsetLast; i++)
			{
				players.Add(new CpuPlayer(Guid.NewGuid(),
					$"{PlayersConstants.DefaultCpuPlayerPrefix}{i}",
					PlayersConstants.DefaultCpuPlayerBalance, i));
			}

			return players;
		}
	}
}

using Game.Players.Abstract;
using Game.Services.Abstract;
using Game.Players.Concrete;
using Microsoft.Extensions.Configuration;
using Game.Players.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services.Concrete
{
	public class GameSetupService : IGameSetupService
	{
		private IConfiguration Configuration => ServiceBuilder.ServiceProvider?.GetService<IConfiguration>() ?? throw new InvalidOperationException("Configuration is not initialized.");

		public List<BasePlayer> GeneratePlayers()
		{
			List<BasePlayer> players = new List<BasePlayer>
			{
				new UserPlayer(Guid.NewGuid(),
					$"{PlayersConstants.DefaultUserPlayerName}{PlayersConstants.DefaultUserPlayerNumber}",
					float.Parse(Configuration["UserPlayerBalance"] ?? PlayersConstants.DefaultUserPlayerBalance), PlayersConstants.DefaultUserPlayerNumber)
			};

			Random random = new Random();
			int cpuPlayersAmount = random.Next(
				int.Parse(Configuration["TotalPlayersMinimum"] ?? PlayersConstants.DefaultTotalPlayersMinimum),
				int.Parse(Configuration["TotalPlayersMaximum"] ?? PlayersConstants.DefaultTotalPlayersMaximum));

			for (int i = PlayersConstants.CpuPlayerNameNumberOffsetFirst; i < cpuPlayersAmount + PlayersConstants.CpuPlayerNameNumberOffsetLast; i++)
			{
				players.Add(new CpuPlayer(Guid.NewGuid(),
					$"{PlayersConstants.DefaultCpuPlayerPrefix}{i}",
					float.Parse(Configuration["CpuPlayerBalance"] ?? PlayersConstants.DefaultCpuPlayerBalance), i));
			}

			return players;
		}
	}
}

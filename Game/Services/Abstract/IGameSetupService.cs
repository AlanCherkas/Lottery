using Game.Players.Abstract;

namespace Game.Services.Abstract
{
	public interface IGameSetupService
	{
		public List<BasePlayer> GeneratePlayers();
	}
}

using Game.Players.Abstract;

namespace Game.Players.Concrete
{
	internal class CpuPlayer : BasePlayer
	{
		public CpuPlayer(Guid id, string name, double balance, int number) : base(id, name, balance, number)
		{
		}
	}
}

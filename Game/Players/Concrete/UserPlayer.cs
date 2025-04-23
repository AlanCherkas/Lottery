using Game.Players.Abstract;

namespace Game.Players.Concrete
{
	internal class UserPlayer : BasePlayer
	{
		public UserPlayer(Guid id, string name, float balance, int number) : base(id, name, balance, number)
		{
		}
	}
}

using Game.Enums;
using Game.Players.Abstract;

namespace Game.DTOs
{
	public class WinnerDto
	{
		public BasePlayer? Player { get; set; }
		public double Prize { get; set; }
		public PrizeType PrizeType { get; set; }
		
	}
}

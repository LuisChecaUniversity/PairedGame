using System;

namespace PairedGame
{
	public sealed class GameInfo
	{
		private GameInfo(){}
		
		public static int EnemiesEncountered { get; set; }		
		public static int EnemiesKilled { get; set; }
		public static int EnemiesTurned { get; set; }
		
		public static double ItemSpawnRate { get; set; }
		public static double TimeOfTheDay { get; set; }
		
		public static float TotalGameTime { get; set; }
	}
}

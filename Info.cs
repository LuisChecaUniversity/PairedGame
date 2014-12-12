using System;

namespace PairedGame
{
	public static class Info
	{
		private static Random rnd = new Random();
		
		public static int EnemiesEncountered { get; set; }		
		public static int EnemiesKilled { get; set; }
		public static int EnemiesTurned { get; set; }
		
		public static double ItemSpawnRate { get; set; }
		public static double TimeOfTheDay { get; set; }
		
		public static float TotalGameTime { get; set; }
		
		public static Random Rnd { get { return rnd; } }
		
		public static float CameraHeight { get; set; }
		
		public static bool LevelClear { get; set; }
		public static int LevelNumber { get; set; }
		public static int MaxLevels { get { return 2; } }
	}
}

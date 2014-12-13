using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace PairedGame
{
	public class CrawlerScreen: Scene
	{
		public CrawlerScreen()
		{
			Info.TotalGameTime = 0f;
			Info.LevelNumber = 1;
			LoadLevel(Info.LevelNumber);
			ScheduleUpdate();
		}
		
		private void LoadLevel(int level)
		{
			// Clear scene
			if(Children.Count > 0)
				Children.Clear();
			// (Re)Initialize variables
			Info.CameraHeight = 200f;
			Info.LevelClear = false;
			Info.InBattle = false;
			Vector2 cameraCentre = new Vector2();
			// Load level layout from file
			Tile.Loader("/Application/assets/level" + level.ToString() + ".txt", ref cameraCentre, this);
			// Reset camera to player position
			Camera2D.SetViewFromHeightAndCenter(Info.CameraHeight, cameraCentre);
			if(level != Info.LevelNumber)
				Info.LevelNumber = level;
		}
		
		override public void Update(float dt)
		{
			Info.CameraHeight = Info.InBattle ? 150f : 200f;
			
			if(Info.LevelClear && Info.LevelNumber < Info.MaxLevels)
			{
				Info.LevelNumber++;
				LoadLevel(Info.LevelNumber);
			}
		}
	}
}

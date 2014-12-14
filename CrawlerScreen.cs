using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace PairedGame
{
	public class CrawlerScreen: Scene
	{
		public CrawlerScreen()
		{
			AudioManager.PlayMusic("ambient", true, 1f, 1f);
			LoadLevel(Info.LevelNumber);
			ScheduleUpdate();
		}
		
		private void LoadLevel(int level)
		{
			// Clear scene
			if(Children.Count > 0)
			{
				Children.ForEach(n => n.RemoveAllChildren(true));
				Children.Clear();
			}
			// (Re)Initialize variables
			Info.LevelClear = false;
			Info.InBattle = false;
			Info.HadConversation = false;
			Vector2 cameraCentre = new Vector2();
			// Load level layout from file
			Tile.Loader("/Application/assets/level" + level.ToString() + ".txt", ref cameraCentre, this);
			Info.CameraCentre = cameraCentre;
			if(level != Info.LevelNumber)
				Info.LevelNumber = level;
		}
		
		override public void Update(float dt)
		{
			Info.TotalGameTime += dt;
			Info.CameraHeight = Info.InBattle ? Tile.Height * 5 : Tile.Height * 8;
			Camera2D.SetViewFromHeightAndCenter(Info.CameraHeight, Info.CameraCentre);
			
			if(Info.LevelClear && Info.LevelNumber < Info.MaxLevels)
			{
				LoadLevel(++Info.LevelNumber);
			}
		}
	}
}

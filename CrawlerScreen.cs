using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace PairedGame
{
	public class CrawlerScreen: Scene
	{
		public CrawlerScreen()
		{
			Info.CameraHeight = 200f;
			Info.TotalGameTime = 0f;
			Vector2 cameraCentre = new Vector2();
			Tile.Loader("/Application/assets/level1.txt", ref cameraCentre, this);
			Camera2D.SetViewFromHeightAndCenter(Info.CameraHeight, cameraCentre);
		}
	}
}

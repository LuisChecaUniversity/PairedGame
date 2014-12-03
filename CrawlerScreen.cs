using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace PairedGame
{
	public class CrawlerScreen: Scene
	{
		public CrawlerScreen()
		{
			Camera2D.SetViewFromHeightAndBottomLeft(180f, Vector2.Zero);
			Tile.Loader("/Application/assets/level1.txt", this);
		}
	}
}

using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace PairedGame
{
	public enum Collidables
	{
		None = 0,
		Left,
		Bottom,
		Right,
		Top,
		BottomLeft,
		BottomRight,
		TopRight,
		TopLeft
	}
	
	public class Tile: SpriteTile
	{
		private bool isCollidable = false;
		private bool isLootable = false;
		private bool isOccupied = false;
		private Collidables colliadableSides = 0;
		
		public Tile(char loadKey, Vector2 position): base()
		{
			Position = position;
			
			switch(loadKey)
			{
			case 'S':
				TileIndex1D = 0;
				break;
			case 'X':
				isCollidable = true;
				break;
			default:
				break;
			}
		}
	}
}

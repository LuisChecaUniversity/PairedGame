using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public enum Collidable
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
		private bool isLootable = false;
		private bool isOccupied = false;
		private Collidable collidableSides = Collidable.None;
		
		public Tile(char loadKey, Vector2 position): base()
		{
			TextureInfo = TextureManager.Get("tiles");
			Position = position;
			Quad.S = TextureInfo.TileSizeInPixelsf;
			
			switch(loadKey)
			{
			case 'S': 
				isOccupied = true;
				TileIndex2D = new Vector2i(GameInfo.Rnd.Next(1, 4), GameInfo.Rnd.Next(1, 4));
				break;
			case 'X':
				TileIndex2D = new Vector2i(GameInfo.Rnd.Next(1, 4), GameInfo.Rnd.Next(1, 4));
				break;
			case 'D':
				TileIndex2D = new Vector2i(GameInfo.Rnd.Next(8, 11), 1);
				break;
			case 'P':
				TileIndex2D = new Vector2i(0, 4);
				collidableSides = Collidable.TopLeft;
				break;
			case 'O':
				TileIndex2D = new Vector2i(4, 4);
				collidableSides = Collidable.TopRight;
				break;
			case 'I':
				TileIndex2D = new Vector2i(0, 0);
				collidableSides = Collidable.BottomLeft;
				break;
			case 'U':
				TileIndex2D = new Vector2i(4, 0);
				collidableSides = Collidable.BottomRight;
				break;
			case 'M':
				TileIndex2D = new Vector2i(GameInfo.Rnd.Next(1, 4), 4);
				collidableSides = Collidable.Top;
				break;
			case 'N':
				TileIndex2D = new Vector2i(GameInfo.Rnd.Next(1, 4), 0);
				collidableSides = Collidable.Bottom;
				break;
			case 'B':
				TileIndex2D = new Vector2i(0, GameInfo.Rnd.Next(1, 4));
				collidableSides = Collidable.Left;
				break;
			case 'V':
				TileIndex2D = new Vector2i(4, GameInfo.Rnd.Next(1, 4));
				collidableSides = Collidable.Right;
				break;
			default:
				break;
			}
		}
		
		public static void Loader(string filepath, Scene scene)
		{
			int x = 0;
			int y = 0;
			int SIZE = 16;
			Tile t;
			foreach (string line in System.IO.File.ReadLines(filepath))
			{
				x = 0;
			    foreach (char c in line.ToUpper())
				{
					if (c != ' ')
					{
						t = new Tile(c, new Vector2(x, y));
						scene.AddChild(t);
					}
					x += SIZE;
				}
				y += SIZE;
			}
		}
	}
}

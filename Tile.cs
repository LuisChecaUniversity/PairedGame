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
		private Collidable collidableSides = Collidable.None;
		
		public Tile(char loadKey, Vector2 position): base()
		{
			TextureInfo = TextureManager.Get("tiles");
			Position = position;
			Quad.S = TextureInfo.TileSizeInPixelsf;

			// Based on loadKey set Tile to draw and its collision.
			switch(loadKey)
			{
			case 'S': 
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
			case 'Z':
				TileIndex2D = new Vector2i(11, 3);
				break;
			default:
				break;
			}
		}
		
		public bool IsInTile(Vector2 checkPosition) { return Tile.IsInTile(Position, checkPosition); }
		
		public void HandleCollision(ref Vector2 speed)
		{
			switch (collidableSides)
			{
			case Collidable.Bottom:
				speed.Y = speed.Y < 0 ? -speed.Y : speed.Y;
				break;
			case Collidable.BottomLeft:
				speed.X = speed.X < 0 ? -speed.X : speed.X;
				speed.Y = speed.Y < 0 ? -speed.Y : speed.Y;
				break;
			case Collidable.BottomRight:
				speed.X = speed.X > 0 ? -speed.X : speed.X;
				speed.Y = speed.Y < 0 ? -speed.Y : speed.Y;
				break;
			case Collidable.Left:
				speed.X = speed.X < 0 ? -speed.X : speed.X;
				break;
			case Collidable.None:
				break;
			case Collidable.Right:
				speed.X = speed.X > 0 ? -speed.X : speed.X;
				break;
			case Collidable.Top:
				speed.Y = speed.Y > 0 ? -speed.Y : speed.Y;
				break;
			case Collidable.TopLeft:
				speed.X = speed.X < 0 ? -speed.X : speed.X;
				speed.Y = speed.Y > 0 ? -speed.Y : speed.Y;
				break;
			case Collidable.TopRight:
				speed.X = speed.X > 0 ? -speed.X : speed.X;
				speed.Y = speed.Y > 0 ? -speed.Y : speed.Y;
				break;
			default:
				break;
			}
		}
		
		public static int Height { get { return 16; } }
		public static int Width { get { return 16; } }
		
		public static bool IsInTile(Vector2 tilePosition, Vector2 checkPosition)
		{
			if (tilePosition == checkPosition)
				return true;
			
			if(tilePosition.X <= checkPosition.X && tilePosition.X + Tile.Width > checkPosition.X &&
			   tilePosition.Y <= checkPosition.Y && tilePosition.Y + Tile.Height > checkPosition.Y)
			{
				return true;
			}
			return false;
		}
		
		public static void Loader(string filepath, ref Vector2 playerPos, Scene scene)
		{
			int x = 0;
			int y = 0;
			
			// Read whole level files
			var lines = System.IO.File.ReadAllLines(filepath);
			// Iterate end to start, line by line
			for (int i = lines.Length - 1; i >= 0; i--)
			{
				// New row: reset x position and read next line.
				x = 0;
				var line = lines[i].ToUpper();
			    foreach (char c in line)
				{
					if (c != ' ')
					{
						// Add tile at x, y
						scene.AddChild(new Tile(c, new Vector2(x, y)));
						if (c == 'S')
						{
							// S = Player start, store co-ordinates
							playerPos = new Vector2(x, y);
						}
					}
					// Move to next tile "grid"
					x += Width;
				}
				// End row: move y position to next tile row 
				y += Height;
			}
			
			if (playerPos != Vector2.Zero)
			{
				// Player position has been set, add the player.
				scene.AddChild(new Player(playerPos));
			}
		}
	}
}

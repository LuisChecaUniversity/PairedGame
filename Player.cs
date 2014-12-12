using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public class Player: EntityAlive
	{
		private static int PLAYER_INDEX = 0;
		
		public Player(Vector2 position):
			base(PLAYER_INDEX, position, new Vector2i(0, 1))
		{

		}
		
		override public void Update(float dt)
		{
			base.Update(dt);
		
			Info.TotalGameTime += dt;
			
	        var gamePadData = GamePad.GetData(0);

	        int deltaSpeed = 2;
			//Vector2 moveSpeed = new Vector2();
	
			// Handle movement
	        if((gamePadData.Buttons & GamePadButtons.Left) != 0)
	        {
                MoveSpeed.X = -deltaSpeed;
				// Set animation range.
				TileRangeX = new Vector2i(6, 7);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Right) != 0)
	        {
                MoveSpeed.X = deltaSpeed;
				TileRangeX = new Vector2i(4, 5);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Up) != 0)
	        {
                MoveSpeed.Y = deltaSpeed;
				TileRangeX = new Vector2i(2, 3);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Down) != 0)
	        {
                MoveSpeed.Y = -deltaSpeed;
				TileRangeX = new Vector2i(0, 1);
	        }
			
			// Set frame to start of animation range if outside of range
			if(TileIndex2D.X < TileRangeX.X || TileIndex2D.X > TileRangeX.Y)
				TileIndex2D.X = TileRangeX.X;
			
			// Find current tile and apply collision
			CollideWithTiles(ref MoveSpeed);
			
			// Apply the movement
			Position = Position + MoveSpeed;
			// Make camera follow the player
			Parent.Camera2D.SetViewFromHeightAndCenter(Info.CameraHeight, Position);			
		}
		
		private void CollideWithTiles(ref Vector2 moveSpeed)
		{
			if (SceneManager.CurrentScene == null)
				return;
			
			foreach (var child in SceneManager.CurrentScene.Children)
			{
				if (child != this)
				{
					Tile t = child as Tile;
					if (t != null)
					{
						if (t.Overlaps(this))
						{
							if (!MoveSpeed.IsZero()) t.HandleCollision(Position, ref MoveSpeed);
							if (t.Key == 'Z') 
							{
								Vector2 p = new Vector2(); 
								Sce.PlayStation.HighLevel.GameEngine2D.Scene s = Parent as Sce.PlayStation.HighLevel.GameEngine2D.Scene;
							    //Tile.Loader("/Application/assets/level2.txt", ref p, s);
							}
								
						}
						
					}
				}
			}
		}
	}
}


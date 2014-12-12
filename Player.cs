using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public class Player: EntityAlive
	{
		private static Vector2i PLAYER_INDEX = new Vector2i(0, 0);
		
		public Player(Vector2 position):
			base(position)
		{
			TileIndex2D = PLAYER_INDEX;
			// Initial animation range
			TileRange = new Vector2i(0, 1);
			// Attach custom animation function
			ScheduleInterval( (dt) => {
				if(IsAlive && MoveSpeed != Vector2.Zero)
				{
					int tileIndex = TileIndex2D.X < TileRange.Y ? TileIndex2D.X + 1 : TileRange.X;
					TileIndex2D.X = tileIndex;
				}
			}, 0.3f);
		}
		
		private Vector2 MoveSpeed { get; set; }
		private Vector2i TileRange { get; set; }
		
		override public void Update(float dt)
		{
			base.Update(dt);
			
	        var gamePadData = GamePad.GetData(0);

	        int deltaSpeed = 2;
			Vector2 moveSpeed = new Vector2();
	
			// Handle movement
	        if((gamePadData.Buttons & GamePadButtons.Left) != 0)
	        {
                moveSpeed.X = -deltaSpeed;
				// Set animation range.
				TileRange = new Vector2i(6, 7);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Right) != 0)
	        {
                moveSpeed.X = deltaSpeed;
				TileRange = new Vector2i(4, 5);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Up) != 0)
	        {
                moveSpeed.Y = deltaSpeed;
				TileRange = new Vector2i(2, 3);
	        }
	        if((gamePadData.Buttons & GamePadButtons.Down) != 0)
	        {
                moveSpeed.Y = -deltaSpeed;
				TileRange = new Vector2i(0, 1);
	        }
			
			// Set frame to start of animation range if outside of range
			if(TileIndex2D.X < TileRange.X || TileIndex2D.X > TileRange.Y)
				TileIndex2D.X = TileRange.X;
			
			// Find current tile and apply collision
			CollideWithTiles(ref moveSpeed);
			// Assign modified move speed to class property
			MoveSpeed = moveSpeed;
			
			// Apply the movement
			Position = Position + MoveSpeed;
			// Make camera follow the player
			Parent.Camera2D.SetViewFromHeightAndCenter(GameInfo.CameraHeight, Position);			
		}
		
		private void CollideWithTiles(ref Vector2 moveSpeed)
		{
			foreach (var child in SceneManager.CurrentScene.Children)
			{
				if (child != this)
				{
					if (Tile.IsInTile(child.Position, Position))
					{
						Tile t = child as Tile;
						if (t != null)
							t.HandleCollision(ref moveSpeed);
					}
				}
			}
		}
	}
}


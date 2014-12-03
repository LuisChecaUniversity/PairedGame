using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace PairedGame
{
	public class Player: EntityAlive
	{
		private static int PLAYER_INDEX = 9;
		
		public Player(Vector2 position):
			base(PLAYER_INDEX, position)
		{
		}
		
		override public void Update(float dt)
		{
			base.Update(dt);
			
	        var gamePadData = GamePad.GetData(0);

	        int speed = 4;
			int x = 0;
			int y = 0;
	
	        if((gamePadData.Buttons & GamePadButtons.Left) != 0)
	        {
                x = -speed;
	        }
	        if((gamePadData.Buttons & GamePadButtons.Right) != 0)
	        {
                x = speed;
	        }
	        if((gamePadData.Buttons & GamePadButtons.Up) != 0)
	        {
                y = speed;
	        }
	        if((gamePadData.Buttons & GamePadButtons.Down) != 0)
	        {
                y = -speed;
	        }
			Position = Position + new Vector2(x, y);
			
			Parent.Camera2D.SetViewFromHeightAndCenter(GameInfo.CameraHeight, Position);
		}
	}
}


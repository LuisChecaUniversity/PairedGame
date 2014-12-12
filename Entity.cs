using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	
	public class Entity: SpriteTile
	{
		protected bool isAlive = true;
		
		public Entity(Vector2 position):
			base()
		{
			TextureInfo = TextureManager.Get("entities");
			// Size and position
			Position = position;
			Quad.S = TextureInfo.TileSizeInPixelsf;
			// Attach update function to scheduler
			ScheduleUpdate();
		}
		
		public Entity(int tileIndexY, Vector2i tileRangeX, Vector2 position, float interval=0.2f):
			this(position)
		{
			// Create animation function. tileRange = { xMinTile2D, xMaxTile2D }
			TileIndex2D = new Vector2i(tileRangeX.X, tileIndexY);
			ScheduleInterval( (dt) => {
				if(IsAlive)
				{
					int tileIndex = TileIndex2D.X < tileRangeX.Y ? TileIndex2D.X + 1 : tileRangeX.X;
					TileIndex2D.X = tileIndex;
				}
			}, interval);
		}
		
		public Entity(int tileIndex, Vector2 position):
			this(position)
		{
			TileIndex1D = tileIndex;
		}
		
		override public void Update(float dt)
		{
			base.Update(dt);
		}
		
		public bool IsAlive { get{ return isAlive; } set{ isAlive = value; } }
	}
}

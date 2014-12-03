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
		}
		
		public Entity(Vector2i tileRange, Vector2 position): 
			this(position)
		{
			// Create animation function. tileRange = { minTile1D, maxTile1D }
			ScheduleInterval( (dt) => {
				if(IsAlive)
				{
					int tileIndex = TileIndex1D < tileRange.Y ? TileIndex1D + 1 : tileRange.X;
					TileIndex1D = tileIndex;
				}
			}, 0.2f);
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

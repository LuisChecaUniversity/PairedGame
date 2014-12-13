using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public class Entity: SpriteTile
	{
		public bool IsAlive { get; protected set; }
		
		public Entity(Vector2 position):
			base()
		{
			TextureInfo = TextureManager.Get("entities");
			// Size and position
			Position = position;
			Quad.S = TextureInfo.TileSizeInPixelsf;
			IsAlive = true;
			// Attach update function to scheduler
			ScheduleUpdate();
		}
		
		public Entity(int tileIndex, Vector2 position):
			this(position)
		{
			TileIndex1D = tileIndex;
		}
		
		override public void Update(float dt)
		{
			base.Update(dt);
			if(!IsAlive)
				Parent.RemoveChild(this, true);
		}
		
	}
}

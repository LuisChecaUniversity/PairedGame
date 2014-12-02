using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace PairedGame
{
	public enum AttackStatus
	{
		None = 0,
		MeleeNormal,
		MeleeStrong,
		RangedNormal,
		RangedStrong
	}
	
	public struct Statistics
	{
		int Health = 100;
		int Lives = 5;
		int Defense = 100;
		int Attack = 100;
		int RangedAttack = 75;
		double Luck = rnd.NextDouble();
	}
	
	public class Character: SpriteTile
	{
		private bool isAlive = true;
		private AttackStatus attackState = AttackStatus.None;
		private Statistics statistics = new Statistics();
		private static Random rnd = new Random();
		
		public Character(TextureInfo textureInfo, Vector2i tileSize, Vector2 position):
			base(textureInfo, tileSize)
		{
			// Size and position
			Position = position;
			Quad.S = textureInfo.TileSizeInPixelsf;
			// Attach update function to scheduler
			Schedule(Update);
		}
		
		public Character(TextureInfo textureInfo, Vector2i tileSize, Vector2i tileRange, Vector2 position): 
			this(textureInfo, tileSize, position)
		{
			// Create animation function. tileRange = { minTile1D, maxTile1D }
			sprite.ScheduleInterval( (dt) => {
				if(IsAlive)
				{
					int tileIndex = sprite.TileIndex1D < tileRange.Y ? sprite.TileIndex1D + 1 : tileRange.X;
					sprite.TileIndex1D = tileIndex;
				}
			}, 0.2f);
		}
		
		public Character(TextureInfo textureInfo, Vector2i tileSize, int tileIndex, Vector2 position):
			this(TextureInfo, tileSize, position)
		{
			TileIndex1D = tileIndex;
		}
		
		public Statistics Stats { get{ return statistics; } }
		
		public bool IsAlive { get{ return isAlive; } set{ isAlive = value; } }
		
		public void Update(float dt)
		{
			bool isDefending = false;
			int damage = 0;
			switch(attackState)
			{
			case None:
				isDefending = true;
				break;
			case MeleeNormal:
				damage = (int)(Stats.Attack * Stats.Luck);
				break;
			case MeleeStrong:
				damage = (int)(Stats.Attack * Stats.Luck * 1.15);
				break;
			case RangedNormal:
				damage = (int)(Stats.RangedAttack * Stats.Luck);
				break;
			case RangedStrong:
				damage = (int)(Stats.RangedAttack * Stats.Luck * 1.15);
				break;
			default:
				break;
			}
			
			if (isDefending)
			{
				Stats.Health -= Math.Abs(damage - Stats.Defense);
			}
		}
		
		public void Dispose()
		{
			TextureInfo.Dispose();
		}
		
		public AttackStatus RandomAttack()
		{
			AttackStatus attack = (AttackStatus)rnd.Next(1, AttackStatus.RangedStrong + 1);
			return attack;
		}
	}
}

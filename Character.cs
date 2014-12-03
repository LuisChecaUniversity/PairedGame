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
	
	public class Statistics
	{
		public int Health = 100;
		public int Lives = 5;
		public int Defense = 100;
		public int Attack = 100;
		public int RangedAttack = 75;
		public double Luck = GameInfo.Rnd.NextDouble();
	}
	
	public class Character: SpriteTile
	{
		private bool isAlive = true;
		private AttackStatus attackState = AttackStatus.None;
		private Statistics statistics = new Statistics();
		
		public Character(Vector2 position):
			base()
		{
			TextureInfo = TextureManager.Get("chars");
			// Size and position
			Position = position;
			Quad.S = TextureInfo.TileSizeInPixelsf;
			// Attach update function to scheduler
			ScheduleUpdate();
		}
		
		public Character(Vector2i tileRange, Vector2 position): 
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
		
		public Character(int tileIndex, Vector2 position):
			this(position)
		{
			TileIndex1D = tileIndex;
		}
		
		public Statistics Stats { get{ return statistics; } }
		
		public bool IsAlive { get{ return isAlive; } set{ isAlive = value; } }
		
		override public void Update(float dt)
		{
			base.Update(dt);
			bool isDefending = false;
			int damage = 0;
			switch(attackState)
			{
			case AttackStatus.None:
				isDefending = true;
				break;
			case AttackStatus.MeleeNormal:
				damage = (int)(Stats.Attack * Stats.Luck);
				break;
			case AttackStatus.MeleeStrong:
				damage = (int)(Stats.Attack * Stats.Luck * 1.15);
				break;
			case AttackStatus.RangedNormal:
				damage = (int)(Stats.RangedAttack * Stats.Luck);
				break;
			case AttackStatus.RangedStrong:
				damage = (int)(Stats.RangedAttack * Stats.Luck * 1.15);
				break;
			default:
				break;
			}
			
			if (isDefending)
			{
				Stats.Health -= System.Math.Abs(damage - Stats.Defense);
			}
		}
		
		public AttackStatus RandomAttack()
		{
			AttackStatus attack = (AttackStatus)GameInfo.Rnd.Next(1, (int)AttackStatus.RangedStrong + 1);
			return attack;
		}
	}
}

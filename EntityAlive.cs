using System;
using Sce.PlayStation.Core;
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
		public double Luck = Info.Rnd.NextDouble();
	}
	
	public class EntityAlive: Entity
	{
		protected AttackStatus attackState = AttackStatus.None;
		protected Statistics statistics = new Statistics();
		protected Vector2 MoveSpeed = new Vector2();
		protected Vector2i TileRangeX = new Vector2i();
		
		public EntityAlive(Vector2 position):
			base(position)
		{
		}
		
		public EntityAlive(int tileIndexY, Vector2 position, Vector2i tileRangeX, float interval=0.2f):
			base(position)
		{
			// Assign variables
			TileIndex2D = new Vector2i(tileRangeX.X, tileIndexY);
			TileRangeX = tileRangeX;
			// Attach custom animation function
			ScheduleInterval( (dt) => {
				if(IsAlive && !MoveSpeed.IsZero())
				{
					int tileIndex = TileIndex2D.X < TileRangeX.Y ? TileIndex2D.X + 1 : TileRangeX.X;
					TileIndex2D.X = tileIndex;
				}
			}, interval);
		}
		
		public EntityAlive(int tileIndex, Vector2 position):
			base(tileIndex, position)
		{
		}
		
		public Statistics Stats { get{ return statistics; } }
		
		override public void Update(float dt)
		{
			base.Update(dt);
			// Reset variables
			bool isDefending = false;
			int damage = 0;
			MoveSpeed = Vector2.Zero;
			
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
			AttackStatus attack = (AttackStatus)Info.Rnd.Next(1, (int)AttackStatus.RangedStrong + 1);
			return attack;
		}
	}
}


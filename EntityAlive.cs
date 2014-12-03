using System;
using Sce.PlayStation.Core;

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
	
	public class EntityAlive: Entity
	{
		protected AttackStatus attackState = AttackStatus.None;
		protected Statistics statistics = new Statistics();
		
		public EntityAlive(Vector2 position):
			base(position)
		{
			// Attach update function to scheduler
			ScheduleUpdate();
		}
		
		public EntityAlive(Sce.PlayStation.HighLevel.GameEngine2D.Base.Vector2i tileRange, Vector2 position):
			base(tileRange, position)
		{
			ScheduleUpdate();
		}
		
		public EntityAlive(int tileIndex, Vector2 position):
			base(tileIndex, position)
		{
			ScheduleUpdate();
		}
		
		public Statistics Stats { get{ return statistics; } }
		
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


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
		public int Lives = 2;
		public int Defense = 50;
		public int Attack = 100;
		public int RangedAttack = 75;

		public double Luck { get { return Info.Rnd.NextDouble() * (1 - 0.5) + 0.5; } }
	}
	
	public class EntityAlive: Entity
	{
		protected AttackStatus attackState = AttackStatus.None;
		protected Statistics statistics = new Statistics();
		protected Vector2 MoveSpeed = new Vector2();
		protected Vector2i TileRangeX = new Vector2i();
		
		public Statistics Stats { get { return statistics; } }

		public EntityAlive Opponent { get; set; }

		public bool IsDefending { get; set; }

		public bool InBattle { get; set; }

		public int Damage { get { return CalculateDamage(attackState); } }

		public int DamageReceived { get; protected set; }
		
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
			ScheduleInterval((dt) => {
				if(IsAlive)
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
		
		override public void Update(float dt)
		{
			MoveSpeed = Vector2.Zero;
			// Run for dying
			if(Stats.Health <= 0)
			{
				if(--Stats.Lives <= 0)
					IsAlive = false;
				
			}
			
			if(InBattle)
			{
				// Deal damage
				if(Opponent != null)
					Opponent.DamageReceived = Damage;
				
				// Take damage
				if(DamageReceived != 0)
					Stats.Health -= DamageReceived - (IsDefending ? Stats.Defense : 0);
				
				// Reset attack, defense
				attackState = AttackStatus.None;
				IsDefending = false;
			}
			base.Update(dt);
		}
		
		private int CalculateDamage(AttackStatus attack)
		{
			int damage = 0;
			switch(attack)
			{
			case AttackStatus.None:
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
			return damage;
		}
		
		public AttackStatus RandomAttack()
		{
			AttackStatus attack = (AttackStatus)Info.Rnd.Next(1, (int)AttackStatus.RangedStrong + 1);
			return attack;
		}
	}
}


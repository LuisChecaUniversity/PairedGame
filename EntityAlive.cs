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
		public int Defense = 100;
		public int Attack = 10;
		public int RangedAttack = 15;

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
			// NPCs don't block by default
			IsDefending = false;
			// Attach attack timer
			if(this.GetType() == typeof(EntityAlive))
			{
				ScheduleInterval((dt) => {
					if(InBattle)
						attackState = RandomAttack();
				}, 2f);
			}
		}
		
		public EntityAlive(int tileIndexY, Vector2 position, Vector2i tileRangeX, float interval=0.2f):
			this(position)
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
				else					
					TileIndex2D.X = TextureInfo.NumTiles.X - 1;
			}, interval);
		}
		
		public EntityAlive(Vector2i tileIndex2D, Vector2 position):
			this(position)
		{
			TileIndex2D = tileIndex2D;
		}
		
		override public void Update(float dt)
		{
			MoveSpeed = Vector2.Zero;
			// Dying
			if(Stats.Health <= 0)
			{
				if(--Stats.Lives <= 0)
					IsAlive = false;
			}
			// Fight!
			if(InBattle)
			{
				// Deal damage
				if(Opponent != null)
				{
					Opponent.DamageReceived = Damage;
					// Enemy killed
					if(!Opponent.IsAlive)
					{
						Info.InBattle = false;
						InBattle = false;
						Opponent.InBattle = false;
						Opponent.Opponent = null;
						Opponent = null;
						SceneManager.ReplaceUIScene();
						Info.EnemiesKilled += 1;
						Info.Reputation += 1;
					}
				}
				
				// Take damage
				if(DamageReceived > 0)
				{
					if(IsDefending && Stats.Defense > 0 && DamageReceived <= Stats.Defense)
						Stats.Defense -= DamageReceived;
					else
						Stats.Health -= DamageReceived;
					
					DamageReceived = 0;
				}
				// Reset attack, defense
				attackState = AttackStatus.None;
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


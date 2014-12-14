using Sce.PlayStation.HighLevel.UI;

namespace PairedGame
{
	public class HintBox: ContainerWidget
	{
		private ImageBox box = new ImageBox();
		private Label label = new Label();
		
		public HintBox(string filepath): base()
		{
			ImageAsset i = new ImageAsset(filepath);
			// ImageBox
			box.Image = i;
			box.Width = i.Width;
			box.Height = i.Height;
			AddChildLast(box);
			// Label
			label.SetPosition(X + i.Width, Y + i.Height / 2 - label.TextHeight / 2);
			label.TextColor = new UIColor(1f, 1f, 1f, 1f);
			label.TextShadow = new TextShadowSettings();
			label.TextShadow.Color = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			AddChildLast(label);
		}
		
		public string Text { get { return label.Text; } set { label.Text = value; } }
		
		public Label Desc { get { return label; } }
	}
	
	public class FightUI: Scene
	{
		private static HintBox cross = new HintBox("/Application/assets/vita_cross.png");
		private static HintBox circle = new HintBox("/Application/assets/vita_circle.png");
		private static HintBox square = new HintBox("/Application/assets/vita_square.png");
		private static HintBox triangle = new HintBox("/Application/assets/vita_triangle.png");
		private static HintBox l = new HintBox("/Application/assets/vita_l.png");
		private static ImageBox health = new ImageBox();
		private static ImageBox enemyHealth = new ImageBox();
		private static Label livesText = new Label();
		private static Label enemyLivesText = new Label();
		private static float ImageSize = 50;
		private EntityAlive enemy = null;
		private Player player = null;
		
		public FightUI(Player player, EntityAlive enemy): base()
		{
			Title = "FightUI";
			
			this.player = player;
			this.enemy = enemy;
			
			// Lives text
			livesText.SetPosition(20, 5);
			livesText.Text = "Lives: " + player.Stats.Lives.ToString();
			livesText.TextColor = new UIColor(1f, 1f, 1f, 1f);
			livesText.TextShadow = new TextShadowSettings();
			livesText.TextShadow.Color = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			RootWidget.AddChildLast(livesText);
			// Health bar
			health.SetPosition(20, 30);
			health.Image = new ImageAsset("/Application/assets/green.png");
			health.Height = 10;
			health.Width = player.Stats.Health;
			RootWidget.AddChildLast(health);
			
			// Enemy lives
			enemyLivesText.SetPosition(450, 5);
			enemyLivesText.Text = "Lives: " + enemy.Stats.Lives.ToString();
			enemyLivesText.TextColor = new UIColor(0.8f, 0f, 0f, 1f);
			enemyLivesText.TextShadow = new TextShadowSettings();
			enemyLivesText.TextShadow.Color = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			RootWidget.AddChildLast(enemyLivesText);
			// Enemy health
			enemyHealth.SetPosition(450, 30);
			enemyHealth.Image = new ImageAsset("/Application/assets/green.png");
			enemyHealth.Height = 10;
			enemyHealth.Width = enemy.Stats.Health;
			RootWidget.AddChildLast(enemyHealth);
			
			// Hint buttons
			float x = 200;
			float y = 40;
			
			// Hint image
			cross.SetPosition(x, y);
			cross.Text = "Melee";
			RootWidget.AddChildLast(cross);
			
			// New line
			y += ImageSize;
			circle.SetPosition(x, y);
			circle.Text = "Strong Melee";
			RootWidget.AddChildLast(circle);
			
			// New line
			y += ImageSize;
			square.SetPosition(x, y);
			square.Text = "Ranged";
			RootWidget.AddChildLast(square);
			
			// New line
			y += ImageSize;
			triangle.SetPosition(x, y);
			triangle.Text = "Strong Ranged";
			RootWidget.AddChildLast(triangle);
		}
		
		override protected void OnUpdate(float dt)
		{
			UIColor black = new UIColor(.1f, .1f, .1f, 1f);
			UIColor white = new UIColor(1f, 1f, 1f, 1f);
			Label label = null;
			
			// Match attack to hint box
			switch(player.Attack)
			{
			case AttackStatus.MeleeNormal:
				label = cross.Desc;
				break;
			case AttackStatus.MeleeStrong:
				label = circle.Desc;
				break;
			case AttackStatus.RangedNormal:
				label = square.Desc;
				break;
			case AttackStatus.RangedStrong:
				label = triangle.Desc;
				break;
			}
			// Block to L hint box
			if(player.IsDefending)
				label = l.Desc;
			
			// Any matches then swap colors
			if(label != null)
			{
				label.TextColor = black;
				label.TextShadow.Color = white;
			}
			else
			{
				cross.Desc.TextColor = white;
				cross.Desc.TextShadow.Color = black;
				circle.Desc.TextColor = white;
				circle.Desc.TextShadow.Color = black;
				square.Desc.TextColor = white;
				square.Desc.TextShadow.Color = black;
				triangle.Desc.TextColor = white;
				triangle.Desc.TextShadow.Color = black;
			}
		}
	}
}


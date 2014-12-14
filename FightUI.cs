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
			health.Height = 20;
			health.Width = player.Stats.Health;
			RootWidget.AddChildLast(health);
			
			// Enemy lives
			enemyLivesText.SetPosition(480, 5);
			enemyLivesText.Text = "Lives: " + enemy.Stats.Lives.ToString();
			enemyLivesText.TextColor = new UIColor(0.8f, 0f, 0f, 1f);
			enemyLivesText.TextShadow = new TextShadowSettings();
			enemyLivesText.TextShadow.Color = new UIColor(0.1f, 0.1f, 0.1f, 1f);
			RootWidget.AddChildLast(enemyLivesText);
			// Enemy health
			enemyHealth.SetPosition(480, 30);
			enemyHealth.Image = new ImageAsset("/Application/assets/green.png");
			enemyHealth.Height = 20;
			enemyHealth.Width = enemy.Stats.Health;
			RootWidget.AddChildLast(enemyHealth);
			
			// Hint buttons
			float x = 180;
			float y = 100;
			
			// Hint image
			cross.SetPosition(x, y);
			cross.Text = "Melee";
			RootWidget.AddChildLast(cross);
			
			// Circle line
			y += ImageSize;
			circle.SetPosition(x, y);
			circle.Text = "Strong Melee";
			RootWidget.AddChildLast(circle);
			
			// Square line
			y += ImageSize;
			square.SetPosition(x, y);
			square.Text = "Ranged";
			RootWidget.AddChildLast(square);
			
			// Triangle line
			y += ImageSize;
			triangle.SetPosition(x, y);
			triangle.Text = "Strong Ranged";
			RootWidget.AddChildLast(triangle);
			
			// L line
			y += ImageSize;
			l.SetPosition(x, y);
			l.Text = "Block";
			RootWidget.AddChildLast(l);
		}
		
		override protected void OnUpdate(float dt)
		{
			// Update stats
			livesText.Text = "Lives: " + player.Stats.Lives.ToString();
			enemyLivesText.Text = "Lives: " + enemy.Stats.Lives.ToString();
			health.Width = player.Stats.Health;
			enemyHealth.Width = enemy.Stats.Health;
			
			UIColor black = new UIColor(.1f, .1f, .1f, 1f);
			UIColor white = new UIColor(1f, 1f, 1f, 1f);
			Label[] labels = { cross.Desc, circle.Desc, square.Desc, triangle.Desc, l.Desc };
			Label label = null;
			
			// Match attack to hint box
			if(player.Attack != AttackStatus.None)
				label = labels[(int)player.Attack - 1];
			
			// Match block to hint box
			if(player.IsDefending)
				label = l.Desc;			
			
			// If any matches then swap colors
			if(label != null)
			{
				label.TextColor = black;
				label.TextShadow.Color = white;
			}
			else
			{
				foreach(Label lab in labels)
				{
					if(label != lab)
					{
						lab.TextColor = white;
						lab.TextShadow.Color = black;
					}
				}
			}
		}
	}
}


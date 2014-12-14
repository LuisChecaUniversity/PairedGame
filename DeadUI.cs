using Sce.PlayStation.HighLevel.UI;

namespace PairedGame
{
	public class DeadUI: Scene
	{
		public DeadUI()
		{
			Label lbl = new Label();
			lbl.TextColor = new UIColor(1f, 0f, 0f, 1f);
			lbl.TextShadow = new TextShadowSettings();
			lbl.TextShadow.Color = new UIColor(.1f, .1f, .1f, 1f);
			lbl.Text = "GAME OVER";
			lbl.SetPosition(440, 200);
			RootWidget.AddChildLast(lbl);
			
			Button btn = new Button();
			btn.Text = "Retry";
			btn.SetPosition(450, 225);
			btn.SetSize(100, 35);
			btn.ButtonAction += (sender, e) => { 
				SceneManager.ReplaceScene(new CrawlerScreen());
				SceneManager.ReplaceUIScene(new GameUI());
			};
			RootWidget.AddChildLast(btn);
		}
	}
}


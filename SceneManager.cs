using System;

namespace PairedGame
{
	public sealed class SceneManager
	{
		private SceneManager() {}
		//private static readonly SceneManager _instance = new SceneManager();
		//public static SceneManager Instance { get{ return _instance; } }
		
		public static Sce.PlayStation.HighLevel.GameEngine2D.Scene CurrentScene
		{
			get{ return Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.CurrentScene; } 
		}
		public static void ReplaceScene(Sce.PlayStation.HighLevel.GameEngine2D.Scene scene)
		{
			Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.ReplaceScene(scene);
		}
		
		public static Sce.PlayStation.HighLevel.UI.Scene CurrentUIScene
		{
			get{ return Sce.PlayStation.HighLevel.UI.UISystem.CurrentScene; } 
		}
		public static void ReplaceUIScene(Sce.PlayStation.HighLevel.UI.Scene scene)
		{
			Sce.PlayStation.HighLevel.UI.UISystem.SetScene(scene);
		}
	}
}


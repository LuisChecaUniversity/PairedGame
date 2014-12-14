using System.Collections.Generic;
using Sce.PlayStation.HighLevel.UI;

namespace PairedGame
{
	public class Conversation: Scene
	{
		private static Button[] choice = { new Button(), new Button(), new Button() };
		private static Button read = new Button();
		private static Label text = new Label();
		private static List<string> choices = new List<string>();
		private static List<string> answers = new List<string>();
		private static int MaxAnswers = 0;
		private int Count = 0;
		
		public Conversation()
		{
			Title = "Conversations";
			Count = 0;
			
			if(choices.Count == 0 && answers.Count == 0)
				Load(Info.LevelNumber);
			
			text.SetPosition(20, 0);
			text.SetSize(940, 100);
			text.TextColor = new UIColor(1f, 1f, 1f, 1f);
			text.TextShadow = new TextShadowSettings();
			text.TextShadow.Color = new UIColor(0.2f, 0.2f, 0.2f, 1f);
			text.TextTrimming = TextTrimming.None;
			text.LineBreak = LineBreak.Word;
			
			RootWidget.AddChildLast(text);
			
			read.SetPosition(20, 100);
			read.SetSize(150, 30);
			read.SetFocus(true);
			read.ButtonAction += NextChoiceAction;
			read.Text = "Answer him";
			RootWidget.AddChildLast(read);
			
			for(int i = 0; i < 3; i++)
			{
				choice[i].SetPosition(20, 40 * i);
				choice[i].SetSize(920, 30);
				choice[i].ButtonAction += WrongAnswerAction;
			}
		}

		private void NextChoiceAction(object sender, TouchEventArgs e)
		{
			RootWidget.RemoveChild(text);
			RootWidget.RemoveChild(read);
			choice[0].SetFocus(true);
			for(int i = 0; i < 3; i++)
			{
				choice[i].Text = choices[i + (3 * Count)];
				if(choice[i].Text.StartsWith("Right"))
				{
					choice[i].Text = choice[i].Text.Substring("Right ".Length);
					choice[i].ButtonAction -= WrongAnswerAction;
					choice[i].ButtonAction += NextAnswerAction;
				}
				RootWidget.AddChildLast(choice[i]);
			}
		}
		
		private void NextAnswerAction(object sender, TouchEventArgs e)
		{
			for(int i = 0; i < 3; i++)
			{
				if(choice[i] == sender)
					text.Text = answers[i + (3 * Count)];
				RootWidget.RemoveChild(choice[i]);
			}
			if(++Count >= MaxAnswers)
			{
				read.ButtonAction -= NextChoiceAction;
				read.ButtonAction += EndAction;
			}
			read.SetFocus(true);
			RootWidget.AddChildLast(text);
			RootWidget.AddChildLast(read);
		}
		
		private void WrongAnswerAction(object sender, TouchEventArgs e)
		{
			read.ButtonAction -= NextChoiceAction;
			read.ButtonAction += EndAction;
			Info.InBattle = true;
			NextAnswerAction(sender, e);
		}
		
		private void EndAction(object sender, TouchEventArgs e)
		{
			SceneManager.ReplaceUIScene();
			Info.HadConversation = true;
			SceneManager.ResumeScene();
		}
		
		public static void Load(int level)
		{
			string[] lines = System.IO.File.ReadAllLines("/Application/assets/miniboss" + level.ToString() + ".txt");
			text.Text = lines[0];
			
			for(int i = 1; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("Player"))
				{
					choices.Add(lines[i].Substring("Player ".Length));
					MaxAnswers++;
				}
				if(lines[i].StartsWith("Boss"))
					answers.Add(lines[i].Substring("Boss ".Length));
			}
			MaxAnswers /= 3;
		}
	}
}

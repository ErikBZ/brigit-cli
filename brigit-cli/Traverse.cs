using System;
using System.Text;
using Brigit;
using Brigit.Structure.Exchange;

namespace brigit_cli
{
	class Traverser
	{
		Conversation conversation;

		public Traverser(Brigit.Conversation conv)
		{
			conversation = conv;
		}

		public void Traverse()
		{
			var error = false;
			conversation.StartNewRun();

			while (!conversation.Complete && !error)
			{
				Info info = conversation.GetInfo();
				// render
				Render(info);

				// wait for input
				string input = Console.ReadLine();
				int choice = 0;
				bool parsed = int.TryParse(input, out choice);

				// move to next
				Next(parsed, choice, info);
			}
		}

		private bool Next(bool inputParsed, int choice, Info info)
		{
			bool error = false;
			if(info.type == Info.Type.Descision && info.Descision.Interactive)
			{
				if (!inputParsed || info.Descision.Choices.Count < choice)
				{
					throw new Exception("Need valid choice");
				}

				error = !(conversation.Next(info.Descision.Choices[choice].NextNode));
			}
			else
			{
				error = !(conversation.Next());
			}

			return error;
		}
		
		private void Render(Info info)
		{
			switch (info.type)
			{
				case Info.Type.Descision:
					RenderDescsion(info.Descision);
					break;
				case Info.Type.Dialog:
					RenderDialog(info.Dialog);
					break;
				default:
					break;
			}
		}

		private void RenderDialog(DialogSinglet dialog)
		{
			Console.WriteLine(dialog.Character + ":");
			Console.WriteLine("\t" + dialog.Text);
		}

		private void RenderDescsion(Descision descision)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\n");
			for(int i=0; i<descision.Choices.Count; i++)
			{
				sb.Append("\t" + i + ": " + descision.Choices[i].Text);
				sb.Append("\n");
			}
			Console.WriteLine(sb.ToString());
		}
	}
}

using System;
using System.IO;
using Brigit;
using ManyConsole;

namespace brigit_cli
{
	class TraverseGraphCommand : ConsoleCommand
	{
		public string FileName { get; set; }

		public TraverseGraphCommand()
		{
			IsCommand("Traverse", "Parses the given file and allows you to traverse the story graph");
			HasRequiredOption("f|file=", "File to traverse", f => FileName = f);
		}

		public override int Run(string[] remainingArguments)
		{
			try
			{
				string pathToFile = Path.Combine(Directory.GetCurrentDirectory(), FileName);
				var graph = ConversationLoader.CreateConversation(pathToFile);
				var runner = new Traverser(graph);
				runner.Traverse();

				// success
				return 0;
			}
			catch(Exception e)
			{
				Console.Error.Write(e.Message);
				Console.Error.Write(e.StackTrace);

				// errorrrr
				return 1;
			}
		}
	}
}

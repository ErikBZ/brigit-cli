using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManyConsole;
using Brigit;

namespace brigit_cli
{
	class PreprocessCommand : ConsoleCommand
	{
		public string FileName { get; set; }

		public PreprocessCommand()
		{
			// "this" is not necessary and according to MSDN best practices
			// should be avoided
			IsCommand("Preprocess", "Command makes sure that the following text file compiles");
			HasRequiredOption("f|file=", "The tome file to compile", f => FileName = f);
			// if i need any other options i'll add them here
		}

		public override int Run(string[] remainingArguments)
		{
			try
			{
				var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), FileName);
				// does not need to save anything just needs to parse it
				ConversationLoader.CreateConversation(pathToFile);
				Console.Out.WriteLine(FileName + " parsed correctly.");
				return 0;
			}
			catch (Exception ex)
			{
				// change this with some better exception handling
				Console.Error.WriteLine(ex.Message);
				Console.Error.WriteLine(ex.StackTrace);

				return 1;
			}
		}
	}
}

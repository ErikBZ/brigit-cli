using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManyConsole;

namespace brigit_cli
{
	class Program
	{
		static int Main(string[] args)
		{
			var commands = GetCommands();

			return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
		}

		public static IEnumerable<ConsoleCommand> GetCommands()
		{
			return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
		}
	}
}

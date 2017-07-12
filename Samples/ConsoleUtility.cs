using System;

namespace Samples
{
	internal static class ConsoleUtility
	{
		public static void Green(string value) => WriteLineInternal(value, ConsoleColor.Green);
		public static void Red(string value) => WriteLineInternal(value, ConsoleColor.Red);

		public static void WaitForContinue(string message)
		{
			Green($"{message}. Press ENTER to continue");
			Console.ReadLine();
		}

		private static void WriteLineInternal(string value, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(value);
			Console.ResetColor();
		}
	}
}

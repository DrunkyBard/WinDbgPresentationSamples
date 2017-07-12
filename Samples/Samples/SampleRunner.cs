using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.Samples
{
	internal sealed class SampleRunner
	{
		private readonly Dictionary<int, Sample> _supportedCommands;
		private readonly string _selectionView;

		internal SampleRunner(IReadOnlyCollection<Sample> supportedCommands)
		{
			_supportedCommands = supportedCommands
				.Select((sample, i) => new {i, sample})
				.ToDictionary(x => x.i+1, x => x.sample);
			_selectionView = _supportedCommands
				.Aggregate(
					new StringBuilder($"Choose sample:{Environment.NewLine}"), 
					(builder, orderedSample) => builder.AppendLine($"{orderedSample.Key}. {orderedSample.Value.Description}"))
				.ToString();
		}

		public void Run()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine(_selectionView);
				int sampleNum;
				Sample chosenSample;

				if (int.TryParse(Console.ReadLine(), out sampleNum) && _supportedCommands.TryGetValue(sampleNum, out chosenSample))
				{
					chosenSample.Execute();
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Incorrect sample. Press any key to continue");
					Console.ResetColor();
				}
			}
		}
	}
}

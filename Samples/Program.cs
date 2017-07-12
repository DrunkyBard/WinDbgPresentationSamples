using System.Collections.Generic;
using Samples.Commands;

namespace Samples
{
	static class Program
	{
		static void Main()
		{
			var samples = new List<Sample>
			{
				new ObjectLayoutSample(),
				new BreakpointsSample(),
				new ObjectInspectionSample(),
				new HeapAndStackInspectionSample(),
				new AppDomainInspectionSample(),
				new MemoryLeaksSample(),
				new DeadlocksSample()
			};

			var commandRunner = new CommandRunner(samples);
			commandRunner.Run();
		}
	}
}

using System.Collections.Generic;
using Samples.Samples;

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

			var sampleRunner = new SampleRunner(samples);
			sampleRunner.Run();
		}
	}
}

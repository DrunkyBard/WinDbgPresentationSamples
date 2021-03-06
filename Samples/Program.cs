﻿using System.Collections.Generic;
using Samples.Samples;

namespace Samples
{
	static class Program
	{
		static void Main()
		{
			var samples = new List<Sample>
			{
				new ObjectInspectionSample(),
				new BreakpointsSample(),
				new HeapAndStackInspectionSample(),
				new AppDomainInspectionSample(),
				new MemoryLeaksSample(),
				new DeadlocksSample(),
				new ExceptionsSample()
			};

			var sampleRunner = new SampleRunner(samples);
			sampleRunner.Run();
		}
	}
}

using System;
using System.Diagnostics;

namespace Samples.Commands
{
	internal abstract class Sample
	{
		public abstract SampleType Type { get; }

		public abstract string Description { get; }

		public void Execute()
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}

			ExecuteInternal();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		protected abstract void ExecuteInternal();
	}
}

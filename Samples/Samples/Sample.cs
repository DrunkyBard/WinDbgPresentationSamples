using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Samples.Samples
{
	internal abstract class Sample
	{
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);


		public abstract SampleType Type { get; }

		public abstract string Description { get; }

		public void Execute()
		{
			bool isDebuggerAttached = false;
			CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerAttached);

			if (isDebuggerAttached)
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

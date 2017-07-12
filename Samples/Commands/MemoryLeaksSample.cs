// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedField.Local
// ReSharper disable CollectionNeverQueried.Local

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Console = Samples.ConsoleUtility;

namespace Samples.Commands
{
	internal sealed class MemoryLeaksSample : Sample
	{
		public override SampleType Type => SampleType.MemoryLeaks;
		public override string Description => "Memory Leaks inspection";

		private readonly List<Thread> _threads = new List<Thread>();

		protected override void ExecuteInternal()
		{
			LeakedAttachedClass();

			Debugger.Break();

			var leakedPointerAddresses = LeakedPointers();
			CollectWithFinalizers();
			Console.WaitForContinue("Release leaked resources (sic!)");
			ReleaseLeakedResources(leakedPointerAddresses);
			CollectWithFinalizers();
			Console.WaitForContinue("Resources has been released");

			LeakedThreads();
			CollectWithFinalizers();
			Console.WaitForContinue("Threads has been created");
		}

		private static void LeakedAttachedClass()
		{
			var s = "Interned String";
			var wTable = new ConditionalWeakTable<string, Tuple<object, object>>();
			wTable.Add(s, new Tuple<object, object>(wTable, new AttachedClass()));
			string.Intern(s);
			s = null;
			wTable = null;

			CollectWithFinalizers();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static long[] LeakedPointers()
		{
			var pinnedHandles = new long[100];

			for (int i = 0; i < 100; i++)
			{
				var pinnedHandle = GCHandle.Alloc(new PinnedObject(), GCHandleType.Pinned);
				var pinnedObjAddress = GCHandle.ToIntPtr(pinnedHandle);
				pinnedHandles[i] = pinnedObjAddress.ToInt64() + 100;
			}

			return pinnedHandles;
		}

		private static void ReleaseLeakedResources(long[] leakedAddresses)
		{
			foreach (var addr in leakedAddresses)
			{
				var resurrectedHandle = new IntPtr(addr-100);
				var resurrectedPointer = GCHandle.FromIntPtr(resurrectedHandle);
				resurrectedPointer.Free();
			}
		}

		private void LeakedThreads()
		{
			var threads = Enumerable.Range(1, 10)
				.Select(i =>
				{
					var t = new Thread(_ => Console.Green($"I Am Thread #{Thread.CurrentThread.ManagedThreadId}"));
					t.Name = $"TestThread #{i}";
					t.Start();
					t.Join();

					return t;
				});
			_threads.AddRange(threads);
		}

		private static void CollectWithFinalizers()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private sealed class PinnedObject
		{
			~PinnedObject()
			{
				Console.Red("PinnedObject finalizer");
			}
		}

		private sealed class AttachedClass
		{
			private readonly byte[] _leakedArray;

			public AttachedClass()
			{
				_leakedArray = new byte[86_000];
			}

			~AttachedClass()
			{
				Console.Red("AttachedClass finalizer");
			}
		}
	}
}

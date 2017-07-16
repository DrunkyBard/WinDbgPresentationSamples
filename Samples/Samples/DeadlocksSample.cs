using System.Threading;
using Console = Samples.ConsoleUtility;

namespace Samples.Samples
{
	internal sealed class DeadlocksSample : Sample
	{
		public override SampleType Type => SampleType.Deadlocks;
		public override string Description => "Deadlocks";

		protected override void ExecuteInternal()
		{
			ThreadPool.QueueUserWorkItem(_ =>
			{
				Thread.Sleep(Timeout.Infinite);
			});
			ThreadPool.QueueUserWorkItem(_ =>
			{
				Thread.Sleep(Timeout.Infinite);
			});

			Thread CreateDeadlockedThread(EventWaitHandle ewh1, EventWaitHandle ewh2)
			{
				var thread = new Thread(_ =>
				{
					try
					{
						lock (ewh1)
						{
							WaitHandle.SignalAndWait(ewh1, ewh2);
							Console.Green($"Thread {Thread.CurrentThread.ManagedThreadId} has aquired first lock");

							lock (ewh2)
							{
								System.Console.WriteLine("We will never see this string");
							}
						}
					}
					catch (ThreadInterruptedException) { }
				});

				return thread;
			}

			var are1 = new EventWaitHandle(false, EventResetMode.AutoReset);
			var are2 = new EventWaitHandle(false, EventResetMode.AutoReset);

			var thread1 = CreateDeadlockedThread(are1, are2);
			var thread2 = CreateDeadlockedThread(are2, are1);

			thread1.Start();
			thread2.Start();

			Console.WaitForContinue("Interrupt both threads");
			thread1.Interrupt();
			thread2.Interrupt();
		}
	}
}

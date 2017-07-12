// ReSharper disable UnusedVariable

using System.Linq;
using System.Text;
using Console = Samples.ConsoleUtility;

namespace Samples.Samples
{
	internal class HeapAndStackInspectionSample : Sample
	{
		public override SampleType Type => SampleType.HeapAndStackInspection;
		public override string Description => "Heap & Stack inspection";

		protected override void ExecuteInternal()
		{
			var xParam = 1;
			var yParam = "Some string";
			var zParam = new[] {byte.MinValue, byte.MaxValue};
			var largeObject = new byte[86_000];
			var uselessVariable = new StringBuilder();
			Foo(xParam, yParam, zParam);
		}

		private static void Foo(int x, string y, byte[] z)
		{
			Console.Green($"INT: {x}");
			Console.Green($"STRING: {y}");
			Console.Green($"BYTE ARRAY: [{string.Join(",", z.Select(b => b.ToString()))}]");
		}
	}
}

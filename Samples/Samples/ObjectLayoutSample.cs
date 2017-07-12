// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
#pragma warning disable 414

using System;

namespace Samples.Samples
{
	internal sealed class ObjectLayoutSample : Sample
	{
		public override SampleType Type => SampleType.ObjectLayout;
		public override string Description => "Object memory layout";

		protected override void ExecuteInternal()
		{
			var fooClassInstance = new FooClass();

			Console.WriteLine("Foo class instance created. Press ENTER to continue...");
			Console.ReadLine();

			GC.KeepAlive(fooClassInstance);
		}

		private sealed class FooClass
		{
			public readonly string StringField;
			public readonly BarStruct StructField;
			public readonly byte[] ByteArray;

			public FooClass()
			{
				StringField = "FooClassStringField";
				StructField = new BarStruct(1337, "BarStructStringField");
				ByteArray = new byte[] {1, 2, 3};
			}
		}

		private struct BarStruct
		{
			public readonly int I;
			public readonly string S;

			public BarStruct(int i, string s)
			{
				I = i;
				S = s;
			}
		}
	}
}

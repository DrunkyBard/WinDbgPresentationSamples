// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
#pragma warning disable 414

using System;

namespace Samples.Samples
{
	internal sealed class ObjectInspectionSample : Sample
	{
		public override SampleType Type => SampleType.ObjectInspection;
		public override string Description => "Object inspection";

		protected override void ExecuteInternal()
		{
			var fooClassInstance = new FooClass();
			var barClassInstance = new BarClass();

			Console.WriteLine("Foo class instance created. Press ENTER to continue...");
			Console.ReadLine();

			GC.KeepAlive(fooClassInstance);
			GC.KeepAlive(barClassInstance);
		}

		private sealed class BarClass : ISomeGenericInterface<int>
		{
			public void Test(int input)
			{ }
		}

		private sealed class FooClass : AbstractClass, IInterface, ISomeGenericInterface<string>
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

			public override void AbstractMethod()
			{ }

			public void A()
			{ }

			public int B(int i) => i;

			public void Test(string input)
			{ }
		}

		private interface IInterface
		{
			void A();
			int B(int i);
		}

		private interface ISomeGenericInterface<in T>
		{
			void Test(T input);
		}

		private abstract class AbstractClass
		{
			public abstract void AbstractMethod();
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

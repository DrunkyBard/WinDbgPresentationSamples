// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
#pragma warning disable 414

using System;
using System.Collections.Generic;

namespace Samples.Samples
{
	internal sealed class ObjectInspectionSample : Sample
	{
		public override SampleType Type => SampleType.ObjectInspection;
		public override string Description => "Object inspection";

		protected override void ExecuteInternal()
		{
			var fooClassInstance = new FooClass();

			Console.WriteLine("Foo class instance created. Press ENTER to continue...");
			Console.ReadLine();

			GC.KeepAlive(fooClassInstance);
		}

		private sealed class FooClass : AbstractClass, IInterface, ISomeGenericInterface<string>
		{
			public readonly string StringField;
			public readonly BarStruct StructField;
			public readonly byte[] ByteArray;
			public readonly Dictionary<int, string> Dictionary;
			public readonly List<string> List;

			public FooClass()
			{
				StringField = "FooClassStringField";
				StructField = new BarStruct(1337, "BarStructStringField");
				ByteArray = new byte[] {1, 2, 3};
				Dictionary = new Dictionary<int, string>
				{
					{ 1, "First" },
					{ 2, "Second" },
					{ 3, "Third" }
				};
				List = new List<string>{"First", "Second", "Third"};
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

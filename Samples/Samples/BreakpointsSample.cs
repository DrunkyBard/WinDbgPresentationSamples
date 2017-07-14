// ReSharper disable UnusedTypeParameter
#pragma warning disable 219

using System.Runtime.CompilerServices;

namespace Samples.Samples
{
	internal sealed class BreakpointsSample : Sample
	{
		public override SampleType Type => SampleType.Breakpoints;
		public override string Description => "Breakpoints";

		protected override void ExecuteInternal()
		{
			FirstMethod();

			var generic1 = new SomeGenericClass<int>();
			var generic2 = new SomeGenericClass<string>();
			var generic3 = new SomeGenericClass<object>();
			
			generic1.Foo();
			generic2.Foo();

			generic1.SomeGenericMethod<int>();
			generic1.SomeGenericMethod<string>();
			generic2.SomeGenericMethod<object>();
			generic2.SomeGenericMethod<string>();
			generic3.SomeGenericMethod<string>();

			var nonGeneric = new SomeClassWithGenericMethod();
			nonGeneric.Foo<int>();
			nonGeneric.Foo<string>();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void FirstMethod()
		{
			var a = 1;
		}

		private sealed class SomeGenericClass<TX>
		{
			public void Foo()
			{
				var a = 1;
			}

			public void SomeGenericMethod<TY>()
			{
				var a = default(TY);
			}
		}

		private sealed class SomeClassWithGenericMethod
		{
			public void Foo<T>()
			{
				var a = default(T);
			}
		}
	}
}

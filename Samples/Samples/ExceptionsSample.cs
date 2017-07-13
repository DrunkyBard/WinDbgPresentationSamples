// ReSharper disable RedundantCatchClause
#pragma warning disable 168

using System;

namespace Samples.Samples
{
	internal sealed class ExceptionsSample : Sample
	{
		public override SampleType Type => SampleType.Exceptions;
		public override string Description => "Exceptions";

		protected override void ExecuteInternal()
		{
			try
			{
				throw new Exception("This exception will be handled");
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}

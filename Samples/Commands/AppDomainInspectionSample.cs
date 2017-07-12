﻿// ReSharper disable UnusedVariable

using System;
using System.Diagnostics;
using Console = Samples.ConsoleUtility;

namespace Samples.Commands
{
	internal sealed class AppDomainInspectionSample : Sample
	{
		public override SampleType Type => SampleType.AppDomainInspection;
		public override string Description => "Application Domain inspection";

		protected override void ExecuteInternal()
		{
			var testDomain = AppDomain.CreateDomain("TestAppDomain");
			var crossDomainInstance = testDomain.CreateInstanceAndUnwrap(typeof(CrossDomainObject).Assembly.FullName, typeof(CrossDomainObject).FullName);
			
			Console.Green("Cross domain instance created");
			Debugger.Break();
			AppDomain.Unload(testDomain);
		}
	}

	[Serializable]
	public sealed class CrossDomainObject { }
}
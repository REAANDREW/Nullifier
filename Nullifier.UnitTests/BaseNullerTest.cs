
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public abstract class BaseNullerTest
	{
		[TestFixtureSetUp]
		public void Initialize(){
			Nuller.InitializeWithDefaultFinders();
		}
		
		[TestFixtureTearDown]
		public void CleanUp(){
			Nuller.Initialize();
		}
	}
}

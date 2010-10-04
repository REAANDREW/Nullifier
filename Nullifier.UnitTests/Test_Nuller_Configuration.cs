
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class Test_Nuller_Configuration
	{
		[Test]
		public void Can_initialize_with_default_finders(){
			Nuller.InitializeWithDefaultFinders();
			
			Assert.IsAssignableFrom(typeof(IDictionaryChildrenFinder),Nuller.Finders[0]);
			Assert.IsAssignableFrom(typeof(GenericEnumerableChildrenFinder),Nuller.Finders[1]);
			Assert.IsAssignableFrom(typeof(ArrayChildrenFinder),Nuller.Finders[2]);
			Assert.IsAssignableFrom(typeof(IEnumerableChildrenFinder),Nuller.Finders[3]);
			Assert.IsAssignableFrom(typeof(PrimitiveChildrenFinder),Nuller.Finders[4]);
			Assert.IsAssignableFrom(typeof(StringChildrenFinder),Nuller.Finders[5]);
			Assert.IsAssignableFrom(typeof(DefaultChildrenFinder),Nuller.Finders[6]);
		}
		
		[Test]
		public void Can_initialize_with_supplied_finders(){
			Nuller.Initialize();
			
			Assert.AreEqual(0,Nuller.Finders.Count);
		}
	}
}

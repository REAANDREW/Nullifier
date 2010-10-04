
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_exclude_root_property : BaseNullerTest
	{	
		private static FakeObject FakeContext;
		
		[SetUp]
		public void Setup()
		{
			FakeContext = new FakeObject();
			When_exlude_FakeProperty1();
		}
		
		public void When_exlude_FakeProperty1()
		{
			FakeContext = Nuller.Exclude<FakeObject>(FakeContext, new NullerInstruction("FakeProperty1"));
		}
		
		[Test]
		public void Then_FakeProperty1_is_null()
		{
			Assert.IsNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_FakeProperty2_is_not_null(){
			Assert.IsNotNull(FakeContext.FakeProperty2);
		}
		
		[Test]
		public void Then_ChildObjects_is_not_null(){
			Assert.IsNotNull(FakeContext.ChildObjects);
		}
	}
}

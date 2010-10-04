
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_include_root_property : BaseNullerTest
	{
		private static FakeObject FakeContext;
		
		[SetUp]
		public void SetUp(){
			FakeContext = new FakeObject();
			When_include_FakeProperty1();
		}
		
		public void When_include_FakeProperty1(){
			FakeContext = Nuller.Include<FakeObject>(FakeContext, new NullerInstruction("FakeProperty1"));
		}
		
		[Test]
		public void Then_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_FakeProperty2_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty2);
		}
		
		[Test]
		public void Then_ChildObjects_should_be_null(){
			Assert.IsNull(FakeContext.ChildObjects);
		}
	}
}

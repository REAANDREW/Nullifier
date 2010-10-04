
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_exclude_nested_public_field : BaseNullerTest
	{
		private static FakeObject FakeContext;
		
		[SetUp]
		public void SetUp(){
			FakeContext = new FakeObject();
			When_exclude_FakeField1();
		}
		
		public void When_exclude_FakeField1(){
			FakeContext.FakeField1 = "FooBar";
			Nuller.Exclude<FakeObject>(FakeContext,"FakeField1");
		}
		
		[Test]
		public void Then_FakeField1_should_be_null(){
			Assert.IsNull(FakeContext.FakeField1);
		}
	}
}

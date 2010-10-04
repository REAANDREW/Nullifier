
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_include_multiple_level_properties : BaseNullerTest
	{
		private static FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_include_FakeProperty1_and_FakeProperty2();
		}
		
		public void When_include_FakeProperty1_and_FakeProperty2(){
			FakeContext = Nuller.Include<FakeObject>(FakeContext, new NullerInstruction{
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("FakeProperty1"),	
					new NullerInstruction("FakeProperty2")	
				}
			});
		}
		
		[Test]
		public void Then_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_FakeProperty2_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty2);
		}
		
		[Test]
		public void Then_ChildObjects_will_be_null(){
			Assert.IsNull(FakeContext.ChildObjects);
		}
	}
}

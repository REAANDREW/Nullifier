
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_exclude_multilple_level_properties : BaseNullerTest
	{

		private static FakeObject FakeContext;
		
		[SetUp]
		public void SetUp(){
			FakeContext = new FakeObject();
			When_exclude_FakeProperty1_and_FakeProperty2();
		}
		
		public void When_exclude_FakeProperty1_and_FakeProperty2(){
			FakeContext = Nuller.Exclude<FakeObject>(FakeContext, new NullerInstruction{
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("FakeProperty1"),	
					new NullerInstruction("FakeProperty2")	
				}
			});
		}
		
		[Test]
		public void Then_FakeProperty1_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_FakeProperty2_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty2);
		}
	}
}

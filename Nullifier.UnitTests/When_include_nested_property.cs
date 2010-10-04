
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_include_nested_property : BaseNullerTest
	{
		private FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_include_Name_of_FakeProperty1();
		}
		
		public void When_include_Name_of_FakeProperty1(){
			FakeContext = Nuller.Include<FakeObject>(FakeContext,new NullerInstruction("FakeProperty1"){
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("Name")
				}
			});
		}
		
		[Test]
		public void Then_Name_of_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1.Name);
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

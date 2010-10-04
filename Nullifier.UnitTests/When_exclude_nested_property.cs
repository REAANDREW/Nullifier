
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_exclude_nested_property : BaseNullerTest
	{
		private static FakeObject FakeContext;
	
		[SetUp]
		public void SetUp(){
			FakeContext = new FakeObject();
			When_exclude_Name_of_FakeProperty1();
		}
		
		public void When_exclude_Name_of_FakeProperty1(){
			FakeContext = Nuller.Exclude<FakeObject>(FakeContext, new NullerInstruction("FakeProperty1"){
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("Name")	
				}
			});
		}
		
		[Test]
		public void Then_FakeProperty1_name_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty1.Name);
		}
		
		[Test]
		public void Then_FakeProperty2_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty2);	
		}
		
		[Test]
		public void Then_ChildObjects_should_not_be_null(){
			Assert.IsNotNull(FakeContext.ChildObjects);
		}
	}
}

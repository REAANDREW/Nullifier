
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_include_root_property_and_nested_property : BaseNullerTest
	{
		private FakeObject FakeContext; 
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_include_FakeProperty2_and_Name_of_FakeProperty1();
		}
		
		public void When_include_FakeProperty2_and_Name_of_FakeProperty1(){
			FakeContext = Nuller.Include<FakeObject>(FakeContext, new NullerInstruction{
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("FakeProperty1"){
						ChildProperties = new List<NullerInstruction>{
							new NullerInstruction("Name")	
						}
					},
					new NullerInstruction("FakeProperty2")	
				}
			});
		}
		
		[Test]
		public void Then_FakeProperty2_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty2);
		}
		
		[Test]
		public void Then_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_Name_Of_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1.Name);
		}
		
		[Test]
		public void Then_ChildObjects_Should_be_null(){
			Assert.IsNull(FakeContext.ChildObjects);	
		}
		
	}
}


using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_exclude_root_property_and_nested_property : BaseNullerTest
	{
		private static FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_exclude_FakeProperty2_and_Name_Of_FakeProperty1();
		}
		
		
		public void When_exclude_FakeProperty2_and_Name_Of_FakeProperty1(){
			FakeContext = Nuller.Exclude<FakeObject>(FakeContext, new NullerInstruction{
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
		public void Then_FakeProperty1_should_not_be_null(){
			Assert.IsNotNull(FakeContext.FakeProperty1);
		}
		
		[Test]
		public void Then_FakeProperty2_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty2);
		}
		
		[Test]
		public void Then_FakeProperty1_Name_should_be_null(){
			Assert.IsNull(FakeContext.FakeProperty1.Name);
		}
		
		[Test]
		public void Then_ChildObjects_should_not_be_null(){
			Assert.IsNotNull(FakeContext.ChildObjects);
		}
	}
}

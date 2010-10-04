
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_include_nested_property_of_list_property : BaseNullerTest
	{
		private static FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_include_every_FakeProperty1_Property_In_Each_ChildObject();
		}
		
		public void When_include_every_FakeProperty1_Property_In_Each_ChildObject(){
			
			for(var i = 0 ; i < 10; i++){
				FakeContext.ChildObjects.Add(new FakeObject());
			}
			
			FakeContext = Nuller.Include<FakeObject>(FakeContext, new NullerInstruction("ChildObjects"){
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("FakeObject"){
						ChildProperties = new List<NullerInstruction>{
							new NullerInstruction("FakeProperty1")			
						}
					}
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
		
		[Test]
		public void Then_Every_FakeProperty1_Property_In_Each_ChildObject_should_not_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNotNull(childObject.FakeProperty1);	
			}	
		}
		
		[Test]
		public void Then_Every_FakeProperty2_Property_In_Each_ChildObject_should_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNull(childObject.FakeProperty2);	
			}
		}
		
		[Test]
		public void Then_Every_ChildObjects_Property_In_Each_ChildObject_should_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNull(childObject.ChildObjects);	
			}		
		}
	}
}

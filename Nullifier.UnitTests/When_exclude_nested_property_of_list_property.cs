
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_exclude_nested_property_of_list_property : BaseNullerTest
	{
		private FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			When_exclude_every_FakeProperty1_Property_In_Each_ChildObject();
		}
		
		public void When_exclude_every_FakeProperty1_Property_In_Each_ChildObject(){
			for(var i = 0 ; i < 10; i++){
				FakeContext.ChildObjects.Add(new FakeObject());
			}
			//FakeContext.ChildObjects.Add(FakeContext);
			
			FakeContext = Nuller.Exclude<FakeObject>(FakeContext,new NullerInstruction("ChildObjects"){
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
		public void Then_every_FakeProperty1_property_of_each_FakeObject_inside_ChildObjects_should_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNull(childObject.FakeProperty1);
			}
		}
		
		[Test]
		public void Then_every_FakeProperty2_property_of_each_FakeObject_inside_ChildObjects_should_not_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNotNull(childObject.FakeProperty2);
			}
		}
		
		[Test]
		public void Then_every_ChildObjects_property_of_each_FakeObject_inside_ChildObjects_should_not_be_null(){
			foreach(var childObject in FakeContext.ChildObjects){
				Assert.IsNotNull(childObject.ChildObjects);
			}
		}
	}
}

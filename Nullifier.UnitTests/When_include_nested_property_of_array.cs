
using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_include_nested_property_of_array : BaseNullerTest
	{

		private static FakeObject FakeContext;
		
		[SetUp]
		public void Setup(){
			FakeContext = new FakeObject();
			List<FakeSecondObject> list = new List<FakeSecondObject>();
			for(var i = 0 ; i < 10; i++){
				list.Add(new FakeSecondObject{Name = "ArrayObject"+i});	
			}
			FakeContext.FakeObjectsOfArray = list.ToArray();
			When_include_Name_of_Each_FakeSecondProperty_In_Array();
		}
		
		public void When_include_Name_of_Each_FakeSecondProperty_In_Array(){
			FakeContext = Nuller.Include<FakeObject>(FakeContext, new NullerInstruction("FakeObjectsOfArray"){
			ChildProperties = new List<NullerInstruction>{
					new NullerInstruction("FakeSecondObject"){
						ChildProperties = new List<NullerInstruction>{
							new NullerInstruction("Name")			
						}
					}
				}
			});
		}
		
				[Test]
		public void Then_every_name_value_of_each_FakeSecondProperty_object_in_array_should_not_be_null(){
			foreach(var item in FakeContext.FakeObjectsOfArray){
				Assert.IsNotNull(item.Name);	
			}
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
		public void Then_ChildObjects_should_be_null(){
			Assert.IsNull(FakeContext.ChildObjects);
		}
		
	}
}

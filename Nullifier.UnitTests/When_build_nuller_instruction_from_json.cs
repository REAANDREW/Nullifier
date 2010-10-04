
using System;
using System.Runtime.Serialization.Json;
using System.Xml;
using NUnit.Framework;

namespace Nullifier.UnitTests
{
	[TestFixture]
	public class When_build_nuller_instruction_from_json : BaseNullerTest
	{
		private static string FakeJsonString;
		private static NullerInstruction FakeInstruction;
		
		[SetUp]
		public void Setup(){
			FakeJsonString = @"{
				PropertyName : 'FakeProperty1'
			}";
			When_deserialize_json_object_to_NullerInstruction();
		}
		
		public void When_deserialize_json_object_to_NullerInstruction(){
			
		}
		
		[Test]
		public void Then_NullerInstruction_should_not_be_null(){
			
		}
		
		[Test]
		public void Then_NullerInstruction_should_have_a_PropertyName_FakeProperty1(){
			
		}
	}
}

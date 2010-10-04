
using System;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_getting_values_from_NullerProperty : BaseNullerTest
	{

		private static NullerInstruction FakePropertyContext;
		
		[SetUp]
		public void SetUp(){
			FakePropertyContext = new NullerInstruction("FakeRoot");
			When_using_a_three_level_deep_NullerProperty();
		}
		
		public void When_using_a_three_level_deep_NullerProperty(){
			var localNullerProperty = FakePropertyContext;
			for(int i = 0 ; i < 10;i++){
				var newNullerProperty = new NullerInstruction("FakeProperty"+i.ToString());
				localNullerProperty.ChildProperties.Add(newNullerProperty);
				localNullerProperty = newNullerProperty;
			}
		}
		
		[Test]
		public void Then_calling_ToString_will_join_all_nested_properties_with_fullstop(){
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder.Append("FakeRoot");
			for(var i = 0 ; i < 10;i++){
				builder.Append(".FakeProperty"+i.ToString());
			}
			
			var output = FakePropertyContext.Transform(new DotNotationTransformer())[0];
			
			Console.WriteLine(builder.ToString());
			Console.WriteLine(output);
			
			Assert.AreEqual(builder.ToString(), output);
		}
	}
}

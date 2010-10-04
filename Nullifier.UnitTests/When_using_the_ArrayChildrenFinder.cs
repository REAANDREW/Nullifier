
using System;
using System.Linq;
using NUnit.Framework;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class When_using_the_ArrayChildrenFinder
	{
		private ArrayChildrenFinder _childrenFinder;
		
		[SetUp]
		public void Setup(){
			_childrenFinder = new ArrayChildrenFinder();
		}
		
		[Test]
		public void Then_it_should_not_handle_the_array_if_the_array_type_is_primitive(){
			var array = new int[]{};
			var result = _childrenFinder.CanGetChildren(array);
			Assert.IsFalse(result);
		}
		
		[Test]
		public void Then_it_should_not_handle_the_array_if_the_array_type_is_a_string(){
			var array = new string[]{};
			var result = _childrenFinder.CanGetChildren(array);
			Assert.IsFalse(result);
		}
		
		[Test]
		public void Then_it_should_not_handle_the_array_if_the_array_type_is_an_object(){
			var array = new object[]{};
			var result = _childrenFinder.CanGetChildren(array);
			Assert.IsFalse(result);
		}
		
		[Test]
		public void Then_it_should_handle_the_array_if_the_array_is_not_an_object_not_a_string_not_a_primitive(){
			var array = new FakeObject[]{};
			var result = _childrenFinder.CanGetChildren(array);
			Assert.IsTrue(result);
		}
		
		[Test]
		public void Then_it_should_not_return_the_children_if_supplying_a_populated_object_array(){
			var array = new object[]{1,"string","something"};
			var results = _childrenFinder.GetChildrenProperties(array);
			Assert.AreEqual(0,results.Count());
		}
		
		[Test]
		public void Then_it_should_return_the_correct_properties(){
			var array = new SimpleFakeObject[]{
				new SimpleFakeObject{Property1="Test1"},
				new SimpleFakeObject{Property1="Test2"}
			};
			var results = _childrenFinder.GetChildrenProperties(array);
			Assert.AreEqual(2,results.Count());
		}
	}
}

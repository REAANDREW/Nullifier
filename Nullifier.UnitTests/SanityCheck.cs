
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Collections;

namespace Nullifier.UnitTests
{

	[TestFixture]
	public class SanityCheck : BaseNullerTest
	{
		[Test]
		public void Nuller_can_return_the_type_passed_into_it_for_include ()
		{
			var testClass = new SomeClass ();
			var instruction = new NullerInstruction ("FakeProp1");
			var processedClass = Nuller.Include<SomeClass> (testClass, instruction);
			Assert.IsAssignableFrom (typeof(SomeClass), processedClass);
		}

		[Test]
		public void Is_string_a_String(){
			string s = "string";
			Assert.AreEqual(typeof(String),s.GetType());
		}
		
		[Test]
		public void Nuller_can_return_the_type_passed_into_it_for_exclude ()
		{
			var testClass = new SomeClass ();
			var instruction = new NullerInstruction ("FakeProp1");
			var processedClass = Nuller.Exclude<SomeClass> (testClass, instruction);
			Assert.IsAssignableFrom (typeof(SomeClass), processedClass);
		}

		[Test]
		public void Nuller_exclude_takes_a_NullerInstruction ()
		{
			;
			var target = new SomeClass ();
			var instruction = new NullerInstruction ("FakeProp1");
			var result = Nuller.Exclude<SomeClass> (target, instruction);
		}

		[Test]
		public void Exclude_can_target_root_properties ()
		{
			var child = new SomeClass { FakeProp1 = "string4", FakeProp2 = "string5", FakeProp3 = "string6" };
			
			var target = new SomeClass { FakeProp1 = "string1", FakeProp2 = "string2", FakeProp3 = "string3", FakeProp4 = child };
			
			var instruction = new NullerInstruction { ChildProperties = new List<NullerInstruction> { new NullerInstruction ("FakeProp4") } };
			
			var result = Nuller.Exclude<SomeClass> (target, instruction);
			
			Assert.IsNull (target.FakeProp4);
			
		}

		[Test]
		public void Exclude_can_process_IEnumerable ()
		{
			var target = new SomeClass { ListProp = new List<SomeClass> { new SomeClass { FakeProp1 = "F11", FakeProp2 = "F21" }, new SomeClass { FakeProp1 = "F21", FakeProp2 = "F22" }, new SomeClass { FakeProp1 = "F31", FakeProp2 = "F32" }, new SomeClass { FakeProp1 = "F41", FakeProp2 = "F42" } } };
			var instruction = new NullerInstruction ("ListProp") { 
				ChildProperties = new List<NullerInstruction> { 
					new NullerInstruction ("SomeClass") {
						ChildProperties = new List<NullerInstruction>{
							new NullerInstruction("FakeProp2")	
						}
					}
				} 
			};
			
			var result = Nuller.Exclude<SomeClass> (target, instruction);
			
			foreach (var r in result.ListProp) {
				Assert.IsNull (r.FakeProp2);
			}
		}
		
		[Test]
		public void Exclude_can_process_IEnumerable_Dot ()
		{
			var target = new SomeClass { ListProp = new List<SomeClass> { new SomeClass { FakeProp1 = "F11", FakeProp2 = "F21" }, new SomeClass { FakeProp1 = "F21", FakeProp2 = "F22" }, new SomeClass { FakeProp1 = "F31", FakeProp2 = "F32" }, new SomeClass { FakeProp1 = "F41", FakeProp2 = "F42" } } };
			var instruction = "ListProp.SomeClass.FakeProp2";
			
			var result = Nuller.Exclude<SomeClass> (target, instruction);
			
			foreach (var r in result.ListProp) {
				Assert.IsNull (r.FakeProp2);
			}
		}
		
		[Test]
		public void Nuller_can_process_object_by_reference(){
			var someClass = new SomeClass{FakeProp1="Test"};
			var processor = new TestProcessor();
			processor.Process(someClass);
			Assert.IsNull(someClass.FakeProp1);
		}
		

		public class SomeClass
		{
			public string FakeProp1 { get; set; }
			public string FakeProp2 { get; set; }
			public string FakeProp3 { get; set; }
			public SomeClass FakeProp4 { get; set; }
			public List<SomeClass> ListProp { get; set; }
		}
		
		public class TestProcessor{
			public void Process(SomeClass someClass){
				Nuller.Exclude(someClass,"FakeProp1");
			}
		}

		public static IEnumerable<object> Test (object o)
		{
			Console.WriteLine ("Test 123");
			yield return 1;
		}
	}
}

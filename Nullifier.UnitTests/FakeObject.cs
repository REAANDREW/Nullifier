
using System;
using System.Collections.Generic;

namespace Nullifier.UnitTests
{
	public class FakeObject
	{
		public FakeObject ()
		{
			ChildObjects = new List<FakeObject> ();
			
			this.FakeProperty1 = new FakeSecondObject ();
			
			this.FakeProperty2 = new FakeThirdObject ();
			
			this.DictionaryProperty = new Dictionary<int, string> { { 1, "en" } };
			
			this.ConstructorType = new TypeWithConstructor (1);
			
			this.TheProperty = "Something";
			
			this.Strings = new List<string>{
				"String1",
				"String2"
			};
			
			this.FakeField1 = "Test";
		}
		
		public String FakeField1;

		public String TheProperty{get;set;}
		
		public List<FakeObject> ChildObjects { get; set; }

		public FakeSecondObject FakeProperty1 { get; set; }

		public FakeThirdObject FakeProperty2 { get; set; }

		public FakeSecondObject[] FakeObjectsOfArray { get; set;}
		
		public Dictionary<int, string> DictionaryProperty { get; set; }

		public TypeWithConstructor ConstructorType { get; set; }
		
		public List<string> Strings {
			get;
			set;
		}
	}

	public class TypeWithConstructor
	{
		private int _typeid;

		public TypeWithConstructor (int typeid)
		{
			_typeid = typeid;
		}

		public int TypeId {
			get { return _typeid; }
			set { _typeid = value; }
		}
	}

	public class FakeSecondObject
	{
		public FakeSecondObject ()
		{
			Name = Guid.NewGuid ().ToString ();
			Value = 1001;
		}

		public String Name { get; set; }
		public int Value { get; set; }
	}

	public class FakeThirdObject
	{
		public FakeThirdObject ()
		{
			FakeThirdObjectProperty = new FakeSecondObject ();
		}

		public FakeSecondObject FakeThirdObjectProperty { get; set; }
	}

	public class SimpleFakeObject
	{
		public string Property1 { get; set; }
	}
}

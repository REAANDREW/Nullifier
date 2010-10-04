
using System;
using System.Reflection;

namespace Nullifier
{
	public abstract class ExtendedChildInfo
	{
		public object Target { get; set; }
		public abstract void SetValue (object obj, object value);
		public abstract object GetValue (object target);
		public abstract string Name{get;}
		public abstract string TypeName{get;}
	}

	public class ExtendedChildPropertyInfo : ExtendedChildInfo
	{
		public PropertyInfo PropertyInfo { get; set; }
		
		public override string Name {
			get {
				return PropertyInfo.Name;
			}
		}
		
		public override string TypeName {
			get {
				return PropertyInfo.PropertyType.Name;
			}
		}
		
		
		
		public override object GetValue (object target)
		{
			return PropertyInfo.GetValue(target,null);
		}

		public override void SetValue (object obj, object value)
		{
			PropertyInfo.SetValue(obj,value, null);
		}
	}

	public class ExtendedChildFieldInfo : ExtendedChildInfo
	{
		public FieldInfo FieldInfo { get; set; }
		
		public override string Name {
			get {
				return FieldInfo.Name;
			}
		}
		
		public override string TypeName {
			get {
				return FieldInfo.FieldType.Name;
			}
		}
		
		
		
		public override object GetValue (object target)
		{
			return FieldInfo.GetValue(target);
		}

		public override void SetValue (object obj, object value)
		{
			FieldInfo.SetValue(obj,value);
		}
	}
}

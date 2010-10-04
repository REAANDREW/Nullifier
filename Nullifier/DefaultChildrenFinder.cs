
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace Nullifier
{
	public class DefaultChildrenFinder : INullerChildrenFinder
	{
		#region INullerChildrenFinder implementation
		public bool CanGetChildren (object target)
		{
			return true;
		}

		public IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			foreach (var property in target.GetType ().GetProperties (BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)) {
				
				yield return new ExtendedChildPropertyInfo { PropertyInfo = property, Target = target };
			}
			
			foreach (var field in target.GetType ().GetFields (BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)) {
				yield return new ExtendedChildFieldInfo { FieldInfo = field, Target = target };
			}
		}

		public string GetDotNotationPreifx (object target, ExtendedChildInfo propertyInfo)
		{
			return propertyInfo.Name;
		}
		#endregion
		
	}
}

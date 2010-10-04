
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nullifier
{
	public class IEnumerableChildrenFinder : INullerChildrenFinder
	{
		#region INullerChildrenFinder implementation
		public bool CanGetChildren (object target)
		{
			return target != null &&
				(target.GetType () == typeof(IEnumerable) || 
				 target.GetType ().GetInterfaces ().Any (x => x == typeof(IEnumerable)));
		}
		
		public IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			foreach (var obj in ((IEnumerable)target)) {
				int index = 0;
				foreach (var nestedProp in obj.GetType ().GetProperties ()) {
					yield return new ExtendedChildPropertyInfo { PropertyInfo = nestedProp, Target = obj };
				}
			}
		}
		
		public string GetDotNotationPreifx(object target, ExtendedChildInfo propertyInfo){
			return string.Concat("Object",".",propertyInfo.Name);
		}
		
		#endregion
	}
}

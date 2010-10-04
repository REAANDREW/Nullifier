
using System;
using System.Linq;
using System.Collections;

namespace Nullifier
{
	public class ArrayChildrenFinder : INullerChildrenFinder
	{
		#region INullerChildrenFinder implementation
		
		public bool CanGetChildren (object target)
		{
			var targetType = target.GetType();
			
			return targetType.IsArray &&
				!(targetType.GetElementType().IsPrimitive) &&
				!(targetType.GetElementType() == typeof(string)) &&
				!(targetType.GetElementType() == typeof(object));
				
		}
		
		
		public System.Collections.Generic.IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			if(CanGetChildren(target)){
				var array = (Array)target;
				
				for(var i = 0; i < array.GetLength(0); i++){
					var childObject = array.GetValue(i);
					foreach (var nestedProp in childObject.GetType ().GetProperties ()) {
						yield return new ExtendedChildPropertyInfo { PropertyInfo = nestedProp, Target = childObject };
					}
				}
			}
		}
		
		
		public string GetDotNotationPreifx (object target, ExtendedChildInfo propertyInfo)
		{
			return string.Concat(target.GetType().Name.TrimEnd('[',']'),".",propertyInfo.Name);
		}
		
		#endregion
	}
}

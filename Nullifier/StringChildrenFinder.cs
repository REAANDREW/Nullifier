
using System;
using System.Collections.Generic;

namespace Nullifier
{
	public class StringChildrenFinder : INullerChildrenFinder
	{
		#region INullerChildrenFinder implementation
		public bool CanGetChildren (object target)
		{
			return target == null ? false : target.GetType () == typeof(string);
		}


		public IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			return new ExtendedChildInfo[] {  };
		}
		
		public string GetDotNotationPreifx(object target, ExtendedChildInfo propertyInfo){
			return propertyInfo.Name;
		}
		
		#endregion
	}
}

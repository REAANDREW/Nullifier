
using System;
using System.Linq;
using System.Collections;

namespace Nullifier
{


	public class IDictionaryChildrenFinder : INullerChildrenFinder
	{
		#region INullerChildrenFinder implementation
		public bool CanGetChildren (object target)
		{
			return target.GetType ().GetInterfaces ().Any (x => x == typeof(IDictionary));
		}
		
		
		public System.Collections.Generic.IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			return new ExtendedChildInfo[]{};
		}
		
		
		public string GetDotNotationPreifx (object target, ExtendedChildInfo propertyInfo)
		{
			return string.Empty;
		}
		
		#endregion
	}
}


using System;

namespace Nullifier
{


	public class GenericEnumerableChildrenFinder : INullerChildrenFinder
	{
		private IEnumerableChildrenFinder _innerFinder;

		public GenericEnumerableChildrenFinder ()
		{
			_innerFinder = new IEnumerableChildrenFinder ();
		}

		#region INullerChildrenFinder implementation
		public bool CanGetChildren (object target)
		{
			if (!target.GetType ().IsGenericType)
				return false;
			
			return _innerFinder.CanGetChildren (target);
		}


		public System.Collections.Generic.IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target)
		{
			var genericArgument = target.GetType ().GetGenericArguments ()[0];
			
			if (!(genericArgument.IsPrimitive) && !(genericArgument == typeof(string)) && !(genericArgument == typeof(object))) {
				return _innerFinder.GetChildrenProperties (target);
			} else {
				return new ExtendedChildInfo[] {  };
			}
		}


		public string GetDotNotationPreifx (object target, ExtendedChildInfo propertyInfo)
		{
			var genericTypeName = target.GetType ().GetGenericArguments ()[0].Name;
			return string.Concat (genericTypeName, ".", propertyInfo.Name);
		}
		
		#endregion
		
	}
}

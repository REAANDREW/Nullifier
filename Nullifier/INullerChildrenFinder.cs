
using System;
using System.Collections.Generic;

namespace Nullifier
{
	public interface INullerChildrenFinder
	{
		bool CanGetChildren (object target);
		IEnumerable<ExtendedChildInfo> GetChildrenProperties (object target);
		string GetDotNotationPreifx(object target, ExtendedChildInfo propertyInfo);
	}
}

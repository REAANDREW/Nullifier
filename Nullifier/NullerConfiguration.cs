
using System;
using System.Collections.Generic;

namespace Nullifier
{


	public class NullerConfiguration
	{

		private Stack<INullerChildrenFinder> _finders;

		public NullerConfiguration ()
		{
			_finders = new Stack<INullerChildrenFinder> ();
		}

		public NullerConfiguration AddFinder (INullerChildrenFinder finder)
		{
			_finders.Push (finder);
			return this;
		}

		internal Stack<INullerChildrenFinder> Finders {
			get { return _finders; }
		}
	}
}

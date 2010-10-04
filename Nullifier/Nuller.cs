
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nullifier
{
	public class Nuller
	{
		private static NullerConfiguration _configuration;

		public static void InitializeWithDefaultFinders ()
		{
			_configuration = new NullerConfiguration ();
			_configuration.AddFinder (new DefaultChildrenFinder ());
			_configuration.AddFinder (new StringChildrenFinder ());
			_configuration.AddFinder (new PrimitiveChildrenFinder ());
			_configuration.AddFinder (new IEnumerableChildrenFinder ());
			_configuration.AddFinder (new ArrayChildrenFinder ());
			_configuration.AddFinder (new GenericEnumerableChildrenFinder ());
			_configuration.AddFinder (new IDictionaryChildrenFinder ());
		}

		public static NullerConfiguration Initialize ()
		{
			_configuration = new NullerConfiguration ();
			return _configuration;
		}

		public static IList<INullerChildrenFinder> Finders {
			get { return new List<INullerChildrenFinder> (_configuration.Finders.ToArray ()); }
		}

		public static TTarget Include<TTarget> (TTarget target, params string[] props)
		{
			
			var list = Execute (target);
			foreach (var nullerTarget in list) {
				if (!props.Contains (nullerTarget.Prefix) && !props.Any (x => x.StartsWith (nullerTarget.Prefix))) {
					nullerTarget.Child.SetValue (nullerTarget.Target, null);
				}
			}
			return target;
		}

		public static TTarget Include<TTarget> (TTarget target, NullerInstruction instruction)
		{
			
			var props = instruction.Transform (new DotNotationTransformer ());
			
			return Include (target, props);
		}

		public static TTarget Exclude<TTarget> (TTarget target, params string[] props)
		{
			//Evaluate the condition for the visitor
			
			var list = Execute (target);
			Console.WriteLine (String.Join (",", props));
			foreach (var nullerTarget in list) {
				Console.WriteLine ("List value {0}", nullerTarget.Prefix);
				if (props.Contains (nullerTarget.Prefix)) {
					nullerTarget.Child.SetValue (nullerTarget.Target, null);
				}
			}
			
			return target;
		}

		public static TTarget Exclude<TTarget> (TTarget target, NullerInstruction instruction)
		{
			
			var props = instruction.Transform (new DotNotationTransformer ());
			return Exclude (target, props);
			
		}

		protected static IList<NullerTarget> Execute<TTarget> (TTarget target)
		{
			var shallowCopyTarget = target;
			
			var returnList = new List<NullerTarget> ();
			var seen = new List<object> ();
			var stack = new Stack<NullerTarget> ();
			stack.Push (new NullerTarget { Prefix = string.Empty, Target = shallowCopyTarget });
			
			while (stack.Count > 0) {
				var current = stack.Pop ();
				var finder = GetChildrenFinder (current.Target);
				
				//Need to get the fields here too.
				var props = finder.GetChildrenProperties (current.Target);
				
				foreach (var childProperty in props) {
					try {
						//To get the object it might be better to put a method on the actual ExtendedPropertyInfo
						object childTarget = childProperty.GetValue(childProperty.Target);
						
						//object childTarget = childProperty.GetValue(childProperty.Target)
						//A child target type can be Property or Field to accomodate for public fields
						
						if (childTarget != null && !seen.Contains (childTarget)) {
							var finderPrefix = finder.GetDotNotationPreifx (current.Target, childProperty);
							var prefix = string.IsNullOrEmpty (current.Prefix) ? finderPrefix : string.Concat (current.Prefix, ".", finderPrefix);
							
							returnList.Add (new NullerTarget { Prefix = prefix, Target = childProperty.Target, Child = childProperty });
							stack.Push (new NullerTarget { Target = childTarget, Prefix = prefix });
							
							seen.Add (childTarget);
						}
					} catch (ArgumentException ex1) {
						throw new Exception (string.Format ("Cannot process {0} of {1} of {2}", childProperty.TypeName, childProperty.Name, current.Target.GetType ().Name), ex1);
					} catch (TargetParameterCountException ex2) {
						throw new Exception (string.Format ("Cannot process {0} of {1} of {2}", childProperty.TypeName, childProperty.Name, current.Target.GetType ().Name), ex2);
					}
				}
			}
			
			return returnList;
		}

		public static INullerChildrenFinder GetChildrenFinder (object target)
		{
			foreach (var finder in _configuration.Finders) {
				if (finder.CanGetChildren (target))
					return finder;
			}
			throw new ArgumentException ("No finder founder for that type of object", "target");
		}
	}
}

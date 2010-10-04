
using System;
using System.Text;
using System.Collections.Generic;

namespace Nullifier
{


	public class DotNotationTransformer : INullerInstructionTransformer
	{
		#region INullerPropertyTransformer implementation
		public string[] Transform (NullerInstruction instruction)
		{
			var returnList = new List<string>();
			
			if(instruction.ChildProperties.Count == 0)
			{
				return new string[]{instruction.PropertyName};
			}else{
				Console.WriteLine("Not returning null");
			}
			
			foreach(var childProp in instruction.ChildProperties){
				var list = new List<string>(Transform(childProp) ?? new string[]{});

				if(!String.IsNullOrEmpty(instruction.PropertyName))
					list.Insert(0,instruction.PropertyName);
				returnList.Add(String.Join(".",list.ToArray()));
			}
			
			return returnList.ToArray();
		}
		
		#endregion
	}
}

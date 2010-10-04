
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nullifier
{
	[DataContract]
	public class NullerInstruction
	{
		public NullerInstruction(){
			ChildProperties = new List<NullerInstruction>();	
		}
		
		public NullerInstruction (string propertyName) : this()
		{
			PropertyName = propertyName;
		}
		
		[DataMember]
		public IList<NullerInstruction> ChildProperties {
			get;
			set;
		}
		
		[DataMember]
		public string PropertyName {
			get;
			set;
		}
		
		public string[] Transform(INullerInstructionTransformer transformer){
			return transformer.Transform(this);
		}
	}
}

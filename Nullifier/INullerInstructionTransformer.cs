
using System;

namespace Nullifier
{
	public interface INullerInstructionTransformer
	{
		string[] Transform(NullerInstruction property);
	}
}

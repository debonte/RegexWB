using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
	class RegexAlternate: RegexItem
	{
		public RegexAlternate(RegexBuffer buffer)
		{
			buffer.AddLookup(this, buffer.Offset, buffer.Offset);

			buffer.MoveNext();		// skip "|"
		}

		public override string ToHumanReadableRepresentation(int offset)
		{
			return(new String(' ', offset) + "or");
		}		
	}
}

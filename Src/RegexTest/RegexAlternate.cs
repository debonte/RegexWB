using System;
using System.Text.RegularExpressions;

namespace RegexTest
{
	class RegexAlternate: RegexItem
	{
		public RegexAlternate(RegexBuffer buffer)
        {
            TryParse(buffer);
        }

        public override bool TryParse(RegexBuffer buffer)
        {
            buffer.AddLookup(this, buffer.Offset, buffer.Offset);

            buffer.MoveNext();      // skip "|"
            return true;
        }

        public override string ToString(int offset)
		{
			return(new String(' ', offset) + "or");
		}		
	}
}

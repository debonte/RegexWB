using RegexTest.Text;

namespace RegexTest
{
	internal class RegexAlternate: RegexItem
	{
		public RegexAlternate(RegexBuffer buffer)
		{
			buffer.AddLookup(this, buffer.Offset, buffer.Offset);

			buffer.MoveNext();		// skip "|"
		}

		public override string ToHumanReadableRepresentation(int offset)
		{
			return(Indent.By(offset) + "or");
		}
	}
}

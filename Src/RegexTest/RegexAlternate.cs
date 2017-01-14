using RegexTest.Text;

namespace RegexTest
{
	public class RegexAlternate: RegexItem
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

		public InterpretedInEnglish Interpret()
		{
			return new InterpretedInEnglish("or");
		}
	}
}

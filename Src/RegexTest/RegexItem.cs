using System;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexItem.
	/// </summary>
	public abstract class RegexItem
	{
		public abstract string ToString(int indent);

        public abstract bool TryParse(RegexBuffer buffer);
	}
}

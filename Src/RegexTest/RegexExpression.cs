using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using RegexTest.Text;

namespace RegexTest
{
	public class RegexExpression: RegexItem
	{
		public RegexExpression(RegexBuffer buffer)
		{
			Parse(buffer);
		}
	
		public ArrayList Items { get; } = new ArrayList();

		public override string ToHumanReadableRepresentation(int indent)
		{
			var result = new StringBuilder();
			var line = new StringBuilder();

			foreach (RegexItem item in Items)
			{
				var regexChar = item as RegexCharacter;
				if (regexChar != null && !regexChar.Special)
				{
					line.Append(regexChar.ToHumanReadableRepresentation(indent));
				}
				else
				{
					// add any buffered chars...
					if (line.Length != 0)
					{
						result.Append(Indent.By(indent));
						result.AppendLine(line.ToString());
						line = new StringBuilder();
					}
					result.Append(new string(' ', indent));
					var itemString = item.ToHumanReadableRepresentation(indent);
					if (itemString.Length != 0)
					{
						result.Append(itemString);
						var newLineAlready = new Regex(@"\r\n$");
						if (!newLineAlready.IsMatch(itemString))
						{
							result.Append("\r\n");
						}
					}
				}
			}
			if (line.Length != 0)
			{
				result.Append(Indent.By(indent));
				result.AppendLine(line.ToString());
			}
			return result.ToString();
		}
        
		// eat the whole comment until the end of line...
		private static void EatComment(RegexBuffer buffer)
		{
			while (buffer.Current != '\r')
			{
				buffer.MoveNext();
			}
		}

		void Parse(RegexBuffer buffer)
		{
			while (!buffer.AtEnd)
			{
					// if this regex ignores whitespace, we need to ignore these
				if (buffer.IgnorePatternWhitespace &&
					((buffer.Current == ' ') ||
					(buffer.Current == '\r') ||
					(buffer.Current == '\n') ||
					(buffer.Current == '\t')))
				{
					buffer.MoveNext();
				}
				else
				{
					switch (buffer.Current)
					{
						case '(':
							Items.Add(new RegexCapture(buffer));
							break;

						case ')':
							// end of closure; just return.
							return;

						case '[':
							Items.Add(new RegexCharClass(buffer));
							break;

						case '{':
							Items.Add(new RegexQuantifier(buffer));
							break;

						case '|':
							Items.Add(new RegexAlternate(buffer));
							break;

						case '\\':
							Items.Add(new RegexCharacter(buffer));
							break;

						case '#':
							if (buffer.IgnorePatternWhitespace)
							{
								EatComment(buffer);
							}
							else
							{
								Items.Add(new RegexCharacter(buffer));
							}
							break;

						default:
							Items.Add(new RegexCharacter(buffer));
							break;
					}
				}
			}
		}
	}
}

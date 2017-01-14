using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace RegexTest
{
	/// <summary>
	/// Summary description for RegexExpression.
	/// </summary>
	public class RegexExpression: RegexItem
	{
		ArrayList	items = new ArrayList();

		public RegexExpression(RegexBuffer buffer)
		{
			Parse(buffer);
		}
	
		public ArrayList Items
		{
			get
			{
				return items;
			}
		}

		public override string ToHumanReadableRepresentation(int indent)
		{
			var result = new StringBuilder();
			var line = new StringBuilder();

			foreach (RegexItem item in items)
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
						result.Append(new String(' ', indent));
						result.Append(line.ToString() + "\r\n");
						line = new StringBuilder();
					}
					result.Append(new String(' ', indent));
					string itemString = item.ToHumanReadableRepresentation(indent);
					if (itemString.Length != 0)
					{
						result.Append(itemString);
						Regex newLineAlready = new Regex(@"\r\n$");
						if (!newLineAlready.IsMatch(itemString))
						{
							result.Append("\r\n");
						}
					}
				}
			}
			if (line.Length != 0)
			{
				result.Append(new String(' ', indent));
				result.Append(line.ToString() + "\r\n");
			}
			return result.ToString();
		}
        
			// eat the whole comment until the end of line...
		void EatComment(RegexBuffer buffer)
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
							items.Add(new RegexCapture(buffer));
							break;

						case ')':
							// end of closure; just return.
							return;

						case '[':
							items.Add(new RegexCharClass(buffer));
							break;

						case '{':
							items.Add(new RegexQuantifier(buffer));
							break;

						case '|':
							items.Add(new RegexAlternate(buffer));
							break;

						case '\\':
							items.Add(new RegexCharacter(buffer));
							break;

						case '#':
							if (buffer.IgnorePatternWhitespace)
							{
								EatComment(buffer);
							}
							else
							{
								items.Add(new RegexCharacter(buffer));
							}
							break;

						default:
							items.Add(new RegexCharacter(buffer));
							break;
					}
				}
			}
		}
	}
}

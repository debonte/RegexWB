using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace RegexTest
{
	/// <summary>
	/// String with a pointer in it.
	/// </summary>
	public class RegexBuffer
	{
		string expression;
		int offset = 0;
		int errorLocation = -1;
		int errorLength = -1;
		RegexOptions regexOptions;
		bool inSeries = false;

		ArrayList expressionLookup = new ArrayList();

		public RegexBuffer(string expression)
		{
			this.expression = expression;
		}

		public char Current
		{
			get
			{
				if (AtEnd)
					throw new Exception("Beyond end of buffer");
				return expression[offset];
			}
		}

		public void MoveNext()
		{
			offset++;
		}

		public bool AtEnd
		{
			get
			{
                while (offset < expression.Length &&
                       IgnorePatternWhitespace &&
                       IsWhitespace(Current))
                {
                    MoveNext();
                    continue;
                }

                return offset >= expression.Length;
			}
		}

        private static bool IsWhitespace(char ch)
        {
            switch (ch)
            {
                case ' ':
                case '\r':
                case '\n':
                case '\t':
                    return true;
                default:
                    return false;
            }
        }

        public int Offset
		{
			get
			{
				return offset;
			}
			set
			{
				offset = value;
			}
		}

		public string String
		{
			get
			{
				return expression.Substring(offset);
			}
		}

		public RegexBuffer Substring(int start, int end)
		{
			return new RegexBuffer(expression.Substring(start, end - start + 1));
		}

		public int ErrorLocation
		{
			get
			{
				return errorLocation;
			}
			set
			{
				errorLocation = value;
			}
		}

		public int ErrorLength
		{
			get
			{
				return errorLength;
			}
			set
			{
				errorLength = value;
			}
		}

		public RegexOptions RegexOptions
		{
			get
			{
				return regexOptions;
			}
			set
			{
				regexOptions = value;
			}
		}

		public bool IgnorePatternWhitespace
		{
			get
			{
				return (regexOptions & RegexOptions.IgnorePatternWhitespace) != 0;
			}
		}

		public bool ExplicitCapture
		{
			get
			{
				return (regexOptions & RegexOptions.ExplicitCapture) != 0;
			}
		}

		public void AddLookup(RegexItem item, int startLocation, int endLocation)
		{
			AddLookup(item, startLocation, endLocation, false);
		}

		public void ClearInSeries()
		{
			inSeries = false;
		}

		public void AddLookup(RegexItem item, int startLocation, int endLocation, bool canCoalesce)
		{
			if (inSeries)
			{
					// in a series, add character to the previous one...
				if (canCoalesce)
				{
					RegexRef lastItem = (RegexRef) expressionLookup[expressionLookup.Count - 1];
					lastItem.StringValue += item.ToString(0);
					lastItem.Length += endLocation - startLocation + 1;
				}
				else
				{
					expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
					inSeries = false;
				}
			}
			else
			{
				if (canCoalesce)
				{
					inSeries = true;
				}
				expressionLookup.Add(new RegexRef(item, startLocation, endLocation));
			}
		}

		public RegexRef MatchLocations(int spot)
		{
			ArrayList locations = new ArrayList();
			foreach (RegexRef regexRef in expressionLookup)
			{
				if (regexRef.InRange(spot))
					locations.Add(regexRef);
			}
			locations.Sort();
			if (locations.Count != 0)
				return (RegexRef) locations[0];
			else
				return null;
		}
	}
}

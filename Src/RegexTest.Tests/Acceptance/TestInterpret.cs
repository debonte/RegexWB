using NUnit.Framework;
using RegexTest;

namespace RegexWorkbench.Tests
{
	[TestFixture]
	public class TestInterpret
	{
		private static string Interpret(string regex)
		{
			return new RegexExpression(new RegexBuffer(regex)).ToHumanReadableRepresentation(0);
		}

		[TestCase(@"Test", "Test\r\n", TestName="Normal Chars")]
		[TestCase(@"\a", "A bell (alarm) \\u0007 \r\n", TestName = "Character Shortcut a")]
		[TestCase(@"\t", "A tab \\u0009 \r\n", TestName = "Character Shortcut t")]
		[TestCase(@"\r", "A carriage return \\u000D \r\n", TestName = "Character Shortcut r")]
		[TestCase(@"\v", "A vertical tab \\u000B \r\n", TestName = "Character Shortcut v")]
		[TestCase(@"\f", "A form feed \\u000C \r\n", TestName = "Character Shortcut f")]
		[TestCase(@"\n", "A new line \\u000A \r\n", TestName = "Character Shortcut n")]
		[TestCase(@"\e", "An escape \\u001B \r\n", TestName = "Character Shortcut e")]
		[TestCase(@"\xFF", "Hex FF\r\n", TestName = "Character Shortcut x")]
		[TestCase(@"\cC", "CTRL-C\r\n", TestName = "Character Shortcut c")]
		[TestCase(@"\u1234", "Unicode 1234\r\n", TestName = "Character Shortcut u")]
		public void InterpretingTest(string inputString, string expectedString)
		{
			Assert.AreEqual(expectedString, Interpret(inputString));
		}

		[Test]
		public void TestCharacterGroup()
		{
			string output = Interpret("[abcdef]");
			Assert.AreEqual("Any character in \"abcdef\"\r\n", output);
		}

		[Test]
		public void TestCharacterGroupNegated()
		{
			string output = Interpret("[^abcdef]");
			Assert.AreEqual("Any character not in \"abcdef\"\r\n", output);
		}

		[Test]
		public void TestCharacterPeriod()
		{
			string output = Interpret(".");
			Assert.AreEqual(". (any character)\r\n", output);
		}

		[Test]
		public void TestCharacterWord()
		{
			string output = Interpret(@"\w");
			Assert.AreEqual("Any word character \r\n", output);
		}

		[Test]
		public void TestCharacterNonWord()
		{
			string output = Interpret(@"\W");
			Assert.AreEqual("Any non-word character \r\n", output);
		}
		
		[Test]
		public void TestCharacterWhitespace()
		{
			string output = Interpret(@"\s");
			Assert.AreEqual("Any whitespace character \r\n", output);
		}

		
		[Test]
		public void TestCharacterNonWhitespace()
		{
			string output = Interpret(@"\S");
			Assert.AreEqual("Any non-whitespace character \r\n", output);
		}
		
		[Test]
		public void TestCharacterDigit()
		{
			string output = Interpret(@"\d");
			Assert.AreEqual("Any digit \r\n", output);
		}
		
		[Test]
		public void TestCharacterNonDigit()
		{
			string output = Interpret(@"\D");
			Assert.AreEqual("Any non-digit \r\n", output);
		}

		[Test]
		public void TestQuantifierPlus()
		{
			string output = Interpret(@"+");
			Assert.AreEqual("+ (one or more times)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierStar()
		{
			string output = Interpret(@"*");
			Assert.AreEqual("* (zero or more times)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierQuestion()
		{
			string output = Interpret(@"?");
			Assert.AreEqual("? (zero or one time)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierFromNToM()
		{
			string output = Interpret(@"{1,2}");
			Assert.AreEqual("At least 1, but not more than 2 times\r\n", output);
		}
		
		[Test]
		public void TestQuantifierAtLeastN()
		{
			string output = Interpret(@"{5,}");
			Assert.AreEqual("At least 5 times\r\n", output);
		}		

		[Test]
		public void TestQuantifierExactlyN()
		{
			string output = Interpret(@"{12}");
			Assert.AreEqual("Exactly 12 times\r\n", output);
		}

		[Test]
		public void TestQuantifierPlusNonGreedy()
		{
			string output = Interpret(@"+?");
			Assert.AreEqual("+ (one or more times) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierStarNonGreedy()
		{
			string output = Interpret(@"*?");
			Assert.AreEqual("* (zero or more times) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierQuestionNonGreedy()
		{
			string output = Interpret(@"??");
			Assert.AreEqual("? (zero or one time) (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierFromNToMNonGreedy()
		{
			string output = Interpret(@"{1,2}?");
			Assert.AreEqual("At least 1, but not more than 2 times (non-greedy)\r\n", output);
		}
		
		[Test]
		public void TestQuantifierAtLeastNNonGreedy()
		{
			string output = Interpret(@"{5,}?");
			Assert.AreEqual("At least 5 times (non-greedy)\r\n", output);
		}		

		[Test]
		public void TestQuantifierExactlyNNonGreedy()
		{
			string output = Interpret(@"{12}?");
			Assert.AreEqual("Exactly 12 times (non-greedy)\r\n", output);
		}
	}
}

#if fred


= "Beginning of string - ^";
= "Beginning, multiline - \\A";
= "End of string - $";
= "End, multiline - \\Z";
= "End, multiline -  \\z";
= "Word boundary - \\b";
= "Non-word boundary - \\B";
= "Grouping";
= "Capture - (<exp>)";
= "Named capture - (?<<name>>x)";
= "Non-capture - (?:<exp>)";
= "Alternation - (<x>|<y>)";
= "Zero-Width";
= "Positive Lookahead - (?=<x>)";
= "Negative Lookahead - (?!<x>)";
= "Positive Lookbehind - (?<=<x>)";
= "Negative Lookbehind - (?<!<x>)";
= "Conditionals";
= "Expression - (?(<exp>)yes|no)";
= "Named - (?(<name>)yes|no)";
= "Options";
= "Ignore Case - (?i)";
= "Ignore Case off - (?-i)";
= "Multline - (?m)";
= "Multiline off - (?-m)";
= "Explicit Capture - (?c)";
= "Explicit Capture off - (?-c)";
= "Singleline - (?s)";
= "Singleline off - (?-s)";
= "Ignore Whitespace - (?x)";
= "Ignore Whitespace off - (?-x)";


#endif

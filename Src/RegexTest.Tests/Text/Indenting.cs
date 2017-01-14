using NUnit.Framework;
using RegexTest.Text;

namespace RegexWorkbench.Tests.Text
{
	public class Indenting
	{
		[Test]
		public void By0()
		{
			var indentString = Indent.By(0);
			Assert.AreEqual(string.Empty, indentString);
		}

		[Test]
		public void By1()
		{
			var indentString = Indent.By(1);
			Assert.AreEqual(" ", indentString);
		}

		[Test]
		public void By3()
		{
			var indentString = Indent.By(3);
			Assert.AreEqual("   ", indentString);
		}
	}
}

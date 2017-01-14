using NUnit.Framework;
using RegexTest;

namespace RegexWorkbench.Tests.RepresentationInEnglish
{
	public class InterpretingAlternate
	{
		[Test]
		public void OrOnSingleLine()
		{
			var element = new RegexAlternate(new RegexBuffer(null));

			var interpreted = element.Interpret();

			Assert.AreEqual("or", interpreted.Text);
			// Assert.IsFalse(interpreted.);
		}
	}
}

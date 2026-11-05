using AwesomeAssertions;

namespace DemoProject.Tests;

[TestClass]
public class AssertionLibrary
{
	[TestMethod]
	public void TestAwesomeAssertions()
	{
		var actual = 14;
		var expected = 14;
		Assert.AreEqual(expected, actual); // easy to mix up the order of actual and expected
		actual.Should().Be(expected); // unless you're Yoda, actual and expected are very natural

		// awesomeassertions is also great with asserting objects and collections
		var someObj = new { Age = 24 };
		var otherObj = new { Age = 24, Name = "JP" };
		someObj.Should().BeEquivalentTo(otherObj, opts => opts.Excluding(x => x.Name));
		// collections / objects
	}
}
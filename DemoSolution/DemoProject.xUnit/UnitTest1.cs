namespace DemoProject.xUnit;

public class UnitTest1
{
	[Theory]
	[InlineData(4, 8, 12)]
	public void Test1()
	{
		Assert.Equal(14, 14);
		Assert.Equal("hi there how are you", "hi htreuh oh my god");
	}
}
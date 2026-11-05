namespace DemoProject.Tests;

public class FakeNavigateService : INavigateService
{
	public bool HasNextBeenCalled { get; set; }
	public int Next<T>(List<T> data, int? currentIndex)
	{
		HasNextBeenCalled = true;
		return 42;
	}
}
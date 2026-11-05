namespace DemoProject;

public class NavigateService : INavigateService
{
	public int Next<T>(List<T> data, int? currentIndex)
	{
		currentIndex ??= -1;
		return (currentIndex.Value + 1) % data.Count;
	}
}
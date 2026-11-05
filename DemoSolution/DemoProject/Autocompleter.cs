namespace DemoProject;

public class Autocompleter<T>
{
	public IEnumerable<T> Data { get; set; } = null!;
	public string Query { get; set; } = string.Empty;
	public List<T>? Suggestions { get; set; }
	public int? ActiveSuggestionIndex { get; set; }

	private INavigateService _navigateService;
	public Autocompleter(INavigateService navigateService)
	{
		_navigateService = navigateService;
	}
	
	public void Autocomplete()
	{
		Suggestions = [];

		foreach (var item in Data)
		{
			// reflection: introspection on your code.
			// been in .NET 1.0   2002  generics 2005
			// Invoke("MethodName")
			// reflection is not looked upon as very fun.
			// "a necessary evil"
			var props = item.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
			foreach (var prop in props) // make model   
			{
				var value = prop.GetValue(item) as string; // item[make]   item[model]
				if (value.Contains(Query, StringComparison.OrdinalIgnoreCase))
				{
					Suggestions.Add(item);
					break;
				}
			}
		}
	}

	public void Next()
	{
		if (Suggestions is null) return;
		ActiveSuggestionIndex = _navigateService.Next(Suggestions, ActiveSuggestionIndex);
	}
	
}
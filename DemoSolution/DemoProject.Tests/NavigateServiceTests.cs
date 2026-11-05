using AwesomeAssertions;

namespace DemoProject.Tests;

[TestClass]
public class NavigateServiceTests
{
	private NavigateService _sut = null!;
	private List<string> _data = null!;
	
	[TestInitialize]
	public void Init()
	{
		_data = ["hi", "wow", "aha", "ok"];
		_sut = new NavigateService();
	}

	[TestMethod]
	public void Next_NothingHighlighted_HighlightFirstSuggestion()
	{
		// Act
		var result = _sut.Next(_data, null);
	
		// Assert
		result.Should().Be(0);
	}
	
	[TestMethod]
	public void Next_FirstSuggestionHighlighted_HighlightSecondSuggestion()
	{
		// Act
		var result = _sut.Next(_data, 0);
	
		// Assert
		result.Should().Be(1);
	}
	
	[TestMethod]
	public void Next_LastSuggestionHighlighted_HighlightFirstSuggestion()
	{
		// Act
		var result = _sut.Next(_data, _data.Count - 1);
	
		// Assert
		result.Should().Be(0);
	}
}
using AwesomeAssertions;

namespace DemoProject.Tests;

class Car
{
	public string Make { get; set; }
	public string Model { get; set; }
}

[TestClass]
public class AutocompleterTests
{
	private Autocompleter<Car> _sut = null!;
	private List<Car> _cars = null!;
	private FakeNavigateService _navigateServiceMock = null!;
		
	public AutocompleterTests()
	{
		_cars =
		[
			new() { Make = "Cupra", Model = "Born" },
			new() { Make = "Peugeot", Model = "208" },
			new() { Make = "Porsche", Model = "911" },
			new() { Make = "Lambo", Model = "Recuelti" },
			new() { Make = "Fiat", Model = "500" },
			new() { Make = "Ferrari", Model = "Testarossa" },
		];
		_navigateServiceMock = new FakeNavigateService(); // use mocking framework
		_sut = new(_navigateServiceMock);
		_sut.Data = _cars;
	}

	[TestMethod]
	public void Autocomplete_BasicAutocomplete_GiveSuggestions()
	{
		// Act
		_sut.Query = "i";
		_sut.Autocomplete();

		// Assert
		IEnumerable<Car> expected =
		[
			new() { Make = "Fiat", Model = "500" },
			new() { Make = "Lambo", Model = "Recuelti" },
			new() { Make = "Ferrari", Model = "Testarossa" },
		];
		_sut.Suggestions.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void Autocomplete_CapitalQuery_GiveSuggestionsCaseInsensitively()
	{
		// Act
		_sut.Query = "C";
		_sut.Autocomplete();

		// Assert
		IEnumerable<Car> expected =
		[
			new() { Make = "Cupra", Model = "Born" },
			new() { Make = "Lambo", Model = "Recuelti" },
			new() { Make = "Porsche", Model = "911" },
		];
		_sut.Suggestions.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void Autocomplete_QueryThatMatchesMultipleProperties_GiveUniqueSuggestions()
	{
		// Act
		_sut.Query = "a";
		_sut.Autocomplete();

		// Assert
		IEnumerable<Car> expected =
		[
			new() { Make = "Cupra", Model = "Born" },
			new() { Make = "Lambo", Model = "Recuelti" },
			new() { Make = "Fiat", Model = "500" },
			new() { Make = "Ferrari", Model = "Testarossa" },
		];
		_sut.Suggestions.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void Next_UsesNavigateService()
	{
		// Arrange
		_sut.Query = "i";
		_sut.Autocomplete();
	
		// Act
		_sut.Next();
	
		// Assert
		_navigateServiceMock.HasNextBeenCalled.Should().BeTrue();
		_sut.ActiveSuggestionIndex.Should().Be(42);
	}
	
	[TestMethod]
	public void Next_NoSuggestions_NoAction()
	{
		// Act
		_sut.Next();

		// Assert
		_sut.ActiveSuggestionIndex.Should().BeNull();
		_navigateServiceMock.HasNextBeenCalled.Should().BeFalse();
	}
}
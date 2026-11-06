using AwesomeAssertions;
using Moq;

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
	private Mock<INavigateService> _navigateServiceMock = null!;
		
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
		_navigateServiceMock = new Mock<INavigateService>();
		
		_sut = new(_navigateServiceMock.Object);
		_sut.Data = _cars;
	}

	// [TestCleanup]
	// public void Cleanup()
	// {
	// 	_navigateServiceMock.VerifyAll();
	// }

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
		_navigateServiceMock.Setup(x => x.Next(It.IsAny<List<Car>>(), It.IsAny<int?>())).Returns(42);
	
		// Act
		_sut.Next();
	
		// Assert
		_navigateServiceMock.Verify(x => x.Next(It.IsAny<List<Car>>(), It.IsAny<int?>()), Times.Once);
		_sut.ActiveSuggestionIndex.Should().Be(42);
	}
	
	[TestMethod]
	public void Next_NoSuggestions_NoAction()
	{
		// Act
		_sut.Next();

		// Assert
		_sut.ActiveSuggestionIndex.Should().BeNull();
		_navigateServiceMock.Verify(x => x.Next(It.IsAny<List<Car>>(), It.IsAny<int?>()), Times.Never);
	}
}

using AwesomeAssertions;

namespace DemoProject.Tests;

[TestClass]
public class CalculatorTests
{
	// be very wary of side-effects "flaky tests" - sometimes it succeeds/fails, other times it doesn't
	private Calculator _sut = null!;

	// test method naming conventions:
	// - MS: Method_State_ExpectedBehavior
	// - State_Method_ExpectedBehavior
	// - GivenMethod_When_State_Then_Result
	// - feature: Add_Customer_Should_Update_Total_In_Cart_When_Button_Pressed

	static int _counter = 0;

	// [ClassInitialize] // fires once
	// public void BiggerInit()
	// {
	// 	
	// }

	[TestInitialize] // runs before every test
	public void Init()
	{
		Console.WriteLine($"Counter: {++_counter}");
		// Arrange - setup / given - 3-30 lines (if 30, consider placing stuff in separate methods)
		_sut = new Calculator(); // system under test
	}

	[TestMethod]
	[DataRow(4, 8, 12)]
	[DataRow(-4, 8, 4)]
	[DataRow(4, -8, -4)]
	[DataRow(-4, -8, -12)]
	public void Add_DifferentNumberInputs_Adds(int a, int b, int expected)
	{
		// Act - do / when  - 1 or 2 lines
		// Thread.Sleep(Random.Shared.Next(1, 10) * 1000);
		var result = _sut.Add(a, b);

		// Assert - verify / then (1 - 10 lines)
		Assert.AreEqual(expected, result);
		Console.WriteLine($"magic number is now {Calculator.MagicNumber}");
	}
}
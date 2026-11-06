# Notes

## What to test?

Right BICEP can help determine what to test:

- Right - are the results right?
- Boundary - int.maxValue
- Inverse something - `GetMaleEmployees()` - `GetAllEmployees()` - `GetFemaleEmployees()`
- Cross-check results -
- Error conditions - `someService.GetMagicNumber()`
- Performance

## Black box vs white box testing

- black box: no idea what's happening inside
  - preferred
- white box: transparent, you have knowledge of its internals

## What is a unit?

"smallest possible bit of code to execute"

`Add(4, 8)`

- `return 4 + 8;` - operator overloading
- `return Math.Round(4 + 8);` - calling a static method inside of .NET
- `return abs(4) + abs(8);` - my own `abs()` inside the same class
- `return calcService.Add(4, 8);` - call method on dependency injected service
- `return CallWebserverInSweden(4, 8);` - integration
- `return AskChatGpt(4, 8);` - integration

## constructor vs `Init()` with `[TestInitialize]`

- this only applies when using MSTest!
- both can be used for initialization
- `Init()` can be made `async` in order to use `await`. Async constructors are not yet a thing in C#
- if your test classes use inheritance, it can be nicer to explicitly call base initialization logic
  ```cs
  base.Init();
  ```
  Base constructors get called no matter what.

## MSTest vs xUnit vs NUnit

- .NET Core 1.0 didn't support MSTest, xUnit gained a lot of popularity at that time
- MSTest [used to only have `\[DataSource\]` for data-driven tests](https://learn.microsoft.com/en-us/visualstudio/test/how-to-create-a-data-driven-unit-test?view=visualstudio):

  ```cs
  [TestMethod]
  [DataSource(@"Provider=Microsoft.SqlServerCe.Client.4.0; Data Source=C:\Data\MathsData.sdf;", "Numbers")]
  public void AddIntegers_FromDataSourceTest()
  {
      var target = new Maths();

      // Access the data
      int x = Convert.ToInt32(TestContext.DataRow["FirstNumber"]);
      int y = Convert.ToInt32(TestContext.DataRow["SecondNumber"]);
      int expected = Convert.ToInt32(TestContext.DataRow["Sum"]);
      int actual = target.AddIntegers(x, y);
      Assert.AreEqual(expected, actual, "x:<{0}> y:<{1}>", new object[] {x, y});
  }
  ```

  xUnit/NUnit both had easier approaches with `[InlineData()]` and `[TestCase()]`. MSTest later introduced `[DataRow]`

- they've all grown a lot closer!

## When tests are less valuable / when not to write tests

- if the intern/tester writes all the tests
  - the developer implementing the feature holds all the knowledge of that feature. He should write the tests.
- if you're trying to get to x% code coverage
  - the metric is not that holy
- if your project is done in 6 months time - temporary projects, hobby projects, demonstration stuff, proof of concepts, maybe an electoral app for this year's elections
  - do know though, "nothing is as permanent as a temporary solution"

## Test-driven development

1. Write test
2. Run test(s) and see that one or more FAILS
3. Implement as small and simple as possible
4. Run tests and see that everything SUCCEEDS
5. Refactor

Repeat. Shorthand: red-green-refactor.

## Concept of mocking

What do I want to mock? "external resources"

- Database
- File system
- Everything network

But also:

Everything not my class.

- All dependencies

Dependency injection is often seen in combination with mocking:

- a form of inversion of control - no more `new MyDependencyService()`
- "high cohesion, low coupling"
  - in C#, we generally work with interfaces to accomplish this
- inject dependencies:
  - constructor injection
    ```cs
    public Autocompleter(NavigateService navigateService, MoreService moreService) {}
    ```
  - field/property injection
    ```cs
    [Inject]
    public MoreService _moreService { get; set; }
    ```
  - explicitly
    ```cs
    MoreService _moreService = inject<MoreService>();
    ```

## Mock frameworks

- Moq
  - most popular
- NSubstitute
- FakeItEasy

## Mutation testing

a tool that automates the changing of your code ("mutating") and running all your tests

- introducing bugs in your code to see if your tests fail
- tests should fail
- "testing your tests"
- Stryker

```cs
// production code
if (number > 14)
{
	// ...
}

// mutants
if (number < 14) { } // run all tests    KILLED
if (number == 14) { }  // run all tests  KILLED
if (number > 4000) { }  // run all tests  KILLED
```

## Various

- Assertion libraries such as [AwesomeAssertions](https://awesomeassertions.org/) make devlife easier
- [TypeMock](https://www.typemock.com) allows testing legacy code by mocking on IL-level
- Only [VS Enterprise has Microsoft Fakes](https://visualstudio.microsoft.com/vs/compare/) that allows mocking statics like `DateTime.Now` and `File.AppendAllText()`
  - does not work in Rider/VS Code
  - want to mock this without? write a wrapper class that is easily mockable
    ```cs
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTIme.Now;
        }
    }
    ```
- [The Turkey test](https://stackoverflow.com/questions/796986/what-is-the-turkey-test) and why `.ToUpper()` isn't the best option for case-insensitive string comparisons
- [Best moment of TaskMaster](https://www.youtube.com/watch?v=_UgSDcHPgCc)
  - Another favorite: [Open the Briefcase](https://www.youtube.com/watch?v=5UapTuq7e90)

namespace Lesson21;

public enum TestDataAge
{
    Child = 7,
    Teenager = 14,
    Adult = 30
}

[TestFixture]
public class EnumHomework
{
    [Test]
    public void CheckCustomIntNumbersForTestDataAgeEnum()
    {

        Assert.Multiple(() =>
        {
            Assert.That((int)TestDataAge.Child, Is.EqualTo(7));
            Assert.That((int)TestDataAge.Teenager, Is.EqualTo(14));
            Assert.That((int)TestDataAge.Adult, Is.EqualTo(30));
        });
    }

    [Test]
    public void SomeIntCorrespondsToSomeTestDataAgeValue()
    {
        var listOfInt = new List<int>() { 5, 14, 15 };
        var testDataAgeValues = Enum.GetValues(typeof(TestDataAge)).Cast<int>();
        bool isAnyIntCorrespondsToTestDataAge = listOfInt.Any(x => testDataAgeValues.Contains(x));

        Assert.That(isAnyIntCorrespondsToTestDataAge, Is.True);
    }

    [Test]
    public void NumberOfIntCorrespondsToSomeTestDataAgeValue()
    {
        var listOfInt = new List<int>() { 5, 14, 15, 30 };
        var testDataAgeValues = Enum.GetValues(typeof(TestDataAge)).Cast<int>();
        var numberOfIntCorrespondToTestDataAge = listOfInt.Count(x => testDataAgeValues.Contains(x));

        Assert.That(numberOfIntCorrespondToTestDataAge, Is.EqualTo(2));
    }


    [TestCaseSource(nameof(StringlEmentsArePresentInEnumCases))]
    public void StringlEmentsArePresentInEnum(string[] list, int expectedNumberPresent, int expectedNumberExtra, bool areAllPresentExpected, bool areExtraElementsExpected)
    {
        var listOfString = list.ToList();
        var enumValues = Enum.GetNames(typeof(TestDataAge));

        // Calculate the number of strings present in the enum
        var numberOfStringsWhichPresentInEnum = listOfString.Count(str => enumValues.Contains(str));
        // Calculate the number of strings not present in the enum
        var numberOfStringsWhichAreNotPresentInEnum = listOfString.Count(str => !enumValues.Contains(str));
        // Determine if all strings are present in the enum
        var areAllPresent = numberOfStringsWhichAreNotPresentInEnum == 0;
        // Determine if there are extra elements not present in the enum
        var areExtraElements = numberOfStringsWhichAreNotPresentInEnum > 0;

        Assert.Multiple(() =>
        {
            Assert.That(numberOfStringsWhichPresentInEnum, Is.EqualTo(expectedNumberPresent));
            Assert.That(numberOfStringsWhichAreNotPresentInEnum, Is.EqualTo(expectedNumberExtra));
            Assert.That(areAllPresent, Is.EqualTo(areAllPresentExpected));
            Assert.That(areExtraElements, Is.EqualTo(areExtraElementsExpected));
        });

    }

    public static object[] StringlEmentsArePresentInEnumCases =
    {
            new object[] { new string[] { "Child", "Baby", "Teenager", "Eldery", "Adult" }, 3, 2, false, true },
            new object[] { new string[] { "Child", "Teenager", "Adult" }, 3, 0, true, false },
            new object[] { new string[] { "Baby", "Teenager", "Eldery" }, 1, 2, false, true },
            new object[] { new string[] { "Adult", "Child" }, 2, 0, true, false },
            new object[] { new string[] { "Eldery", "Baby" }, 0, 2, false, true },
            new object[] { new string[] { }, 0, 0, true, false },
    };
}

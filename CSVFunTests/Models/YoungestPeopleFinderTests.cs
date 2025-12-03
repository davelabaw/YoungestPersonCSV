using NUnit.Framework;

namespace CSVFun.Models.Tests;

[TestFixture()]
public class YoungestPeopleFinderTests
{
    [Test()]
    public void AddPersonIfYoungerTest()
    {
        var youngestPeopleFinder = new YoungestPeopleFinder(maxListCount: 2);
        var testPeople = GetTestPeople();

        foreach (var person in testPeople)
        {
            youngestPeopleFinder.AddPersonIfYounger(person);
        }

        var youngestPeople = youngestPeopleFinder.GetYoungestPeopleOrderedDescending().ToList();
        Assert.AreEqual(2, youngestPeople.Count);
        Assert.AreEqual("Charlie", youngestPeople[0].FirstName);
        Assert.AreEqual("Diana", youngestPeople[1].FirstName);
    }

    #region Build Test Data
    private List<Person> GetTestPeople()
    {
        return new List<Person>
        {
            new Person
            {
                Index = 0,
                UserId = "user0",
                FirstName = "Alice",
                LastName = "Smith",
                DateOfBirth = new DateTime(2000, 1, 1),
            },
            new Person
            {
                Index = 1,
                UserId = "user1",
                FirstName = "Bob",
                LastName = "Johnson",
                DateOfBirth = new DateTime(1995, 5, 15),
            },
            new Person
            {
                Index = 2,
                UserId = "user2",
                FirstName = "Charlie",
                LastName = "Brown",
                DateOfBirth = new DateTime(2010, 10, 30),
            },
            new Person
            {
                Index = 3,
                UserId = "user3",
                FirstName = "Diana",
                LastName = "Princess",
                DateOfBirth = new DateTime(2005, 7, 20),
            },
        };
    }
    #endregion
}
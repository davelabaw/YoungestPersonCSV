namespace CSVFun.Models;

public class YoungestPeopleFinder
{
    private readonly List<Person> _people = new();
    private Person? _currentOldestPerson;
    private int _maxListCount;

    public YoungestPeopleFinder(int maxListCount)
    {
        _maxListCount = maxListCount;
    }

    public void AddPersonIfYounger(Person person)
    {
        //Data structure
        //     - keep track of the oldest record in the list
        //     - if older than the older list, ignore
        //     - else, remove the oldest person, and capture the current oldest person

        if (IsYoungerThanOldest(person))
        {
            if (_people.Count >= _maxListCount)
            {
                //Remove the oldest person
                if (_currentOldestPerson != null)
                {
                    _people.Remove(_currentOldestPerson);
                }
            }

            _people.Add(person);
            _currentOldestPerson = GetOldestPerson();
        }
    }

    private bool IsYoungerThanOldest(Person person)
    {
        if (_currentOldestPerson == null)
        {
            return true;
        }
        return person.DateOfBirth > _currentOldestPerson.DateOfBirth;
    }

    private Person? GetOldestPerson()
    {
        return _people
            .OrderBy(p => p.DateOfBirth)
            .FirstOrDefault();
    }

    public IOrderedEnumerable<Person> GetYoungestPeopleOrderedDescending()
    {
        return _people
            .OrderByDescending(p => p.DateOfBirth);
    }
}

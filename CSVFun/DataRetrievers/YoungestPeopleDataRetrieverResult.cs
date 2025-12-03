using CSVFun.Models;

namespace CSVFun.DataRetrievers;

public class YoungestPeopleDataRetrieverResult
{
    public required YoungestPeopleFinder YoungestPeopleFinder { get; init; }
    public required int TotalPeopleProcessed { get; init; }
}

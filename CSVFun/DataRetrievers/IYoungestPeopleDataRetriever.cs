using CSVFun.Models;

namespace CSVFun.DataRetrievers;

public interface IYoungestPeopleDataRetriever
{
    YoungestPeopleDataRetrieverResult GetYoungestPeople(YoungestPeopleDataRetrieverRequestOptions options);
}

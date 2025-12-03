namespace CSVFun.DataRetrievers;

public  class YoungestPeopleDataRetrieverRequestOptions
{
    public required int MaxYoungestListCount { get; init; }
    public required string CsvFilePath { get; init; }
    public bool IsDebugMode { get; init; } = false;
}

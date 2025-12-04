using CSVFun.Models;
using CsvHelper;

namespace CSVFun.DataRetrievers;

public class YoungestPeopleCsvHelperDataRetriever : IYoungestPeopleDataRetriever
{
    public YoungestPeopleDataRetrieverResult GetYoungestPeople(YoungestPeopleDataRetrieverRequestOptions options)
    {
        var lineCount = 0;
        var youngestPeopleFinder = new YoungestPeopleFinder(options.MaxYoungestListCount);

        foreach (var csvFilePath in options.CsvFilePaths)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            var records = csv.GetRecords<Person>();

            foreach (var record in records)
            {
                lineCount++;

                if (options.IsDebugMode)
                    Console.WriteLine($"{record.Index},{record.UserId},{record.FirstName},{record.LastName},{record.DateOfBirth.ToShortDateString()}");

                youngestPeopleFinder.AddPersonIfYounger(record);
            }
        }

        return new YoungestPeopleDataRetrieverResult
        {
            TotalPeopleProcessed = lineCount,
            YoungestPeopleFinder = youngestPeopleFinder
        };
    }
}

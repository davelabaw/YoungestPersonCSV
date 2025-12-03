using CSVFun.Models;
using Microsoft.VisualBasic.FileIO;

namespace CSVFun.DataRetrievers;

public class YoungestPeopleStandardDataRetriever : IYoungestPeopleDataRetriever
{
    public YoungestPeopleDataRetrieverResult GetYoungestPeople(YoungestPeopleDataRetrieverRequestOptions options)
    {
        var lineCount = 0;
        var youngestPeopleFinder = new YoungestPeopleFinder(options.MaxYoungestListCount);

        using var streamReader = new StreamReader(options.CsvFilePath);
        using (TextFieldParser parser = new TextFieldParser(streamReader))
        {
            parser.SetDelimiters([","]);
            parser.HasFieldsEnclosedInQuotes = true;

            parser.ReadLine(); //Ignore the header line

            while (!parser.EndOfData)
            {
                var values = new List<string>();

                var readFields = parser.ReadFields();
                if (readFields != null)
                {
                    values.AddRange(readFields);
                    lineCount++;


                    if (options.IsDebugMode)
                        Console.WriteLine(String.Join(",", readFields));

                    youngestPeopleFinder.AddPersonIfYounger(
                        new Person
                        {
                            Index = int.Parse(readFields[0]),
                            UserId = readFields[1],
                            FirstName = readFields[2],
                            LastName = readFields[3],
                            //...
                            DateOfBirth = DateTime.Parse(readFields[7]),
                        }
                    );
                }

            }
        }
        return new YoungestPeopleDataRetrieverResult
        {
            TotalPeopleProcessed = lineCount,
            YoungestPeopleFinder = youngestPeopleFinder
        };
    }
}

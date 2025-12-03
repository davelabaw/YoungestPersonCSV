using CSVFun.DataRetrievers;

#region Specify Settings
bool _isDebugMode = false;

//People data from https://www.datablist.com/learn/csv/download-sample-csv-files#people-dataset
var csvFilePath = @"C:\Dev\Testbed\CSVFun\Resources\people-2000000.csv"; //people-1000000.csv"; //
if (_isDebugMode)
    csvFilePath = @"C:\Dev\Testbed\CSVFun\Resources\people-100.csv";

var useStandardDataRetriever = false;

IYoungestPeopleDataRetriever retriever = (useStandardDataRetriever ? new YoungestPeopleStandardDataRetriever() : new YoungestPeopleCsvHelperDataRetriever());
const int maxYoungestCount = 10;
#endregion

#region Retrieve and Process Data
Console.WriteLine($"{DateTime.Now}: Processing CSV file: {csvFilePath}");
Console.WriteLine($"{DateTime.Now}: Using {retriever.GetType()}");

var result = retriever.GetYoungestPeople(
    new YoungestPeopleDataRetrieverRequestOptions
    {
        CsvFilePath = csvFilePath,
        MaxYoungestListCount = maxYoungestCount,
        IsDebugMode = _isDebugMode,
    }
);

Console.WriteLine($"{DateTime.Now}: Processed {result.TotalPeopleProcessed:N0} people.");

#endregion

#region Display Results
var currentDate = DateTime.Now;
Console.WriteLine("Youngest People:");
foreach (var person in result.YoungestPeopleFinder.GetYoungestPeopleOrderedDescending())
{
    //Get current age
    int years = currentDate.Year - person.DateOfBirth.Year;
    int months = currentDate.Month - person.DateOfBirth.Month;

    // Adjust for cases where the current month/day is before the birth month/day
    if (months < 0 || (months == 0 && currentDate.Day < person.DateOfBirth.Day))
    {
        years--;
        months += 12; // Add 12 months to get the correct positive month difference
    }

    Console.WriteLine($"\t{person.FirstName} {person.LastName} - DOB: {person.DateOfBirth.ToShortDateString()} (Age: {years} yrs, {months} mos)");
}

Console.WriteLine($"{DateTime.Now}: Finished!");
#endregion

Console.ReadLine(); //Stay open

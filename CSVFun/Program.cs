using CSVFun.DataRetrievers;

#region Specify Settings
bool _isDebugMode = false;

//People data downloaded from https://www.datablist.com/learn/csv/download-sample-csv-files#people-dataset
//Attention: Be sure that the csv files are copied to the output directory
//      [ Solution Explorer > right click .csv file > Properties > set Copy To Output Directory to "Copy always" ]
//string[] csvFilePaths = [$@"{Environment.CurrentDirectory}\Resources\people-2000000.csv"]; //File too large to check in to GitHub
//string[] csvFilePaths = [$@"{Environment.CurrentDirectory}\Resources\people-1000000.csv"]; //File too large to check in to GitHub 

string[] csvFilePaths = [$@"{Environment.CurrentDirectory}\Resources\people-500000A.csv", $@"{Environment.CurrentDirectory}\Resources\people-500000B.csv"];
if (_isDebugMode)
    csvFilePaths = [$@"{Environment.CurrentDirectory}\Resources\people-100.csv"];

//Ensure csv file exists
foreach (var csvFilePath in csvFilePaths)
{
    if (!File.Exists(csvFilePath))
    {
        throw new FileNotFoundException("CSV file not found.", csvFilePath);
    }
}

var useStandardDataRetriever = false;

IYoungestPeopleDataRetriever retriever = (useStandardDataRetriever ? new YoungestPeopleStandardDataRetriever() : new YoungestPeopleCsvHelperDataRetriever());
const int maxYoungestCount = 100;
#endregion

#region Retrieve and Process Data
Console.WriteLine($"{DateTime.Now}: Processing CSV file: {String.Join(", ", csvFilePaths)}");
Console.WriteLine($"{DateTime.Now}: Using {retriever.GetType()}");

var result = retriever.GetYoungestPeople(
    new YoungestPeopleDataRetrieverRequestOptions
    {
        CsvFilePaths = csvFilePaths,
        MaxYoungestListCount = maxYoungestCount,
        IsDebugMode = _isDebugMode,
    }
);

Console.WriteLine($"{DateTime.Now}: Processed {result.TotalPeopleProcessed:N0} people.");

#endregion

#region Display Results
var currentDate = DateTime.Now;
Console.WriteLine($"Top {maxYoungestCount} Youngest People:");
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

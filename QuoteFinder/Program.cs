// sample url to get one page of quotes: https://quote-garden.onrender.com/api/v3/quotes?limit=100&page=1 
/*
Main application workflow    

When the application starts, it shall ask the user the following:
The word they want to search. Only quotes containing this word will be finally shown. This should be validated to be a single word, without spaces, numbers, special characters, etc.
The number of pages of data to check. This number will match the number of quotes shown to the user as an output.
The number of quotes to be on each page. The more quotes, the bigger the chance of finding one containing the required word.
(optional) Whether the processing of the downloaded data should be performed in parallel or not.
Then, the app will query the Quote Garden API to get the quotes to be searched. There should be 1 request per page, and the number of quotes on each page should be limited to the value of the third parameter provided by the user.

Once data is downloaded, it should be processed. For each page, we search for a single quote containing the given word. The algorithm checking for word presence should be smart and, for example, don’t include quotes with word category if the user searches for cat. 

If more than one quote with this word is found on a page, the shortest should be chosen and printed to the console. If no quote is found on a page, a proper message should also be printed.
Optional requirement:
The processing of the response includes JSON deserialization and the search for a matching word in the collection of quotes. As an optional requirement, we want to let the user choose whether they want this processing to be done in sequence or in parallel, using multithreading. The execution time should be measured so the results can be compared. 
Finally, the app prints “Program is finished.” and after the user presses any key, the window closes.

 

            Alternative data source
Since the QuoteGarderis an external service, we don’t have control over whether it is available or not. In case it is down, the developer can use the mock data provider called MockQuotesApiDataReader. Its source code can be found in the Git repository, as well as in the resources of the “Assignment - Quotes Finder - Description and requirements” lecture. 


*/


// Entry point - wires up dependencies and starts the application
using QuoteFinder.App;
using QuoteFinder.DataAccess;
using QuoteFinder.DataAccess.Mock;
using QuoteFinder.Infrastructure;
using QuoteFinder.Services;

// Wire up dependencies
var userInteractor = new ConsoleUserInteractor();
var inputValidator = new InputValidator();
var userInputService = new UserInputService(userInteractor, inputValidator);
var quotesProvider = new QuotesProvider(new MockQuotesApiDataReader());
var quotesProcessor = new QuotesProcessor();

// Create and run the app
var app = new QuoteFinderApp(userInputService, quotesProvider, quotesProcessor, userInteractor);
app.Run();

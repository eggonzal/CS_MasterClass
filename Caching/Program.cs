
using Caching.Cache;
using Caching.Downloaders;

var dataDownloader = new PrintingDataDownloader(
    new CachingDataDownloader(
        new SlowDataDownloader(),
        new SimpleCacheDict<string, string>()
    )
);

Console.WriteLine(dataDownloader.DownloadData("id1"));
Console.WriteLine(dataDownloader.DownloadData("id2"));
Console.WriteLine(dataDownloader.DownloadData("id3"));
Console.WriteLine(dataDownloader.DownloadData("id1"));
Console.WriteLine(dataDownloader.DownloadData("id3"));
Console.WriteLine(dataDownloader.DownloadData("id1"));
Console.WriteLine(dataDownloader.DownloadData("id2"));

Console.ReadKey();
namespace Caching.Downloaders;

public class PrintingDataDownloader(IDataDownloader downloader) : IDataDownloader
{
    private readonly IDataDownloader _downloader = downloader;

    public string DownloadData(string resourceId)
    {
        var data = _downloader.DownloadData(resourceId);
        Console.WriteLine($"Data is ready!");
        return data;
    }
}

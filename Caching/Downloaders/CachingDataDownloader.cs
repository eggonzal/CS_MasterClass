using Caching.Cache;

namespace Caching.Downloaders;

public class CachingDataDownloader(IDataDownloader downloader, ICache<string, string> cache) : IDataDownloader
{
    private readonly IDataDownloader _downloader = downloader;
    private readonly ICache<string, string> _cache = cache;

    public string DownloadData(string resourceId)
    {
        return _cache.Get(resourceId, _downloader.DownloadData);
    }
}

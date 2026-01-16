namespace Caching.Cache;

public class SimpleCacheDict<TKey, TData> : ICache<TKey, TData> where TKey : notnull
{
    private readonly Dictionary<TKey, TData> _cache = [];

    public TData Get(TKey resourceId, Func<TKey, TData> func)
    {
        if (_cache.TryGetValue(resourceId, out var cachedData))
        {
            return cachedData;
        }
        var data = func(resourceId);
        _cache[resourceId] = data;
        return data;
    }
}

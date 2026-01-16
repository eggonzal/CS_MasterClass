namespace Caching.Cache;

public interface ICache<TKey, TData> where TKey : notnull
{
    TData Get(TKey resourceId, Func<TKey, TData> func);
}

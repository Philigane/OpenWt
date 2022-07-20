namespace OpenWt.Models.Mocks;

public class ConcurrentList<T>
{
    private readonly List<T> _list = new();
    private readonly SemaphoreSlim _locker = new(1);
    public void AddOrUpdate(T value, Predicate<T> predicate)
    {
        _locker.Wait();
        var index = _list.FindIndex(predicate);
        if (index == -1)
            _list.Add(value);
        else
            _list[index] = value;
        _locker.Release();
    }
    public void RemoveAll(Predicate<T> predicate)
    {
        _locker.Wait();
        _list.RemoveAll(predicate);
        _locker.Release();
    }
    public IEnumerable<T> Where(Func<T, bool> predicate)
    {
        try
        {
            _locker.Wait();
            return _list.Where(predicate);
        }
        finally
        {
            _locker.Release();
        }
    }
}
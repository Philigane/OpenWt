namespace OpenWt.Models.Mocks;

public abstract class DatabaseCoreMock<T, T2> where T2 : T, new()
{
    private readonly ConcurrentList<T> _elements = new();

    public IEnumerable<T> Get(Func<T, bool>? predicate = null) => _elements.Where(predicate ?? ((_) => true));

    public int GetId(T entity) => (int)entity?.GetType().GetProperty("Id")?.GetValue(entity)!;

    public T AddOrUpdate(T entity)
    {
        _elements.AddOrUpdate(entity, x => GetId(x) == GetId(entity));
        return entity;
}

    public void Delete(T entity)
    {
        _elements.RemoveAll(x => GetId(x) == GetId(entity));
    }

    public void Delete(IEnumerable<T> entities)
    {
        var ids = entities.ToList().Select(GetId);
        _elements.RemoveAll(x => ids.Contains(GetId(x)));
    }

    public T GetNew() => new T2();
}
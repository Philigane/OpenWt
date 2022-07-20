namespace OpenWt.Contracts;

public interface IAddOrUpdate<T> where T : class
{
    public T GetNew();
    public T AddOrUpdate(T entity);
}

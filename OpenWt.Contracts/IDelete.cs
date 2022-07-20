namespace OpenWt.Contracts;

public interface IDelete<in T> where T : class
{
    public void Delete(T entity);
    public void Delete(IEnumerable<T> entities);
}

namespace OpenWt.Contracts;

public interface IGetter<out T> where T : class
{
    public IEnumerable<T> Get(Func<T, bool>? predicate = null);
}

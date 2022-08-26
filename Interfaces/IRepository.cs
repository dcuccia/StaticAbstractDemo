public interface IRepository<T>
{
    List<T> Find(Predicate<T> query); 
}
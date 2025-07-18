namespace rubricaClinica.Repos
{
    public interface IRepo<T>
    {
        T? GetById(int id);

        IEnumerable<T> GetAll();

        bool Create(T entity);

        bool Update(T entity);

        bool Delete(int id);
    }
}

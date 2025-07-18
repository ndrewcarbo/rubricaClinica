namespace rubricaClinica.Services
{
    public interface IService<T>
    {
        T? CercaPerCodice(string codice);
        IEnumerable<T> CercaTutti();
        bool Inserisci(T entity);
        bool Aggiorna(T entity);
        bool Elimina(string codice);
    }
}

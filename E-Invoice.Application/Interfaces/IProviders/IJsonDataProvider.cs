namespace E_Invoice.Application.Interfaces.IProviders
{
    public interface IJsonDataProvider<T>
    {
        IEnumerable<T> GetAll();
    }
}

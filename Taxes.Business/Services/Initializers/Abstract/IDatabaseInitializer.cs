namespace Taxes.Business.Services.Initializers.Abstract
{
    public interface IDatabaseInitializer
    {
        Task InitializeDatabaseAsync();
    }
}

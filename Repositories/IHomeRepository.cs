namespace MagyargombfociStore
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetButtonFootballs(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Categories>> Categories();

    }
}
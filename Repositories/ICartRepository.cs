namespace MagyargombfociStore.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int buttonfootballId, int qty);
        Task<int> RemoveItem(int buttonfootballId);

        Task<ShoppingCart> GetUserCart();


        Task<int> GetCartItemCount(string userId = "");

        Task<ShoppingCart> GetCart(string userId);

        Task<bool> DoCheckout();



    }
}
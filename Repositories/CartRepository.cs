using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagyargombfociStore.Repositories
{
    public class CartRepository:ICartRepository
    {
        private readonly MagyargombfociStoreContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CartRepository(MagyargombfociStoreContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<int> AddItem(int buttonfootballId, int qty)
        {
            string userId = GetUserId();
            using var transaction = _db.Database.BeginTransaction();
            try {

                
                if (string.IsNullOrEmpty(userId))

                    throw new Exception("a felhasználó nem lépett be!");

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId


                    };
                    _db.ShoppingCarts.Add(cart);

                }

                _db.SaveChanges();

                //kosár részletek szekció
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == buttonfootballId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var button_football = _db.Products.Find(buttonfootballId);
                    cartItem = new CartDetail
                    {
                        ProductId = buttonfootballId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = (double)button_football.Price
                    };

                    _db.CartDetails.Add(cartItem);

                }

                _db.SaveChanges();
                transaction.Commit();
              

            }
            catch (Exception ex)
            {
                
            }

            var cartItemCount =await GetCartItemCount(userId);
            return cartItemCount;

        }


        public async Task<int> RemoveItem(int buttonfootballId)
        {

            //using var transaction = _db.Database.BeginTransaction();
            string userId = GetUserId();
            try
            {


                if (string.IsNullOrEmpty(userId))

                    throw new Exception("a felhasználó nem lépett be!");

                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Érvénytelen kosár");

               



                //kosár részletek szekció
                var cartItem = _db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == buttonfootballId);
                if (cartItem is null)
                    throw new Exception("A kosár üres");
                else if (cartItem.Quantity == 1)
                
                    _db.CartDetails.Remove(cartItem);
                
                else
                
                    cartItem.Quantity = cartItem.Quantity - 1;

                

                _db.SaveChanges();
                
                

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Hiba a kosár művelet közben: {ex.Message}");
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;

        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var userId= GetUserId();
            if (userId == null)
                throw new Exception("Érvénytelen felhasználó azonosító");

            var shoppingCart = await _db.ShoppingCarts.
                Include(a => a.CartDetails)
                .ThenInclude(a => a.Product)
                .ThenInclude(a => a.Categories)
                .Where(a => a.UserId == userId).FirstOrDefaultAsync();

            return shoppingCart;

        }

        public async Task<ShoppingCart> GetCart(string userId) 
        { 
            var cart= await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        
        }


        public async Task<int> GetCartItemCount(string userId="")
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }

            var data = await (from cart in _db.ShoppingCarts join CartDetail in _db.CartDetails on cart.Id equals CartDetail.ShoppingCartId select new { CartDetail.Id }).ToListAsync();

            return data.Count;


        }



        public async Task<bool> DoCheckout()
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                // logic
                // move data from cartDetail to order and order detail then we will remove cart detail
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartDetail = _db.CartDetails
                                    .Where(a => a.ShoppingCartId == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");
                var order = new Order
                {
                    UserId = userId,
                    CreateDate = DateTime.UtcNow,
                    OrderStatusId = 1//pending
                };
                _db.Orders.Add(order);
                _db.SaveChanges();
                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    _db.OrderDetails.Add(orderDetail);
                }
                _db.SaveChanges();

                // removing the cartdetails
                _db.CartDetails.RemoveRange(cartDetail);
                _db.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }




        private string GetUserId()
        {
            var principal= _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);

            return userId;

        }


    }
}

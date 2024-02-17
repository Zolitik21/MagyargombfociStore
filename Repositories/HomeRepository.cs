

using Microsoft.EntityFrameworkCore;

namespace MagyargombfociStore.Repositories
{
    public class HomeRepository: IHomeRepository
    {
        private readonly MagyargombfociStoreContext _db;
        public HomeRepository(MagyargombfociStoreContext db)
        {
                _db= db;
        }

        public async Task<IEnumerable<Categories>> Categories()
        {
            return await _db.Categories.ToListAsync();


        }


        public async Task<IEnumerable<Product>> GetButtonFootballs(string sTerm="", int categoryId=0)
        {

            sTerm=sTerm.ToLower();
            IEnumerable<Product> buttonfootballs= await (from buttonfootball in _db.Products
                                 join category in _db.Categories
                                 on buttonfootball.CategoryId equals
                                 category.Id
                                 where string.IsNullOrWhiteSpace(sTerm) || (buttonfootball!=null && buttonfootball.ProductName.ToLower().StartsWith(sTerm))
                                 select new Product
                                 {
                                     Id=buttonfootball.Id,
                                     ProductName = buttonfootball.ProductName,
                                     Quality = buttonfootball.Quality,
                                     Price = buttonfootball.Price,
                                     Image =buttonfootball.Image,
                                     CategoryId=buttonfootball.CategoryId,
                                     CategoryName=category.CategoryName
                                     


                                 }).ToListAsync();


            if (categoryId>0)
            {
                buttonfootballs=buttonfootballs.Where(a=>a.CategoryId==categoryId).ToList();
            }
            return buttonfootballs;






        }


    }
}

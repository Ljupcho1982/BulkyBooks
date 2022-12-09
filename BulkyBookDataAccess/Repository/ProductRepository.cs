using BulkyBook.Model;
using BulkyBookDataAccess.Repository.IRepository;

namespace BulkyBookDataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(Product obj)
        {
            //_db.Products.Update(obj);

            var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);

            if (objFromDb != null)
            {

                objFromDb.Title = obj.Title;
                objFromDb.ISNB = obj.ISNB;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.CoverType = obj.CoverType;

                if (obj.ImageUrl != obj.ImageUrl)
                {

                    objFromDb.ImageUrl = obj.ImageUrl;
                }


            }
        }
    }
}

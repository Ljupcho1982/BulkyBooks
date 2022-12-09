using BulkyBook.Model;
using BulkyBookDataAccess.Repository.IRepository;

namespace BulkyBookDataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {

        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(CoverType obj)
        {
            _db.CoverType.Update(obj);
        }
    }
}

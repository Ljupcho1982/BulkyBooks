﻿using BulkyBook.Model;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);



    }
}

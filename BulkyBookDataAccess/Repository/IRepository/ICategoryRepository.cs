﻿using BulkyBook.Model;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);



    }
}

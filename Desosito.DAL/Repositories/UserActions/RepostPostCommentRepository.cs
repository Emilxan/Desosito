using Desosito.DAL.Interface;
using Desosito.Domain.Entity;
using Desosito.Domain.Entity.UserAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.DAL.Repositories
{
    public class LikePostRepository : IBaseRepository<LikePost>
    {
        private readonly ApplicationDbContext _db;

        public LikePostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(LikePost entity)
        {
            await _db.LikePost.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(LikePost entity)
        {
            _db.LikePost.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<LikePost> GetAll()
        {
            return _db.LikePost;
        }

        public async Task<LikePost> Update(LikePost entity)
        {
            _db.LikePost.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

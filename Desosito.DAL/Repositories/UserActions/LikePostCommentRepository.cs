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
    public class RepostPostRepository : IBaseRepository<RepostPost>
    {
        private readonly ApplicationDbContext _db;

        public RepostPostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(RepostPost entity)
        {
            await _db.RepostPost.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(RepostPost entity)
        {
            _db.RepostPost.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<RepostPost> GetAll()
        {
            return _db.RepostPost;
        }

        public async Task<RepostPost> Update(RepostPost entity)
        {
            _db.RepostPost.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

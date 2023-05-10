using Desosito.DAL.Interface;
using Desosito.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Desosito.DAL.Repositories
{
    public class PostRepository : IBaseRepository<Post>
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Post entity)
        {
            await _db.Post.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Post entity)
        {
            _db.Post.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<Post> GetAll()
        {
            return _db.Post;
        }

        public async Task<Post> Update(Post entity)
        {
            _db.Post.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

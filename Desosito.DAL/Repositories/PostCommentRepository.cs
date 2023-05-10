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
    public class PostCommentRepository : IBaseRepository<PostComment>
    {
        private readonly ApplicationDbContext _db;

        public PostCommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(PostComment entity)
        {
            await _db.PostComment.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(PostComment entity)
        {
            _db.PostComment.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<PostComment> GetAll()
        {
            return _db.PostComment;
        }

        public async Task<PostComment> Update(PostComment entity)
        {
            _db.PostComment.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

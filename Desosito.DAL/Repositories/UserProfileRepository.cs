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
    public class UserProfileRepository : IBaseRepository<UserProfile>
    {
        private readonly ApplicationDbContext _db;

        public UserProfileRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(UserProfile entity)
        {
            await _db.UserProfile.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(UserProfile entity)
        {
            _db.UserProfile.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<UserProfile> GetAll()
        {
            return _db.UserProfile;
        }

        public async Task<UserProfile> Update(UserProfile entity)
        {
            _db.UserProfile.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

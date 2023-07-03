using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialOrdersInput
{
   public class UsersRepository : UserRepository, IUsersRepository
   {
      public UsersRepository(DbContext context) : base(context)
      {
      }
      public async Task<IQueryable<User>> GetUsersAsync()
      {
         var @user = _users.AsQueryable();
         return await Task.FromResult(user);
      }
   }
   public interface IUsersRepository :IUserRepository
   {
      Task<IQueryable<User>> GetUsersAsync();
   }
}

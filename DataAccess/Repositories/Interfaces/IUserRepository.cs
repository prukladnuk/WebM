//using WebMessangerAPI.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        public Task<bool> UserExistAsync(object id);
    }
}
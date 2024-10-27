//using WebMessangerAPI.DataAccess.Models;
using WebMessangerAPI.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebMessangerAPI.DataAccess.Repositories;
using DataAccess.Models;

namespace WebMessangerAPI.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
    }
}
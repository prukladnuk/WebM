//using WebMessangerAPI.DataAccess.Models;
using WebMessangerAPI.DataAccess.Repositories.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMessangerAPI.DataAccess.Repositories;

namespace WebMessangerAPI.DataAccess.Repositories.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        
    }
}
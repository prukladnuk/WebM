//using MessangerContextAPI.DataAccess;
//using WebMessangerAPI.DataAccess.Models;
using WebMessangerAPI.DataAccess.Repositories.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMessangerAPI.DataAccess.Repositories
{
    public class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(MessangerContext context) : base(context)
        {
        }
    }
}
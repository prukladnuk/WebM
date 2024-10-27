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
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(MessangerContext context) : base(context)
        {
        }
    }
}
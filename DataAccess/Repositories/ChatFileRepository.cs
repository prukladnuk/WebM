using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ChatFileRepository : GenericRepository<ChatFile>, IChatFileRepository
    {
        public ChatFileRepository(MessangerContext context) : base(context)
        {
        }
    }
}
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
    public class FileRepository : GenericRepository<File>, IFileRepository
    {
        public FileRepository(MessangerContext context) : base(context)
        {
        }
    }
}
﻿//using WebMessangerAPI.DataAccess.Models;
using WebMessangerAPI.DataAccess.Repositories.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebMessangerAPI.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {
        private readonly MessangerContext _context;
        public UserRepository(MessangerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UserExistAsync(object id)
        {
            return await _context.AspNetUsers.AnyAsync(user => user.Id == id);
        }
    }
}
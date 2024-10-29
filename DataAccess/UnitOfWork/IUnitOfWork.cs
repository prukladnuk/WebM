using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IChatFileRepository _chatRepository { get; }
        //IChatRepository _chatRepository { get; }
        //IMessageRepository _messageRepository { get; }
        //IUserRepository _userRepository { get; }

        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CommitAsync();
    }
}
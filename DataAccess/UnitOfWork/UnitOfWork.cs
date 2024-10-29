using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataAccess.UnitOfWork
{
    //public class UnitOfWork : IDisposable, IUnitOfWork
    //{
    //    private readonly AppDbContext _context; /*= new MessangerContext();*/

    //    public IChatFileRepository _chatRepository {  get; }

    //    public IChatRepository _chatRepository {  get; }

    //    public IMessageRepository _messageRepository { get; }

    //    public IUserRepository _userRepository { get; }

    //    public UnitOfWork(
    //    MessangerContext context,
    //    IChatFileRepository chatRepository,
    //    IChatRepository chatRepository,
    //    IMessageRepository messageRepository,
    //    IUserRepository userRepository)
    //    {
    //        _context = context;
    //        _chatfileRepository = chatfileRepository;
    //        _chatRepository = chatRepository;
    //        _messageRepository = messageRepository;
    //        _userRepository = userRepository;
    //    }
    //    public async Task<int> CommitAsync()
    //    {
    //        return await _context.SaveChangesAsync();
    //    }

    //    public void Dispose()
    //    {
    //        _context.Dispose();

    //        //if (!disposed)
    //        //{
    //        //    if (disposing)
    //        //    {
    //        //        _context.Dispose();
    //        //    }
    //        //}
    //        //disposed = true;
    //    }

    //}

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly MessangerContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(MessangerContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _repositories = new Dictionary<string, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var typeName = typeof(T).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var repository = _serviceProvider.GetRequiredService<IRepository<T>>();
                _repositories.Add(typeName, repository);
            }

            return (IRepository<T>)_repositories[typeName];
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
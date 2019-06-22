using EComerence.Core;
using EComerence.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Producers = new ProducerRepository(_context);
            Users = new UserRepository(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            OrderLists = new OrderListRepository(_context);
        }

        public IProducerRepository Producers { get; private set; }
        public IUserRepository Users { get; private set; }
        public IProductRepository Products  { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderListRepository OrderLists { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

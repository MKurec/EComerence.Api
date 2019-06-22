using EComerence.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        IProducerRepository Producers { get;}
        ICategoryRepository Categories { get; }
        IOrderListRepository OrderLists { get; }
        int Complete();
    }
}

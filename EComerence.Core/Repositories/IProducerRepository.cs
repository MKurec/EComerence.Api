using EComerence.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Core.Repositories
{
    public interface IProducerRepository
    {
        Task<IEnumerable<Producer>> BrowseAsync(string name = "");
        Task<Producer> GetAsync(string name);
        Task<Producer> GetAsync(Guid id);

        Task AddAsync(Producer producer);
        Task UpdateAsync(Producer producer);
        Task DeleteAsync(Producer producer);
    }
}

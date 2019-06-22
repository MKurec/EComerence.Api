using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface IProducerService
    {
        Task<ProducerDto> GetAsync(Guid id);
        Task<ProducerDto> GetAsync(string name);

        Task<IEnumerable<ProducerDto>> BrowseAsync(string name = null);
        Task UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
    }
}

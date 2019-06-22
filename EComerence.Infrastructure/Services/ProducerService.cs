using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IMapper _mapper;
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IMapper mapper, IProducerRepository producerRepository)
        {
            _mapper = mapper;
            _producerRepository = producerRepository;
        }
        public async Task<ProducerDto> GetAsync(Guid id)
        {
            var @producer = await _producerRepository.GetAsync(id);
            return _mapper.Map<ProducerDto>(@producer);
        }
        public async Task<ProducerDto> GetAsync(string name)
        {
            var @producer = await _producerRepository.GetAsync(name);
            return _mapper.Map<ProducerDto>(@producer);

        }
        public async Task<IEnumerable<ProducerDto>> BrowseAsync(string name = null)
        {
            var producers = await _producerRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<ProducerDto>>(producers);
        }


        public async Task UpdateAsync(Guid id, string name)
        {
            var @producer = await _producerRepository.GetOrFailAsync(id);
            @producer.SetName(name);
            await _producerRepository.UpdateAsync(@producer);
        }
        public async Task DeleteAsync(Guid id)
        {
            var @producer = await _producerRepository.GetOrFailAsync(id);
            await _producerRepository.DeleteAsync(@producer);
        }
    }
}

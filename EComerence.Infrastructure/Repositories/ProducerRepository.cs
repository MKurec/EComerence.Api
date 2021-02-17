using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Repositories
{
    public class ProducerRepository :IProducerRepository
    {
        protected readonly DbContext Context;
        private DbSet<Producer> producers;
        public ProducerRepository(DbContext context)
        {
            this.Context = context;
            this.producers = Context.Set<Producer>();
        }

        public async Task AddAsync(Producer @producer)
        {
            producers.Add(@producer);
            Context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Producer @producer)
        {
            Context.Entry(@producer).State = EntityState.Modified;
            Context.SaveChanges();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(Producer @producer)
        {
            producers.Remove(@producer);
            Context.SaveChanges();
            await Task.CompletedTask;

        }

        public async Task<IEnumerable<Producer>> BrowseAsync(string name = "")
        {
            var xproducers = producers.AsEnumerable();
            if (!string.IsNullOrEmpty(name))
            {
                xproducers = xproducers.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            return await Task.FromResult(xproducers);
        }

        public async Task<Producer> GetAsync(string name)
        {
            var @producer = await Task.FromResult(Context.Set<Producer>().SingleOrDefault(x => x.Name == name));
            return @producer;
        }

        public async Task<Producer> GetAsync(Guid id)
        {
            var @producer = await Task.FromResult(Context.Set<Producer>().SingleOrDefault(x => x.Id == id));
            return @producer;
        }
    }
}

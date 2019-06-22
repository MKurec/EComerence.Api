using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }

        public static async Task<Product> GetOrFailAsync(this IProductRepository repository, Guid id)
        {
            var product = await repository.GetAsync(id);
            if (product == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return product;
        }

        public static async Task<Producer> GetOrFailAsync(this IProducerRepository repository, Guid id)
        {
            var producer = await repository.GetAsync(id);
            if (producer == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return producer;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository repository, Guid id)
        {
            var category = await repository.GetAsync(id);
            if (category == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return category;
        }

        public static async Task<Producer> GetOrFailAsync(this IProducerRepository repository, string name)
        {
            var producer = await repository.GetAsync(name);
            if (producer == null)
            {
                throw new Exception($"User with id: '{name}' does not exist.");
            }

            return producer;
        }

        public static async Task<Category> GetOrFailAsync(this ICategoryRepository repository, string name)
        {
            var category = await repository.GetAsync(name);
            if (category == null)
            {
                throw new Exception($"User with id: '{name}' does not exist.");
            }

            return category;
        }

        public static async Task<Producer> SetOrGetExistingAsync(this IProducerRepository repository, string name)
        {
            var @producer = await repository.GetAsync(name);
            if (@producer == null)
            {
                var id = Guid.NewGuid();
                @producer = new Producer(id, name);
                await repository.AddAsync(@producer);
            }

            return @producer;
        }

        public static async Task<Category> SetOrGetExistingAsync(this ICategoryRepository repository, string name)
        {
            var @category = await repository.GetAsync(name);
            if (@category == null)
            {
                var id = Guid.NewGuid();
                @category = new Category(id, name);
                await repository.AddAsync(@category);
            }

            return @category;
        }
    }
}

using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid userId);
        Task RegisterAsync(Guid userId, string email,string name, string password, string city, string address, string postalCode);

        Task<TokenDto> LoginAsync(string email, string password);
    }
}

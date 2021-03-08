using AutoMapper;
using EComerence.Core.Domain;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.DTO;
using EComerence.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EComerence.Infrastructure.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<AccountDto>(user);
        }

        public async Task RegisterAsync(Guid userId, string email,   string firstName, string lastName, string password, string city, string address, string postalCode)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            var role = "user";
            user = new User(userId, firstName, lastName , email, password, role, city, address, postalCode);
            await _userRepository.AddAsync(user);
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            if (user.Password != password)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }
    }
}

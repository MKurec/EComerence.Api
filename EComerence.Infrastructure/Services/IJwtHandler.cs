using EComerence.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EComerence.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}

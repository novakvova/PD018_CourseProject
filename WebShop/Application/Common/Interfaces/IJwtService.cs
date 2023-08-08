using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email, string[] roles);
    }
}

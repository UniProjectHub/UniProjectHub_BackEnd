using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(Users user);
    }
}
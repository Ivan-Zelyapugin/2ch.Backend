using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2ch.Application.Interfaces
{
    public interface IHashingService
    {
        string GenerateDeterministicSalt(string ipAddress);
        string GenerateHash(string ipAddress, string salt);
    }
}

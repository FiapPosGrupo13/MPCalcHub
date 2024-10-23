using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPCalcHub.Application.DataTransferObjects;

namespace MPCalcHub.Application.Interfaces
{
    public interface ITokenApplicationService
    {
        Task<string> GetToken(UserLogin userLogin);
    }
}
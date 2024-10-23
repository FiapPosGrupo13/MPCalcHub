using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPCalcHub.Application.DataTransferObjects;

namespace MPCalcHub.Application.Interfaces
{
    public interface IUserApplicationService
    {   
        Task<User> Add(User model);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services
{
    public class UserApplicationService(IUserService userService) : IUserApplicationService
    {
        public async Task<User> Add(User model)
        {
            var user = new EN.User(model.Name, model.Email, model.Password, model.PermissionLevel, model.DDD, model.PhoneNumber, Guid.NewGuid());

            user = await userService.Add(user);

            return new User(user.Id, user.CreatedAt, user.CreatedBy, user.UpdatedAt, user.UpdatedBy, user.Removed, user.RemovedAt, user.RemovedBy, user.Name, user.Email, user.Password, user.PermissionLevel, user.DDD, user.PhoneNumber);
            
        }
    }
}
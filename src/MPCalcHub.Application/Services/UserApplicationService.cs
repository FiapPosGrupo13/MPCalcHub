using AutoMapper;
using MPCalcHub.Application.DataTransferObjects;
using MPCalcHub.Application.Interfaces;
using MPCalcHub.Domain.Interfaces;
using EN = MPCalcHub.Domain.Entities;

namespace MPCalcHub.Application.Services;

public class UserApplicationService(
    IUserService userService,
    IMapper mapper) : IUserApplicationService
{
    public async Task<User> Add(BasicUser model)
    {
        var user = mapper.Map<EN.User>(model);

        user = await userService.Add(user);

        return mapper.Map<User>(user);
    }
}

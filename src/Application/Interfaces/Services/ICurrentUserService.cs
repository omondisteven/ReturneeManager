using ReturneeManager.Application.Interfaces.Common;

namespace ReturneeManager.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}
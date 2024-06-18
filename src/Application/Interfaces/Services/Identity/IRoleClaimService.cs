using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Interfaces.Common;
using ReturneeManager.Application.Requests.Identity;
using ReturneeManager.Application.Responses.Identity;
using ReturneeManager.Shared.Wrapper;

namespace ReturneeManager.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}
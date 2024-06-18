using ReturneeManager.Application.Interfaces.Common;
using ReturneeManager.Application.Requests.Identity;
using ReturneeManager.Application.Responses.Identity;
using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}
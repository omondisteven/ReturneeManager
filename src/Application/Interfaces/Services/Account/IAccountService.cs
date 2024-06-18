using ReturneeManager.Application.Interfaces.Common;
using ReturneeManager.Application.Requests.Identity;
using ReturneeManager.Shared.Wrapper;
using System.Threading.Tasks;

namespace ReturneeManager.Application.Interfaces.Services.Account
{
    public interface IAccountService : IService
    {
        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model, string userId);

        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model, string userId);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}
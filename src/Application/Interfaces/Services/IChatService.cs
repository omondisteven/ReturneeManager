using ReturneeManager.Application.Responses.Identity;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Interfaces.Chat;
using ReturneeManager.Application.Models.Chat;

namespace ReturneeManager.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}
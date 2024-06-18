using ReturneeManager.Application.Models.Chat;
using ReturneeManager.Application.Responses.Identity;
using ReturneeManager.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReturneeManager.Application.Interfaces.Chat;

namespace ReturneeManager.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}
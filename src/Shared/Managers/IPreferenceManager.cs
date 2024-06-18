using ReturneeManager.Shared.Settings;
using System.Threading.Tasks;
using ReturneeManager.Shared.Wrapper;

namespace ReturneeManager.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}
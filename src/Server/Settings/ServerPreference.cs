using System.Linq;
using ReturneeManager.Shared.Constants.Localization;
using ReturneeManager.Shared.Settings;

namespace ReturneeManager.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}
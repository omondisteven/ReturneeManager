
using ReturneeManager.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace ReturneeManager.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}
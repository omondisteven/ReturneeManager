using ReturneeManager.Application.Interfaces.Serialization.Serializers;
using ReturneeManager.Application.Serialization.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ReturneeManager.Application.Serialization.Serializers
{
    public class NewtonSoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonSoftJsonSerializer(IOptions<NewtonsoftJsonSettings> settings)
        {
            _settings = settings.Value.JsonSerializerSettings;
        }

        public T Deserialize<T>(string text)
            => JsonConvert.DeserializeObject<T>(text, _settings);

        public string Serialize<T>(T obj)
            => JsonConvert.SerializeObject(obj, _settings);
    }
}
using System.Text.Json;
using ReturneeManager.Application.Interfaces.Serialization.Options;

namespace ReturneeManager.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}
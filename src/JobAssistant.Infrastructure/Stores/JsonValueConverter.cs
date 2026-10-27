using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobAssistant.Infrastructure.Stores;

internal class JsonValueConverter<T> : ValueConverter<T?, string?>
{
    public JsonValueConverter(JsonSerializerOptions? options = null)
        : base(value => value == null ? null : JsonSerializer.Serialize(value, options),
            json => json == null ? default : JsonSerializer.Deserialize<T>(json, options))
    {
    }
}
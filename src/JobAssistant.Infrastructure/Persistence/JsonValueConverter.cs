using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobAssistant.Infrastructure.Persistence;

internal class JsonValueConverter<T>(JsonSerializerOptions? options = null) : ValueConverter<T?, string?>(
    value => value == null ? null : JsonSerializer.Serialize(value, options),
    json => json == null ? default : JsonSerializer.Deserialize<T>(json, options));
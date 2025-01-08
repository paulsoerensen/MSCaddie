using System.Text.Json;

namespace MSCaddie.Client.Utils;

public static class SerializerOptions
{
    public static readonly JsonSerializerOptions Default = new JsonSerializerOptions(JsonSerializerDefaults.Web);

}
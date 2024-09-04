using System.Text.Json.Serialization;

namespace GitHub_Activity_CLI.models;

public class Repository
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("url")]
    public required string Url { get; set; }
}
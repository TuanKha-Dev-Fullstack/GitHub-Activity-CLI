using System.Text.Json.Serialization;

namespace GitHub_Activity_CLI.models;

public class Payload
{
    [JsonPropertyName("action")]
    public required string Action { get; set; }
}
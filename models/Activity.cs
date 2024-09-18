using System.Text.Json.Serialization;

namespace GitHub_Activity_CLI.models;

public class Activity
{
	[JsonPropertyName("id")]
	public required string Id { get; set; }
	[JsonPropertyName("type")]
    public required string Type { get; set; }
	[JsonPropertyName("repo")]
    public required Repository Repository { get; set; }
}

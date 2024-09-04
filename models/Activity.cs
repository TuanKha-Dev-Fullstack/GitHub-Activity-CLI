using System.Text.Json.Serialization;

namespace GitHub_Activity_CLI.models;

public class Activity
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("type")]
    public required string Type { get; set; }
    public required Actor Actor { get; set; }
    public required Repository Repository { get; set; }
    public required Payload Payload { get; set; }
}

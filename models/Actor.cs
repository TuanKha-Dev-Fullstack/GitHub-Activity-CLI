using System.Text.Json.Serialization;

namespace GitHub_Activity_CLI.models;

public class Actor
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("login")] 
    public required string Login { get; set; }
    [JsonPropertyName("display_login")]
    public required string DisplayLogin { get; set; }
    [JsonPropertyName("gravatar_id")]
    public string GravatarId { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public required string Url { get; set; }
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; } = string.Empty;
}
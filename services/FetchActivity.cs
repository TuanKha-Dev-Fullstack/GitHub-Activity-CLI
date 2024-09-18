using System.Text.Json;
using GitHub_Activity_CLI.enums;
using GitHub_Activity_CLI.models;
using static System.Enum;

namespace GitHub_Activity_CLI.services;

public static class FetchActivity
{
    /// <summary>
    /// Fetches GitHub activities and groups them by repository.
    /// </summary>
    /// <param name="username"></param>
    public static async Task Fetch(string username)
    {
        var activities = await FetchGitHubActivities(username);
        if (activities != null)
        {
            var groupedEvents = GroupActivitiesByRepository(activities);
            PrintActivitySummary(groupedEvents);
        }
    }
    /// <summary>
    /// Fetches GitHub activities from the GitHub API.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>
    /// A list of GitHub activities if successful, null otherwise.
    /// </returns>
    private static async Task<List<Activity>?> FetchGitHubActivities(string username)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Github-Activity-CLI");
        try
        {
            var response = await client.GetAsync($"https://api.github.com/users/{username}/events");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error while fetching data: " + response.StatusCode);
                return null;
            }
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Activity>>(data);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception while fetching data: {e}");
            return null;
        }
    }
    /// <summary>
    /// Groups GitHub activities by repository and event type.
    /// </summary>
    /// <param name="activities"></param>
    /// <returns>
    /// A dictionary of GitHub activities grouped by repository and event type.
    /// </returns>
    private static Dictionary<string, Dictionary<TypeEvents, int>> GroupActivitiesByRepository(
        List<Activity> activities)
    {
        var repositoryEvents = new Dictionary<string, Dictionary<TypeEvents, int>>();
        foreach (var activity in activities)
        {
            // Parse the type of event
            if (!TryParse(activity.Type, out TypeEvents eventType)) continue;
            // Get the repository name
            var repoName = activity.Repository.Name;
            // Initialize the dictionary if not already present
            if (!repositoryEvents.TryGetValue(repoName, out Dictionary<TypeEvents, int>? value))
            {
                value = new Dictionary<TypeEvents, int>();
                repositoryEvents[repoName] = value;
            }
            // Initialize the event type count if not already present
            value.TryAdd(eventType, 0);
            value[eventType]++;
        }
        return repositoryEvents;
    }

    /// <summary>
    /// Displays the summary of GitHub activities grouped by repository and event type.
    /// </summary>
    /// <param name="groupedEvents"></param>
    private static void PrintActivitySummary(Dictionary<string, Dictionary<TypeEvents, int>> groupedEvents)
    {
        // Predefined messages for each event type
        var eventMessages = new Dictionary<TypeEvents, string>
        {
            { TypeEvents.PushEvent, "Pushed {0} commits to {1}" },
            { TypeEvents.IssuesEvent, "Opened {0} new issues in {1}" },
            { TypeEvents.CreateEvent, "Created {0} new resources in {1}" },
            { TypeEvents.DeleteEvent, "Deleted {0} items in {1}" },
            { TypeEvents.MemberEvent, "Added {0} members to {1}" },
            { TypeEvents.PublicEvent, "Made {1} public {0} times" },
            { TypeEvents.PullRequestEvent, "Opened {0} pull requests in {1}" },
            { TypeEvents.CommitCommentEvent, "Added {0} commit comments in {1}" },
            { TypeEvents.ForkEvent, "Forked {1} {0} times" },
            { TypeEvents.GollumEvent, "Edited {0} wiki pages in {1}" },
            { TypeEvents.IssueCommentEvent, "Commented on {0} issues in {1}" },
            { TypeEvents.PullRequestReviewEvent, "Reviewed {0} pull requests in {1}" },
            { TypeEvents.PullRequestReviewCommentEvent, "Added {0} comments to pull request reviews in {1}" },
            { TypeEvents.PullRequestReviewThreadEvent, "Created {0} review threads in {1}" },
            { TypeEvents.ReleaseEvent, "Published {0} releases in {1}" },
            { TypeEvents.SponsorshipEvent, "Engaged in {0} sponsorships in {1}" },
            { TypeEvents.WatchEvent, "Starred {1} {0} times" }
        };
        foreach (var (repoName, value) in groupedEvents)
        {
            foreach (var (eventType, eventCount) in value)
            {
                // Check if eventMessages contains the current eventType
                Console.WriteLine(eventMessages.TryGetValue(eventType, out var message)
                    ? $"- {string.Format(message, eventCount, repoName)}"
                    : $"- {eventType} occurred {eventCount} times in {repoName}");
            }
        }
        Console.WriteLine();
    }
}
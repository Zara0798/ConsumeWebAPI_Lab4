using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// metod för att hämta och visa GitHub-repos
public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        await FetchAndDisplayGitHubRepositories();
    }

    private static async Task FetchAndDisplayGitHubRepositories()
    {
        string url = "https://api.github.com/orgs/dotnet/repos";
        client.DefaultRequestHeaders.Add("User-Agent", "ConsumeWebAPIApp");

        string response = ""; // Deklarera variabeln utanför try-blocket

        try
        {
            response = await client.GetStringAsync(url); // Tilldela värde i try-blocket
            Console.WriteLine("Data hämtad!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning: {ex.Message}");
        }

        // Deserialisera JSON-data
        var repositories = JsonSerializer.Deserialize<List<Repository>>(response);

        if (repositories != null)
        {
            foreach (var repo in repositories)
            {
                Console.WriteLine($"Namn: {repo.Name}");
                Console.WriteLine($"Beskrivning: {repo.Description}");
                Console.WriteLine($"URL: {repo.HtmlUrl}");
                Console.WriteLine($"Hemsida: {repo.Homepage ?? "Ingen hemsida"}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Senast uppdaterad: {repo.PushedAt}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}

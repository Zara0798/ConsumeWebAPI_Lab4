using ConsumeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class Program
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main(string[] args)
    {
        // Kör G-delen
        await FetchAndDisplayGitHubRepositories();

        // Separator mellan G- och VG-delen
        Console.WriteLine(new string('=', 50));

        // Kör VG-delen
        await FetchAndDisplayZipData();
    }

    private static async Task FetchAndDisplayGitHubRepositories()
    {
        string url = "https://api.github.com/orgs/dotnet/repos";
        client.DefaultRequestHeaders.Clear(); // Clear any existing headers
        client.DefaultRequestHeaders.Add("User-Agent", "ConsumeWebAPIApp");

        try
        {
            Console.WriteLine("Hämtar GitHub-data...");
            var response = await client.GetStringAsync(url);
            Console.WriteLine("GitHub-data hämtad!");
            Console.WriteLine();

            var repositories = JsonSerializer.Deserialize<List<Repository>>(response);

            if (repositories != null)
            {
                foreach (var repo in repositories)
                {
                    Console.WriteLine($"Name: {repo.Name}");
                    Console.WriteLine($"Homepage: {repo.Homepage ?? "None"}");
                    Console.WriteLine($"GitHub: {repo.HtmlUrl}");
                    Console.WriteLine($"Description: {repo.Description ?? "No description available"}");
                    Console.WriteLine($"Watchers: {repo.Watchers}");
                    Console.WriteLine($"Last push: {repo.PushedAt:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning: {ex.Message}");
        }
    }

    private static async Task FetchAndDisplayZipData()
    {
        string zipUrl = "https://api.zippopotam.us/us/07645";
        client.DefaultRequestHeaders.Clear(); // Clear any existing headers
        client.DefaultRequestHeaders.Add("User-Agent", "ConsumeWebAPIApp");

        try
        {
            Console.WriteLine("Hämtar platsdata från Zippopotamus API...");
            var response = await client.GetStringAsync(zipUrl);
            Console.WriteLine("Platsdata hämtad!");
            Console.WriteLine();

            var location = JsonSerializer.Deserialize<Location>(response);

            if (location != null)
            {
                Console.WriteLine($"Postnummer: {location.PostCode}");
                Console.WriteLine($"Land: {location.Country} ({location.CountryAbbreviation})");
                Console.WriteLine();

                foreach (var place in location.Places)
                {
                    Console.WriteLine($"Plats: {place.PlaceName}");
                    Console.WriteLine($"Delstat: {place.State} ({place.StateAbbreviation})");
                    Console.WriteLine($"Latitude: {place.Latitude}");
                    Console.WriteLine($"Longitude: {place.Longitude}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning: {ex.Message}");
        }
    }
}


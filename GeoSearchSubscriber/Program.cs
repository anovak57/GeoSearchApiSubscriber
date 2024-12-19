using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting SignalR client...");
        
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true)
            .Build();

        var hubUrl = configuration["SignalR:HubUrl"]!;
        var apiKey = configuration["SignalR:ApiKey"]!;

        var connection = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                options.Headers.Add("x-api-key", apiKey);
            })
            .Build();

        connection.On<LocationSearchRequest>("ReceiveSearchRequest", searchRequest =>
        {
            Console.WriteLine($"\nReceived search request: \n" +
                              $"--Latitude: {searchRequest.Latitude},\n" +
                              $"--Longitude: {searchRequest.Longitude},\n" +
                              $"--Query: {searchRequest.Query},\n" +
                              $"--Radius: {searchRequest.Radius}");

            if (searchRequest?.Locations != null && searchRequest.Locations.Any())
            {
                Console.WriteLine("--Locations:");
                foreach (var location in searchRequest.Locations)
                {
                    Console.WriteLine($"   - Name: {location.Name}");
                    Console.WriteLine($"     Latitude: {location.Latitude}");
                    Console.WriteLine($"     Longitude: {location.Longitude}");
                    Console.WriteLine($"     Address: {location.Address}");
                    Console.WriteLine($"     City: {location.City}");
                    Console.WriteLine($"     Region: {location.Region}");
                    Console.WriteLine($"     PostalCode: {location.PostalCode}");
                    Console.WriteLine($"     Categories: {string.Join(", ", location.Categories)}");
                    Console.WriteLine("     ------------------------");
                }
            }
            else
            {
                Console.WriteLine("--Locations: None");
            }
            Console.WriteLine("==================================");
        });

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to SignalR hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to hub: {ex.Message}");
            return;
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }
}

public class LocationSearchRequest
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Query { get; set; }
    public int Radius { get; set; }
    public IEnumerable<Location> Locations { get; set; }
}

public class Location
{
    public string? Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public List<string> Categories { get; set; }
}
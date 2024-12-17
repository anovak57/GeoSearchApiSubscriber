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
                              $"--Radius: {searchRequest.Radius}\n" +
                              $"==================================");
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
}
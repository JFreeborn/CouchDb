// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text;
using CouchDb;

const string serverUrl = "http://IP ADDRESS OF VM:5984";
const string dbName = "";
const string username = "";
const string password = "";

var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

var request = new UserObject("SomeName");

try
{
    using var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        
    var createDbResponse = await client.PutAsync($"{serverUrl}/{dbName}", null);
        
    if (!createDbResponse.IsSuccessStatusCode)
    {
        Console.WriteLine($"Failed to create database: {createDbResponse.ReasonPhrase}");
        return;
    }
        
    Console.WriteLine("Database created successfully.");
    
    var content = new StringContent(request.ToString()!, Encoding.UTF8, "application/json");
    
    var response = await client.PostAsync($"{serverUrl}/{dbName}", content);

    Console.WriteLine(response.IsSuccessStatusCode
        ? "Document created successfully."
        : $"Failed to create document: {response.ReasonPhrase}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
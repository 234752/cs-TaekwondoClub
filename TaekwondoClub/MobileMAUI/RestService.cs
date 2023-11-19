using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MobileMAUI;

public class RestService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public List<Customer> Customers { get; set; }
    public List<Event> Events { get; set; }
    public List<Payment> Payments { get; set; }

    public RestService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task FetchDataContext()
    {

        Uri uri = new Uri(string.Format("https://pogapi.azurewebsites.net/", string.Empty));

        HttpResponseMessage customersResponse = await _client.GetAsync(uri + "customers");
        HttpResponseMessage eventsResponse = await _client.GetAsync(uri + "events");
        HttpResponseMessage paymentsResponse = await _client.GetAsync(uri + "payments");

        if (customersResponse.IsSuccessStatusCode)
        {
            string content = await customersResponse.Content.ReadAsStringAsync();
            Customers = JsonSerializer.Deserialize<List<Customer>>(content, _serializerOptions);
        }

        if (eventsResponse.IsSuccessStatusCode)
        {
            string content = await eventsResponse.Content.ReadAsStringAsync();
            Events = JsonSerializer.Deserialize<List<Event>>(content, _serializerOptions);
        }

        if (paymentsResponse.IsSuccessStatusCode)
        {
            string content = await paymentsResponse.Content.ReadAsStringAsync();
            Payments = JsonSerializer.Deserialize<List<Payment>>(content, _serializerOptions);
        }
    }

    public async Task SendEmailToDuePayments(List<Payment> payments)
    {
        Uri uri = new Uri(string.Format("https://pogapi.azurewebsites.net/email/duepayments", string.Empty));
        await _client.PostAsJsonAsync(uri, payments);
    }

}

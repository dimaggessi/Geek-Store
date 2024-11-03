using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekStore.Infrastructure.Services;
public sealed class DeliveryService : IDeliveryService
{
    private string? Postal { get; init; }
    private string? Url { get; init; }
    private string? Email { get; init; }
    private string? Token { get; init; }
    private string? Services { get; set; }

    public DeliveryService(IConfiguration config)
    {
        Postal = config["MelhorEnvio:PostalCode"];
        Url = config["MelhorEnvio:Url"];
        Email = config["MelhorEnvio:Email"];
        Token = config["MelhorEnvio:Token"];
        Services = config["MelhorEnvio:ChosenServices"];
    }

    public async Task<List<DeliveryMethod>> DeliveryMethods(List<Product> products, string postalCode, int? id = default)
    {
        CheckPropertiesInitialization();

        var productsItems = products.Select(p => new
        {
            Id = p.Id,
            Quantity = p.Quantity,
            Price = p.Price,
            Width = p.Width,
            Height = p.Height,
            Length = p.Length,
            Weight = p.Weight
        });

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Url!),
            Headers =
            {
                { "Accept", "application/json" },
                { "Authorization", $"Bearer {Token}" },
            }
        };

        var requestBody = new
        {
            from = new { postal_code = Postal },
            to = new { postal_code = postalCode },
            products = productsItems,
            services = id.HasValue ? id.ToString() : Services
        };

        string jsonBody = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        request.Content = new StringContent(jsonBody);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode || response.Content is null)
        {
            Console.WriteLine("Error while trying to obtain delivery methods: ");
            return new List<DeliveryMethod>();
        }

        var body = await response.Content.ReadAsStringAsync();

        var deliveryMethods = new List<DeliveryMethod>();

        using (JsonDocument doc = JsonDocument.Parse(body))
        {
            JsonElement root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement delivery in root.EnumerateArray())
                {
                    deliveryMethods.Add(ParseDeliveryMethod(delivery));
                }
            }

            else if (root.ValueKind == JsonValueKind.Object)
            {
                deliveryMethods.Add(ParseDeliveryMethod(root));
            }
        }

        return deliveryMethods;
    }

    private void CheckPropertiesInitialization()
    {
        if (string.IsNullOrWhiteSpace(Url) ||
        string.IsNullOrWhiteSpace(Token) ||
        string.IsNullOrWhiteSpace(Email) ||
        string.IsNullOrWhiteSpace(Postal) ||
        string.IsNullOrWhiteSpace(Services))
            throw new InvalidOperationException("Undefined delivery service variables");
    }

    private DeliveryMethod ParseDeliveryMethod(JsonElement delivery)
    {
        return new DeliveryMethod
        {
            Id = delivery.TryGetProperty("id", out JsonElement dmId) ? ConvertToInt(dmId) : 0,
            Name = delivery.TryGetProperty("name", out JsonElement dmName) ? dmName.ToString() : string.Empty,
            Price = delivery.TryGetProperty("price", out JsonElement dmPrice) ? ConvertToDecimal(dmPrice) : 0m,
            DeliveryTimeInDays = delivery.TryGetProperty("delivery_time", out JsonElement dmTime) ? ConvertToInt(dmTime) : 0,
            Discount = delivery.TryGetProperty("discount", out JsonElement dmDiscount) ? ConvertToDecimal(dmDiscount) : 0m,
            Currency = delivery.TryGetProperty("currency", out JsonElement dmCurrency) ? dmCurrency.ToString() : string.Empty
        };
    }

    private static int ConvertToInt(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.String)
        {
            int.TryParse(element.GetString(), out int result);
            return result;
        }
        if (element.ValueKind == JsonValueKind.Number)
        {
            return element.GetInt32();
        }
        return 0;
    }

    private static decimal ConvertToDecimal(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.String)
        {
            decimal.TryParse(element.GetString(), out decimal result);
            return result;
        }
        if (element.ValueKind == JsonValueKind.Number)
        {
            return element.GetDecimal();
        }
        return 0m;
    }
}
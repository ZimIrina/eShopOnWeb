using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services;

public interface IOrderDeliveryService
{
    void Delivery(Order order);
}

public class OrderDeliveryService : IOrderDeliveryService
{
    private readonly DeliverySettings _settings;
    public OrderDeliveryService(DeliverySettings settings)
    { 
        _settings = settings;
    }

    public void Delivery(Order order)
    {
        var httpClient = new HttpClient();
        HttpRequestMessage httpRequest2 = new HttpRequestMessage(HttpMethod.Post,
           _settings.DeliveryTriggerString);
        httpRequest2.Content = new StringContent(JsonSerializer.Serialize<Order>(order), Encoding.UTF8, "application/json");
        HttpResponseMessage response2 = httpClient.Send(httpRequest2);
    }
}

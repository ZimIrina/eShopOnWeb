using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Web.Services
{
    public interface IOrderItemsReserverService
    {
        Task Reserve(Order order);
}
    public class OrderItemsReserverService: IOrderItemsReserverService
    {
        private string _serviceBusConnStr;
        private string _queueName;

        public OrderItemsReserverService(OrderItemReserverSettings orderItemSettings)
        {
            _serviceBusConnStr = orderItemSettings.OrderItemServiceBusConnection;
            _queueName = orderItemSettings.QueueName;
        }

        public async Task Reserve(Order order)
        {
            // Create a ServiceBusClient object using the connection string to the namespace.
            await using var client = new ServiceBusClient(_serviceBusConnStr);

            // Create a ServiceBusSender object by invoking the CreateSender method on the ServiceBusClient object, and specifying the queue name. 
            ServiceBusSender sender = client.CreateSender(_queueName);

            // Create a new message to send to the queue.
            string messageContent = JsonSerializer.Serialize<Order>(order);
            var message = new ServiceBusMessage(messageContent);

            // Send the message to the queue.
            await sender.SendMessageAsync(message);
        }
    }
}

using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace eShopFunctionApp
{
    public static class OrderItemsReceiver
    {
        [FunctionName("OrderItemsReceiver")]
        public static void Run(
            [ServiceBusTrigger("shoporders", Connection = "ServiceBusConn")]
            string myQueueItem,
            [Blob("orders/{rand-guid}", FileAccess.Write, Connection = "BlobOrderConn")] out string orders,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            try 
            {
                log.LogInformation(myQueueItem);
                orders = myQueueItem;
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}

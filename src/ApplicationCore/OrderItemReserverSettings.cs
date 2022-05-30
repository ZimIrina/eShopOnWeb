using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore;
public class OrderItemReserverSettings
{
    public string OrderItemServiceBusConnection { get; set; }
    public string QueueName { get; set; }
}

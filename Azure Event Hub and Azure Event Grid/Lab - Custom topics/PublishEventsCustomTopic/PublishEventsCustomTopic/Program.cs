using Azure;
using Azure.Messaging.EventGrid;
using Newtonsoft.Json;
using PublishEventsCustomTopic;

Uri topicEndpoint = new Uri("https://ordertopic.northeurope-1.eventgrid.azure.net/api/events");
AzureKeyCredential azureKeyCredential = new AzureKeyCredential("rDrrQuc1JdyC8r2poCS5pSiI6uNcKM9gWze6xe6V3yA=");

EventGridPublisherClient eventGridPublisherClient = new EventGridPublisherClient(topicEndpoint, azureKeyCredential);

List<Order> orders = new List<Order>()
{
    new Order() { OrderID="O1",UnitPrice=9.99f,Quantity=100},
    new Order() { OrderID = "O2", UnitPrice = 10.99f, Quantity = 200 },
    new Order() { OrderID = "O3", UnitPrice = 11.99f, Quantity = 300 }
};

List<EventGridEvent> eventGridList = new List<EventGridEvent>();
string subject = "Adding new order";
string eventType = "app.neworder";
string dataVersion = "1.0";
foreach(Order order in orders)
{
    EventGridEvent eventGridEvent = new EventGridEvent(subject, eventType, dataVersion, JsonConvert.SerializeObject(order));
    eventGridList.Add(eventGridEvent);
}

await eventGridPublisherClient.SendEventsAsync(eventGridList);
Console.WriteLine("Events sent");
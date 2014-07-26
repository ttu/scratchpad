using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace KatanaHost
{
    [HubName("bugs")]
    public class BugHub : Hub
    {
    }
}
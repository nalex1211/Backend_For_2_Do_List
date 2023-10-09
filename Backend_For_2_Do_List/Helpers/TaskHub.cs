using Microsoft.AspNetCore.SignalR;

namespace Backend_For_2_Do_List.Helpers;

public class TaskHub : Hub
{
    public async Task SendTasks(string tasks)
    {
        await Clients.All.SendAsync("ReceiveTasks", tasks);
    }
}
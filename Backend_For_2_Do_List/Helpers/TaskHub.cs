using Microsoft.AspNetCore.SignalR;

namespace Backend_For_2_Do_List.Helpers;

//public class TaskHub : Hub
//{
//    public async Task SendTasks(string tasks)
//    {
//        await Clients.All.SendAsync("ReceiveTasks", tasks);
//    }
//}

public class TaskHub : Hub
{
    private static List<string> tasks = new List<string>();

    public async Task SendTasks(string updatedTasks)
    {
        tasks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(updatedTasks);
        await Clients.All.SendAsync("ReceiveTasks", updatedTasks);
    }

    public async Task TaskAdded(string task)
    {
        tasks.Add(task);
        await Clients.All.SendAsync("TaskAdded", task);
    }

    public async Task TaskEdited(string oldTask, string newTask)
    {
        int index = tasks.IndexOf(oldTask);
        if (index != -1)
        {
            tasks[index] = newTask;
            await Clients.All.SendAsync("TaskEdited", new { oldTask, newTask });
        }
    }

    public async Task TaskDeleted(string task)
    {
        tasks.Remove(task);
        await Clients.All.SendAsync("TaskDeleted", task);
    }

    public async Task TaskMoved(List<string> updatedTasks)
    {
        tasks = updatedTasks;
        string tasksJson = Newtonsoft.Json.JsonConvert.SerializeObject(tasks);
        await Clients.All.SendAsync("ReceiveTasks", tasksJson);
    }
}
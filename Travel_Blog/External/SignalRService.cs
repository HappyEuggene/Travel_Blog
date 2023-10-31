using Microsoft.AspNetCore.SignalR;

namespace Travel_Blog.External;

public class SignalRService : Hub
{
    public async Task UpdateBlog()
    {
        await Clients.All.SendAsync("UpdatePage");
    }
}

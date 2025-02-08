using GeekStore.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace GeekStore.API.SignalR;

[Authorize]
public class NotificationHub : Hub
{
    // Using Redis would be more scalable approach for a real application

    private static readonly ConcurrentDictionary<string, string> UserConnections = new();

    public override Task OnConnectedAsync()
    {
        var email = Context.User?.GetUserEmail();

        if (!string.IsNullOrEmpty(email))
        {
            UserConnections[email] = Context.ConnectionId;
        }

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var email = Context.User?.GetUserEmail();

        if (!string.IsNullOrEmpty(email))
        {
            UserConnections.TryRemove(email, out _);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public static string? GetConnectionIdByEmail(string email)
    {
        UserConnections.TryGetValue(email, out var connectionId);

        return connectionId;
    }

}

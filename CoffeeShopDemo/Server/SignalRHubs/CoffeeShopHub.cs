using Microsoft.AspNetCore.SignalR;

namespace CoffeeShopDemo.Server.SignalRHubs;

public class CoffeeShopHub : Hub
{
    public async Task UpdateCoffeeMakerStatus(int coffeeId, int orderId, string message, int status)
    {
        await Clients.All.SendAsync("UpdateCoffeeMakerStatus", coffeeId, orderId, message, status);
    }

    public async Task UpdateCoffeeProcessStatus(int coffeeId, int orderId, string message, int status)
    {
        await Clients.All.SendAsync("UpdateCoffeeProcessStatus", coffeeId, orderId, message, status);
    }

    public async Task UpdateCoffeeOrderStatus(int orderId, string message, int status)
    {
        await Clients.All.SendAsync("UpdateCoffeeOrderStatus", orderId, message, status);
    }
}

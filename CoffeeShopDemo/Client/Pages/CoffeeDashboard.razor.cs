using CoffeeShopDemo.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace CoffeeShopDemo.Client.Pages;

public partial class CoffeeDashboard
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private HubConnection hubConnection;

    private CoffeeOrder coffeeOrder = new() { Id = int.MinValue };

    private bool coffeeOrderStatusMessageCollapsed = true;

    private string coffeeOrderStatusAlertClass = "alert-primary";

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/coffeeshophub"))
        .Build();

        await hubConnection.StartAsync();

        hubConnection.On<int, string, int>("UpdateCoffeeOrderStatus", (orderId, message, status) =>
        {
            if (coffeeOrder.Id == int.MinValue)
            {
                coffeeOrderStatusMessageCollapsed = false;

                coffeeOrder = new CoffeeOrder()
                {
                    Id = orderId,
                    Status = status,
                    Message = message,
                    Coffees = new List<Coffee>() { }
                };
            }
            else
            {
                if(status == 3)
                {
                    coffeeOrderStatusAlertClass = "alert-success";
                }

                coffeeOrder.Message = message;
            }

            StateHasChanged();
        });

        hubConnection.On<int, int, string, int>("UpdateCoffeeMakerStatus", (coffeeId, orderId, message, status) =>
        {
            Coffee coffee = null;

            if (!coffeeOrder.Coffees.Any(c => c.Id == coffeeId))
            {
                coffee = new Coffee()
                {
                    Id = coffeeId,
                    OrderId = orderId,
                    Status = status,
                    AlertClass = "alert-primary"
                };

                coffeeOrder.Coffees.Add(coffee);
            }

            var coffeeInlist = coffeeOrder.Coffees.FirstOrDefault(c => c.Id == coffeeId);

            if (coffeeInlist != null)
            {
                coffeeInlist.Message = message;

                if (status == 4)
                {
                    coffeeInlist.AlertClass = "alert-success";
                }
                else if (status == 5)
                {
                    coffeeInlist.AlertClass = "alert-danger";
                }
            }
            
            StateHasChanged();
        });

        hubConnection.On<int, int, string, int>("UpdateCoffeeProcessStatus", (coffeeId, orderId, message, status) =>
        {            
            CoffeeProcessStep coffeeProcessStep = new CoffeeProcessStep()
            {
                Id = status,
                CoffeeId = coffeeId,
                Message = message,
            };

            var coffeeInlist = coffeeOrder.Coffees.FirstOrDefault(c => c.Id == coffeeId);

            if (coffeeInlist != null)
            {
                coffeeInlist.CoffeeProcessSteps.Add(coffeeProcessStep);
            }
            
            StateHasChanged();
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }
}


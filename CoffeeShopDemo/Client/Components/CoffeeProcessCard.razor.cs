using CoffeeShopDemo.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CoffeeShopDemo.Client.Components;

public partial class CoffeeProcessCard
{
    [Parameter]
    public CoffeeProcessStep CoffeeProcessStep { get; set; }

    public CoffeeProcessCard()
    {
        CoffeeProcessStep = new CoffeeProcessStep();
    }
}

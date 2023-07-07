namespace CoffeeShopDemo.Shared.Models;

public class Coffee
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int Status { get; set; }

    public string Message { get; set; }

    public string AlertClass { get; set; }

    public List<CoffeeProcessStep> CoffeeProcessSteps { get; set; } = new() { };
}

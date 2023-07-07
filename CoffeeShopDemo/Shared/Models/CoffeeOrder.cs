namespace CoffeeShopDemo.Shared.Models;

public class CoffeeOrder
{
    public int Id { get; set; }

    public List<Coffee> Coffees { get; set; } = new() { };

    public int Status { get; set; }

    public int PaymentStatus { get; set; }

    public string Message { get; set; }

    public decimal TotalCost { get; set; }
}

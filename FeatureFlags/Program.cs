using System;
using ConfigCat;
using ConfigCat.Client;

//Item to be purchased
public record Item
{
    public decimal Price { get; set; }

    //can have name, id, description etc.
}

//Handles all the items
public class Cart
{
    private readonly bool _discount;


    public Cart(bool discount)
    {
        _discount = discount;
    }

    public decimal CalculateTotal(List<Item> purchases)
    {

        decimal totalAmount = 0;

        foreach (var purchase in purchases)
        {
            totalAmount += purchase.Price;


        }

        if (_discount)
        {
            totalAmount *= 0.9m; // Apply 10% discount
        }

        return totalAmount;

    }
}


public class Program
{

        static void Main()
        {


            User user = new User("1275") // Unique identifier is required.
            {

                Custom =
                {
                    { "Type", "Member"}, //Member or Non-Member
                }
            };

            var client = ConfigCatClient.Get("YOUR-SDK-KEY");

            var discountApplies = client.GetValue("discountApplies", false, user);

            Console.WriteLine("discountApplies's value from ConfigCat: " + discountApplies);

            Cart newCart = new Cart(discountApplies);

            // Simulate multiple purchases
            List<Item> purchases = new List<Item>
            {
                new Item { Price = 100 },
                new Item { Price = 50},
                new Item { Price = 50}
            };

        //Calculate and display the total amount to be paid
        Console.WriteLine("Total Amount to Pay Today: " + newCart.CalculateTotal(purchases));

    }
}

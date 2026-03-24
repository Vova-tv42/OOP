/*
    Завдання 2. Оформлення замовлення (Facade Pattern)
    Рішення: Для приховування складної взаємодії з різними підсистемами було створено клас OrderFacade.
    Він надає єдиний простий метод PlaceOrder.
*/

using System;

public class InventorySystem
{
    public bool CheckStock(string itemId)
    {
        Console.WriteLine($"[Склад] Перевірка наявності товару {itemId}");
        return true;
    }

    public void ReserveItem(string itemId)
    {
        Console.WriteLine($"[Склад] Товар {itemId} заброньовано.");
    }
}

public class PricingSystem
{
    public double CalculateTotal(string itemId)
    {
        return 1000.00;
    }

    public double ApplyDiscount(double amount, string promoCode)
    {
        bool hasPromo = !string.IsNullOrEmpty(promoCode);
        if (hasPromo)
        {
            Console.WriteLine($"[Ціна] Застосовано знижку за промокодом {promoCode}.");
            return amount * 0.9;
        }
        return amount;
    }
}

public class PaymentSystem
{
    public bool ProcessPayment(double amount)
    {
        Console.WriteLine($"[Оплата] Знято {amount} грн.");
        return true;
    }
}

public class DeliverySystem
{
    public void ArrangeDelivery(string itemId, string address)
    {
        Console.WriteLine($"[Доставка] Оформлено доставку товару за адресою: {address}.");
    }
}

public class OrderFacade
{
    private readonly InventorySystem _inventory;
    private readonly PricingSystem _pricing;
    private readonly PaymentSystem _payment;
    private readonly DeliverySystem _delivery;

    public OrderFacade()
    {
        _inventory = new InventorySystem();
        _pricing = new PricingSystem();
        _payment = new PaymentSystem();
        _delivery = new DeliverySystem();
    }

    public bool PlaceOrder(string itemId, string promoCode, string address)
    {
        Console.WriteLine("=== Початок оформлення замовлення ===");
        
        bool inStock = _inventory.CheckStock(itemId);
        if (!inStock) return false;

        _inventory.ReserveItem(itemId);

        double basePrice = _pricing.CalculateTotal(itemId);
        double finalPrice = _pricing.ApplyDiscount(basePrice, promoCode);

        bool isPaid = _payment.ProcessPayment(finalPrice);
        if (isPaid)
        {
            _delivery.ArrangeDelivery(itemId, address);
            Console.WriteLine("=== Замовлення успішно оформлено! ===");
        }

        return isPaid;
    }
}

class Program
{
    static void Main()
    {
        OrderFacade facade = new OrderFacade();
        facade.PlaceOrder("Товар", "promo", "Київ");
    }
}

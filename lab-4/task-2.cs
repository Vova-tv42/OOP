/*
    Завдання 2. Вибір способу оплати (Strategy Pattern)
    Рішення: Застосовано патерн "Стратегія". Інтерфейс IPaymentStrategy має спільний метод Pay.
    Кожен спосіб оплати реалізує його по-своєму. Клас кошика ShoppingCart використовує цей інтерфейс, 
    тому йому не потрібні "switch" конструкції.
*/

using System;

public interface IPaymentStrategy
{
    void Pay(double amount);
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} грн успішно здійснена через PayPal");
    }
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} грн списана з кредитної картки");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} грн проведена криптовалютою");
    }
}

public class ShoppingCart
{
    private IPaymentStrategy _paymentStrategy;

    public ShoppingCart(IPaymentStrategy paymentStrategy) => SetPaymentStrategy(paymentStrategy);

    public void SetPaymentStrategy(IPaymentStrategy strategy) => _paymentStrategy = strategy;

    public void Checkout(double amount)
    {
        if (_paymentStrategy == null)
        {
            Console.WriteLine("Помилка: Спосіб оплати не обрано");
            return;
        }

        Console.WriteLine("\nПочаток оформлення замовлення...");
        _paymentStrategy.Pay(amount);
        Console.WriteLine("Замовлення успішно оформлене");
    }
}

class Program
{
    static void Main()
    {
        ShoppingCart cart = new ShoppingCart(new CreditCardPayment());
        cart.Checkout(1500.50);

        cart.SetPaymentStrategy(new CryptoPayment());
        cart.Checkout(5000);
    }
}

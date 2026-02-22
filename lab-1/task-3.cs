/*
    Порушено принцип Open/Closed.
    Клас CarRental змінюється при додаванні нових типів оренди (if/switch).
    Рішення: Використано патерн "Стратегія".
    1. Створено інтерфейс IRentPriceCalculator.
    2. Кожен тип оренди має свій калькулятор.
    3. CarRental делегує розрахунок відповідному калькулятору.
*/

public class Car
{
    public string Maker { get; set; }
    public Color Color { get; set; }
}

public interface IRentPriceCalculator
{
    decimal Calculate(decimal baseValue, int amount);
}

public class DailyRentCalculator : IRentPriceCalculator
{
    public decimal Calculate(decimal baseValue, int amount)
    {
        // Daily Logic
        return baseValue * amount;
    }
}

public class WeeklyRentCalculator : IRentPriceCalculator
{
    public decimal Calculate(decimal baseValue, int amount)
    {
        // Weekly Logic
        return baseValue * (7 * amount);
    }
}

public class CarRental(IRentPriceCalculator calculator)
{
    private readonly IRentPriceCalculator _calculator = calculator;

    public decimal Rent(Car car, decimal baseValue, int amount)
    {
        return _calculator.Calculate(baseValue, amount);
    }
}

/*
    Завдання 9. Комунікація служб аеропорту (Mediator Pattern)
    Рішення: Використано патерн "Посередник". Літаки та служби аеропорту не спілкуються один з одним напряму.
    Всі повідомлення проходять через ControlTower, яка знає кому передати повідомлення або яку дію викликати залежно від ситуації.
*/

using System;

public interface IAirportMediator
{
    void Notify(object sender, string message);
}

public class ControlTower : IAirportMediator
{
    public Airplane Plane { get; set; }
    public FuelService FuelManager { get; set; }

    public void Notify(object sender, string message)
    {
        if (sender is Airplane && message == "RequestLanding")
        {
            Console.WriteLine("Диспетчер: Смуга вільна. Дозвіл на посадку надано");
            Plane.Land();
            FuelManager.PrepareFuel();
        }

        if (sender is FuelService && message == "FuelReady")
        {
            Console.WriteLine("Диспетчер: Заправка готова");
        }
    }
}

public class Airplane
{
    private IAirportMediator _mediator;

    public Airplane(IAirportMediator mediator)
    {
        _mediator = mediator;
    }

    public void RequestLanding()
    {
        Console.WriteLine("Літак запитує дозвіл на посадку");
        _mediator.Notify(this, "RequestLanding");
    }

    public void Land()
    {
        Console.WriteLine("Літак виконує посадку");
    }
}

public class FuelService
{
    private IAirportMediator _mediator;

    public FuelService(IAirportMediator mediator)
    {
        _mediator = mediator;
    }

    public void PrepareFuel()
    {
        Console.WriteLine("Служба палива готує заправку для літака");
        _mediator.Notify(this, "FuelReady");
    }
}

class Program
{
    static void Main()
    {
        ControlTower tower = new ControlTower();
        
        Airplane plane = new Airplane(tower);
        FuelService fuelService = new FuelService(tower);

        tower.Plane = plane;
        tower.FuelManager = fuelService;

        plane.RequestLanding();
    }
}

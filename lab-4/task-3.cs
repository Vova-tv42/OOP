/*
    Завдання 3. Стани робота-пилососа (State Pattern)
    Рішення: Застосовано патерн Стан. Поведінка робота рознесена у різні класи станів.Робот просто викликає метод 
    у свого поточного стану, а стани самі вирішують, яким має бути результат і коли стан змінюється.
*/

using System;

public interface IRobotState
{
    void Handle(RobotVacuum robot);
}

public class FullBatteryState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Робот-пилосос прибирає кімнату. Батарея повна");
    }
}

public class LowBatteryState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Батарея низька. Робот шукає зарядну базу");
    }
}

public class StuckState : IRobotState
{
    public void Handle(RobotVacuum robot)
    {
        Console.WriteLine("Робот застряг та пищить");
    }
}

public class RobotVacuum
{
    private IRobotState _state;

    public RobotVacuum(IRobotState initialState)
    {
        _state = initialState;
    }

    public void SetState(IRobotState newState)
    {
        _state = newState;
        Console.WriteLine($"[Система]: Стан змінено");
    }

    public void DoWork()
    {
        _state.Handle(this);
    }
}

class Program
{
    static void Main()
    {
        RobotVacuum robot = new RobotVacuum(new FullBatteryState());
        
        robot.DoWork();

        robot.SetState(new LowBatteryState());
        robot.DoWork();

        robot.SetState(new StuckState());
        robot.DoWork();
    }
}

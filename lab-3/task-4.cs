/*
    Завдання 4. Компоненти ПЗ (Composite Pattern)
    Рішення: Використано Composite.
    Спільний інтерфейс реалізується і окремими базовими модулями, і групами модулів.
*/

using System;
using System.Collections.Generic;

public interface ISoftwareComponent
{
    double GetPrice();
    void ShowStructure(int nestingLevel);
}

public class SoftwareModule : ISoftwareComponent
{
    private string _name;
    private double _price;

    public SoftwareModule(string name, double price)
    {
        _name = name;
        _price = price;
    }

    public double GetPrice()
    {
        return _price;
    }

    public void ShowStructure(int nestingLevel)
    {
        string nestingLevelStr = new string('-', nestingLevel * 2);
        Console.WriteLine($"{nestingLevelStr} Модуль: {_name} ({_price} грн)");
    }
}

public class ModuleGroup : ISoftwareComponent
{
    private string _name;
    private List<ISoftwareComponent> _components = new List<ISoftwareComponent>();

    public ModuleGroup(string name)
    {
        _name = name;
    }

    public void AddComponent(ISoftwareComponent component)
    {
        _components.Add(component);
    }

    public double GetPrice()
    {
        double totalPrice = 0;
        foreach (var child in _components) totalPrice += child.GetPrice();
        return totalPrice;
    }

    public void ShowStructure(int nestingLevel)
    {
        string nestingLevelStr = new string('-', nestingLevel * 2);
        Console.WriteLine($"{nestingLevelStr} + Група: {_name}");
        
        foreach (var child in _components)
            child.ShowStructure(nestingLevel + 1);
    }
}

class Program
{
    static void Main()
    {
        SoftwareModule core = new SoftwareModule("Ядро", 5000);
        SoftwareModule export = new SoftwareModule("Експорт", 1500);
        
        ModuleGroup analytics = new ModuleGroup("Аналітика");
        analytics.AddComponent(export);

        ModuleGroup crm = new ModuleGroup("CRM");
        crm.AddComponent(core);
        crm.AddComponent(analytics);

        crm.ShowStructure(0);
        Console.WriteLine($"Сумарна вартість: {crm.GetPrice()} грн");
    }
}

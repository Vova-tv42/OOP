/*
    Застосовано патерн Prototype (Прототип).
    Задача: у грі тисячі орків, і кожного не можна створювати "з нуля" через важкі
    операції в конструкторі. Рішення: є один "еталонний" орк (прототип), якого
    клонують для кожного нового екземпляра. Після клонування змінюються лише
    унікальні поля (позиція, зброя, рівень), а спільні дані (текстури, ефекти,
    тип пересування) залишаються без повторної ініціалізації.
*/

using System;

public class Orc
{
    // Спільні параметри — однакові для всіх орків
    public string Texture { get; set; }
    public string MovementType { get; set; }
    public string VisualEffect { get; set; }

    // Унікальні параметри — різні для кожного орка
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Weapon { get; set; }
    public int Level { get; set; }
    public int Strength { get; set; }

    public Orc Clone()
    {
        return new Orc
        {
            Texture = this.Texture,
            MovementType = this.MovementType,
            VisualEffect = this.VisualEffect,

            PositionX = this.PositionX,
            PositionY = this.PositionY,
            Weapon = this.Weapon,
            Level = this.Level,
            Strength = this.Strength
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine("Текстура: " + Texture + " | Рух: " + MovementType + " | Ефект: " + VisualEffect);
        Console.WriteLine("Позиція: (" + PositionX + ", " + PositionY + ") | Зброя: " + Weapon + " | Рівень: " + Level + " | Сила: " + Strength);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Створення еталонного орка ---");
        var prototypeOrc = new Orc
        {
            Texture = "orc.png",
            MovementType = "Біг",
            VisualEffect = "Rage",
            
            PositionX = 0,
            PositionY = 0,
            Weapon = "Сокира",
            Level = 1,
            Strength = 10
        };
        Console.WriteLine("Еталонний орк:");
        prototypeOrc.PrintInfo();

        Console.WriteLine();
        Console.WriteLine("--- Клонування орків ---");

        var orc1 = prototypeOrc.Clone();
        orc1.PositionX = 15;
        orc1.PositionY = 43;
        orc1.Weapon = "Булава";
        orc1.Level = 3;
        orc1.Strength = 14;

        var orc2 = prototypeOrc.Clone();
        orc2.PositionX = 87;
        orc2.PositionY = 10;
        orc2.Level = 5;
        orc2.Strength = 20;

        Console.WriteLine("Еталонний орк:");
        prototypeOrc.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("Орк 1:");
        orc1.PrintInfo();
        Console.WriteLine();

        Console.WriteLine("Орк 2:");
        orc2.PrintInfo();
        Console.WriteLine();
    }
}

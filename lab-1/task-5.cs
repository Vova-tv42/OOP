/*
    Порушено принцип Liskov Substitution.
    Клас Square успадковує Rectangle, але змінює логіку встановлення ширини/висоти,
    що порушує очікування від базового класу Rectangle (зміна ширини не має міняти висоту).
    Рішення:
    1. Розриваємо спадкування Square : Rectangle.
    2. Вводимо спільний абстрактний клас Shape (або інтерфейс) з властивістю Area.
    3. Rectangle та Square реалізують Shape незалежно.
*/

public abstract class Shape
{
    public abstract double Area { get; }
}

public class Rectangle : Shape
{
    public double Height { get; set; }
    public double Width { get; set; }

    public override double Area => Height * Width;
}

public class Square : Shape
{
    public double Side { get; set; }

    public override double Area => Side * Side;
}

public class Execution
{
    public Execution()
    {
        var r = new Rectangle()
        {
            Height = 10,
            Width = 5
        };
        GetShapeArea(r);

        var s = new Square()
        {
            Side = 5 
        };
        GetShapeArea(s);
    }

    public double GetShapeArea(Shape shape)
    {
        Debug.WriteLine($"{shape.Area}");
        return shape.Area;
    }
}

/*
    Завдання 7. Гра Minecraft (Flyweight Pattern)
    Рішення: Всі спільні дані блоку (текстура, модель) винесені у клас BlockType, який кешується на фабриці.
    Клас конкретного блоку (GameBlock) зберігає лише координати і стан. 
    Це економить пам'ять для мільйонів об'єктів.
*/

using System;
using System.Collections.Generic;

public class BlockType
{
    private string _name;
    private string _texture;
    private string _model;
    private int _durability;

    public BlockType(string name, string texture, string model, int durability)
    {
        _name = name;
        _texture = texture;
        _model = model;
        _durability = durability;
    }

    public void Interact()
    {
        Console.WriteLine($"[Взаємодія] Ви торкнулися об'єкта '{_name}'. Спільна міцність типу: {_durability}");
    }

    public void Render(int x, int y, int z, bool isPlaced)
    {
        string status = isPlaced ? "Розміщено" : "Зламано";
        Console.WriteLine($"[{status}] {_name} на координатах ({x}, {y}, {z}). Текстура: {_texture}");
    }
}

public class BlockFactory
{
    private static Dictionary<string, BlockType> _types = new Dictionary<string, BlockType>();

    public static BlockType GetBlockType(string name, string texture, string model, int durability)
    {
        if (!_types.ContainsKey(name))
        {
            Console.WriteLine($"[Фабрика] Створення нового важкого об'єкта: {name}");
            _types[name] = new BlockType(name, texture, model, durability);
        }
        else
        {
            Console.WriteLine($"[Фабрика] Перевикористання існуючого: {name}");
        }

        return _types[name];
    }
}

public class GameBlock
{
    private int _x, _y, _z;
    private bool _isPlaced;
    private BlockType _type;

    public GameBlock(int x, int y, int z, BlockType type)
    {
        _x = x;
        _y = y;
        _z = z;
        _type = type;
        _isPlaced = true;
    }

    public void BreakBlock()
    {
        _isPlaced = false;
        Console.WriteLine($"Блок на ({_x}, {_y}, {_z}) був зламаний");
    }

    public void Use()
    {
        _type.Interact();
    }

    public void Draw()
    {
        _type.Render(_x, _y, _z, _isPlaced);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Генерація світу...");
        List<GameBlock> world = new List<GameBlock>();

        BlockType oak = BlockFactory.GetBlockType("Дерево", "oak.png", "cube.obj", 100);
        
        world.Add(new GameBlock(10, 0, 10, oak));
        world.Add(new GameBlock(10, 1, 10, oak));

        BlockType stone = BlockFactory.GetBlockType("Камінь", "stone.png", "cube.obj", 500);
        world.Add(new GameBlock(0, 0, 0, stone));

        Console.WriteLine("\nВідображення:");
        foreach (var block in world) block.Draw();

        Console.WriteLine("\nВзаємодія з об'єктами:");
        world[0].Use();
        world[2].Use();

        Console.WriteLine("\nГравець ламає дерево");
        world[0].BreakBlock();
        world[0].Draw(); 
    }
}

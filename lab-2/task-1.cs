/*
    Застосовано патерн Builder.
    Задача: створення персонажа з багатьма параметрами (ім'я, стать, клас, характеристики,
    зовнішність, зброя). Builder дозволяє будувати об'єкт покроково, забезпечуючи
    як повне налаштування кожної деталі, так і швидке створення "стандартного бійця"
    через готовий метод CharacterBuilder.CreateDefaultWarrior().
*/

using System;

public class Character
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Class { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Agility { get; set; }
    public string EyeColor { get; set; }
    public string Hairstyle { get; set; }
    public string Weapon { get; set; }

    public void PrintInfo()
    {
        Console.WriteLine("=== Персонаж ===");
        Console.WriteLine("Ім'я: " + Name);
        Console.WriteLine("Стать: " + Gender);
        Console.WriteLine("Клас: " + Class);
        Console.WriteLine("Сила: " + Strength + " | Інтелект: " + Intelligence + " | Спритність: " + Agility);
        Console.WriteLine("Колір очей: " + EyeColor);
        Console.WriteLine("Зачіска: " + Hairstyle);
        Console.WriteLine("Зброя: " + Weapon);
    }
}

public class CharacterBuilder
{
    private Character _character;

    public CharacterBuilder()
    {
        _character = new Character();
    }

    public CharacterBuilder SetName(string name)
    {
        _character.Name = name;
        return this;
    }

    public CharacterBuilder SetGender(string gender)
    {
        _character.Gender = gender;
        return this;
    }

    public CharacterBuilder SetClass(string characterClass)
    {
        _character.Class = characterClass;
        return this;    
    }

    public CharacterBuilder SetStats(int strength, int intelligence, int agility)
    {
        _character.Strength = strength;
        _character.Intelligence = intelligence;
        _character.Agility = agility;
        return this;
    }

    public CharacterBuilder SetEyeColor(string eyeColor)
    {
        _character.EyeColor = eyeColor;
        return this;
    }

    public CharacterBuilder SetHairstyle(string hairstyle)
    {
        _character.Hairstyle = hairstyle;
        return this;
    }

    public CharacterBuilder SetWeapon(string weapon)
    {
        _character.Weapon = weapon;
        return this;
    }

    public Character Build()
    {
        return _character;
    }

    public static Character CreateDefaultWarrior()
    {
        return new CharacterBuilder()
            .SetName("Герой1")
            .SetGender("Чоловік")
            .SetClass("Воїн")
            .SetStats(strength: 10, intelligence: 3, agility: 5)
            .SetEyeColor("Карі")
            .SetHairstyle("Коротке")
            .SetWeapon("Меч")
            .Build();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Стандартний боєць ---");
        var defaultWarrior = CharacterBuilder.CreateDefaultWarrior();
        defaultWarrior.PrintInfo();

        Console.WriteLine();

        Console.WriteLine("--- Персональний персонаж ---");
        var customCharacter = new CharacterBuilder()
            .SetName("Олена")
            .SetGender("Жінка")
            .SetClass("Селянин зі списом")
            .SetStats(strength: 5, intelligence: 10, agility: 12)
            .SetEyeColor("Сині")
            .SetHairstyle("Довге")
            .SetWeapon("Спис")
            .Build();

        customCharacter.PrintInfo();
    }
}

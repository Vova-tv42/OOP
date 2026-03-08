/*
    Застосовано патерн Abstract Factory.
    Задача: мобільний додаток з темною та світлою темою. Abstract Factory гарантує,
    що всі UI елементи (кнопка, текстове поле, список) будуть створені з однієї
    теми, унеможливлюючи появу "змішаного" дизайну. Для зміни теми достатньо
    замінити одну фабрику - весь UI автоматично стане цілісним.
*/

using System;

public interface IButton
{
    void Render();
}

public interface ITextField
{
    void Render();
}

public interface IDropdown
{
    void Render();
}

public interface IThemeFactory
{
    IButton CreateButton();
    ITextField CreateTextField();
    IDropdown CreateDropdown();
}

public class DarkButton : IButton
{
    public void Render()
    {
        Console.WriteLine("[Темна тема] Кнопка: фон #242323, текст #ffffff");
    }
}

public class DarkTextField : ITextField
{
    public void Render()
    {
        Console.WriteLine("[Темна тема] Текстове поле: фон #312e2e, текст #eeeeee");
    }
}

public class DarkDropdown : IDropdown
{
    public void Render()
    {
        Console.WriteLine("[Темна тема] Випадаючий список: фон #333333, текст #ffffff");
    }
}

public class LightButton : IButton
{
    public void Render()
    {
        Console.WriteLine("[Світла тема] Кнопка: фон #f7f5f0, текст #000000");
    }
}

public class LightTextField : ITextField
{
    public void Render()
    {
        Console.WriteLine("[Світла тема] Текстове поле: фон #ffffff, текст #333333");
    }
}

public class LightDropdown : IDropdown
{
    public void Render()
    {
        Console.WriteLine("[Світла тема] Випадаючий список: фон #eeeeee, текст #000000");
    }
}

public class DarkThemeFactory : IThemeFactory
{
    public IButton CreateButton()
    {
        return new DarkButton();
    }

    public ITextField CreateTextField()
    {
        return new DarkTextField();
    }

    public IDropdown CreateDropdown()
    {
        return new DarkDropdown();
    }
}

public class LightThemeFactory : IThemeFactory
{
    public IButton CreateButton()
    {
        return new LightButton();
    }

    public ITextField CreateTextField()
    {
        return new LightTextField();
    }

    public IDropdown CreateDropdown()
    {
        return new LightDropdown();
    }
}

public class MobileApp
{
    private readonly IButton _button;
    private readonly ITextField _textField;
    private readonly IDropdown _dropdown;

    public MobileApp(IThemeFactory factory)
    {
        _button = factory.CreateButton();
        _textField = factory.CreateTextField();
        _dropdown = factory.CreateDropdown();
    }

    public void RenderScreen()
    {
        Console.WriteLine("Відображення екрану:");
        _button.Render();
        _textField.Render();
        _dropdown.Render();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Темна тема ---");
        var darkApp = new MobileApp(new DarkThemeFactory());
        darkApp.RenderScreen();

        Console.WriteLine();

        Console.WriteLine("--- Світла тема ---");
        var lightApp = new MobileApp(new LightThemeFactory());
        lightApp.RenderScreen();
    }
}

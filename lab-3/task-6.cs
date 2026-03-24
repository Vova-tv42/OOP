/*
    Завдання 6. Відображення даних аналітики (Bridge Pattern)
    Рішення: Відокремлює логіку даних від відображення.
    Замість створення класів типу WebFinanceDashboard, ми створюємо інтерфейс IDisplayMethod 
    і базовий клас DataReport. Додавання нових варіантів тепер незалежне.
*/

using System;

public interface IDisplayMethod
{
    void RenderHeader(string title);
    void RenderData(string data);
}

public class WebDisplay : IDisplayMethod
{
    public void RenderHeader(string title) => Console.WriteLine($"[Web UI] <h1>{title}</h1>");
    public void RenderData(string data) => Console.WriteLine($"[Web UI] <div>{data}</div>");
}

public class PdfDisplay : IDisplayMethod
{
    public void RenderHeader(string title) => Console.WriteLine($"[PDF] Формування: {title}");
    public void RenderData(string data) => Console.WriteLine($"[PDF] Текст: {data}");
}

public abstract class DataReport
{
    protected IDisplayMethod _display;

    public DataReport(IDisplayMethod display)
    {
        _display = display;
    }

    public void SetDisplayMethod(IDisplayMethod display)
    {
        _display = display;
    }

    public abstract void OutputReport();
}

public class FinancialReport : DataReport
{
    public FinancialReport(IDisplayMethod display) : base(display) { }

    public override void OutputReport()
    {
        _display.RenderHeader("Фінансовий звіт");
        _display.RenderData("Баланс: $2000");
    }
}

public class UserReport : DataReport
{
    public UserReport(IDisplayMethod display) : base(display) { }

    public override void OutputReport()
    {
        _display.RenderHeader("Метрики");
        _display.RenderData("Активні: 4500");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Web Dashboard ===");
        DataReport report = new FinancialReport(new WebDisplay());
        report.OutputReport();
        
        Console.WriteLine();

        Console.WriteLine("=== PDF Export ===");
        report.SetDisplayMethod(new PdfDisplay());
        report.OutputReport();
    }
}

/*
    Застосовано патерн Factory Method.
    Задача: програма має генерувати звіти у різних форматах (PDF, Excel, HTML, JSON).
    Factory Method дозволяє основному коду не залежати від конкретних класів звітів.
    Код просто викликає ReportExporter.Create(format) і отримує потрібний об'єкт,
    не знаючи про деталі його реалізації. Для додавання нового формату достатньо
    створити новий клас.
*/

using System;

public interface IReportExporter
{
    void Export(string content);
}

public class PdfReportExporter : IReportExporter
{
    public void Export(string content)
    {
        Console.WriteLine("[PDF] Експорт звіту в PDF: " + content);
    }
}

public class ExcelReportExporter : IReportExporter
{
    public void Export(string content)
    {
        Console.WriteLine("[Excel] Експорт звіту в Excel: " + content);
    }
}

public class HtmlReportExporter : IReportExporter
{
    public void Export(string content)
    {
        Console.WriteLine("[HTML] Експорт звіту в HTML: " + content);
    }
}

public class JsonReportExporter : IReportExporter
{
    public void Export(string content)
    {
        Console.WriteLine("[JSON] Експорт звіту в JSON: " + content);
    }
}

public class ReportExporter
{
    public static IReportExporter Create(string format)
    {
        if (format == "pdf") return new PdfReportExporter();
        if (format == "excel") return new ExcelReportExporter();
        if (format == "html") return new HtmlReportExporter();
        if (format == "json") return new JsonReportExporter();

        throw new Exception("Невідомий формат звіту: " + format);
    }
}

class Program
{
    static void Main()
    {
        string reportContent = "Квартальний звіт Q1 2025";

        string[] formats = { "pdf", "excel", "html", "json" };

        foreach (var format in formats)
        {
            var exporter = ReportExporter.Create(format);
            exporter.Export(reportContent);
        }
    }
}

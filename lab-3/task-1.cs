/*
    Завдання 1. Інтеграція платіжних провайдерів (Adapter Pattern)
    Рішення: Для інтеграції різних платіжних провайдерів використано патерн "Адаптер". 
    Ми створили єдиний уніфікований інтерфейс IPaymentProcessor, який очікує наша внутрішня система. 
    Для кожного нестандартного провайдера створено окремий клас-адаптер, 
    який реалізує цей інтерфейс, приховуючи всередині себе специфіку роботи з конкретним API.
*/

using System;

public interface IPaymentProcessor
{
    double GetAmount(string transactionId);
    string GetCurrency(string transactionId);
    string GetStatus(string transactionId);
}

public class JsonPaymentProvider
{
    public string GetPaymentDataJson(string transactionId)
    {
        Console.WriteLine($"[JsonProvider] Запит даних JSON для транзакції {transactionId}");
        return "{\"amount_value\": 100.50, \"currency_code\": \"USD\", \"status\": \"Success\"}";
    }
}

public class JsonPaymentAdapter : IPaymentProcessor
{
    private readonly JsonPaymentProvider _provider;

    public JsonPaymentAdapter(JsonPaymentProvider provider)
    {
        _provider = provider;
    }

    public double GetAmount(string transactionId)
    {
        _provider.GetPaymentDataJson(transactionId);
        Console.WriteLine("Парсинг суми з JSON");
        return 100.50; 
    }

    public string GetCurrency(string transactionId)
    {
        _provider.GetPaymentDataJson(transactionId);
        Console.WriteLine("Парсинг валюти з JSON");
        return "USD";
    }

    public string GetStatus(string transactionId)
    {
        _provider.GetPaymentDataJson(transactionId);
        Console.WriteLine("Парсинг статусу з JSON");
        return "Success";
    }
}

public class XmlPaymentProvider
{
    public string FetchPaymentXml(string txId)
    {
        Console.WriteLine($"[XmlProvider] Запит даних XML для транзакції {txId}");
        return "<payment><amount>250.00</amount><currency>EUR</currency><state>Pending</state></payment>";
    }
}

public class XmlPaymentAdapter : IPaymentProcessor
{
    private readonly XmlPaymentProvider _provider;

    public XmlPaymentAdapter(XmlPaymentProvider provider)
    {
        _provider = provider;
    }

    public double GetAmount(string transactionId)
    {
        _provider.FetchPaymentXml(transactionId);
        Console.WriteLine("Парсинг суми з XML");
        return 250.00;
    }

    public string GetCurrency(string transactionId)
    {
        _provider.FetchPaymentXml(transactionId);
        Console.WriteLine("Парсинг валюти з XML");
        return "EUR";
    }

    public string GetStatus(string transactionId)
    {
        _provider.FetchPaymentXml(transactionId);
        Console.WriteLine("Парсинг статусу з XML");
        return "Pending";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== JSON Adapter ===");
        IPaymentProcessor jsonAdapter = new JsonPaymentAdapter(new JsonPaymentProvider());
        Console.WriteLine(
            $"Отримано: {jsonAdapter.GetAmount("TX1")} {jsonAdapter.GetCurrency("TX1")}, Статус: {jsonAdapter.GetStatus("TX1")}\n"
        );

        Console.WriteLine("=== XML Adapter ===");
        IPaymentProcessor xmlAdapter = new XmlPaymentAdapter(new XmlPaymentProvider());
        Console.WriteLine(
            $"Отримано: {xmlAdapter.GetAmount("TX2")} {xmlAdapter.GetCurrency("TX2")}, Статус: {xmlAdapter.GetStatus("TX2")}"
        );
    }
}

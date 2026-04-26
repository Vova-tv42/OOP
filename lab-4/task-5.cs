/*
    Завдання 5. Ланцюжок обробки запитів у підтримку (Chain of Responsibility)
    Рішення: Застосовано патерн "Ланцюжок обов'язків". Кожен обробник дивиться на складність запиту. 
    Якщо він може його обробити - обробляє. Якщо ні - передає наступному обробнику у ланцюжку.
*/

using System;

public class SupportRequest
{
    public string Message { get; set; }
    public int Difficulty { get; set; }
    
    public SupportRequest(string message, int difficulty)
    {
        Message = message;
        Difficulty = difficulty;
    }
}

public abstract class SupportHandler
{
    protected SupportHandler _nextHandler;

    public void SetNext(SupportHandler handler)
    {
        _nextHandler = handler;
    }

    public abstract void HandleRequest(SupportRequest request);
}

public class ChatBotHandler : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        if (request.Difficulty == 1)
        {
            Console.WriteLine($"Чат-бот обробив запит: '{request.Message}'");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Чат-бот не знає відповіді, передає оператору");
            _nextHandler.HandleRequest(request);
        }
    }
}

public class OperatorHandler : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        if (request.Difficulty == 2)
        {
            Console.WriteLine($"Оператор 1-го рівня допоміг із запитом: '{request.Message}'");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine("Оператор не впорався, передає технічному спеціалісту");
            _nextHandler.HandleRequest(request);
        }
    }
}

public class TechSpecialistHandler : SupportHandler
{
    public override void HandleRequest(SupportRequest request)
    {
        Console.WriteLine($"Технічний спеціаліст успішно вирішив складну проблему: '{request.Message}'");
    }
}

class Program
{
    static void Main()
    {
        SupportHandler bot = new ChatBotHandler();
        SupportHandler operator1 = new OperatorHandler();
        SupportHandler techExpert = new TechSpecialistHandler();

        bot.SetNext(operator1);
        operator1.SetNext(techExpert);

        Console.WriteLine("--- Новий запит ---");
        bot.HandleRequest(new SupportRequest("Як скинути пароль?", 1));

        Console.WriteLine("\n--- Новий запит ---");
        bot.HandleRequest(new SupportRequest("Не працює оплата карткою", 2));

        Console.WriteLine("\n--- Новий запит ---");
        bot.HandleRequest(new SupportRequest("Сервер недоступний, проблема з базою даних", 3));
    }
}

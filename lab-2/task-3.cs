/*
    Застосовано патерн Singleton.
    Задача: єдиний логер для всієї програми. Singleton гарантує, що незалежно від
    того, скільки модулів звертаються до Logger, завжди буде лише один екземпляр
    з одним підключенням до файлу/сервера логів. Це запобігає перевантаженню
    системи зайвими з'єднаннями.
*/

using System;
using System.Collections.Generic;

public class Logger
{
    private static Logger _instance;

    private readonly List<string> _logs;

    private Logger()
    {
        _logs = new List<string>();
        Console.WriteLine("[Logger] Підключення до сервера логів встановлено.");
    }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }

        return _instance;
    }

    public void LogError(string moduleName, string message)
    {
        string logEntry = "[ERROR] [" + moduleName + "] " + message;
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogInfo(string moduleName, string message)
    {
        string logEntry = "[INFO] [" + moduleName + "] " + message;
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public void PrintAllLogs()
    {
        Console.WriteLine("=== Усі записи логів ===");
        foreach (var log in _logs)
        {
            Console.WriteLine(log);
        }
    }
}

public class PaymentModule
{
    public void ProcessPayment()
    {
        var logger = Logger.GetInstance();
        logger.LogInfo("PaymentModule", "Початок обробки платежу.");
        logger.LogError("PaymentModule", "Невірні дані картки.");
    }
}

public class AuthModule
{
    public void Login(string username)
    {
        var logger = Logger.GetInstance();
        logger.LogInfo("AuthModule", "Спроба входу користувача: " + username);
        logger.LogError("AuthModule", "Невірний пароль для: " + username);
    }
}

class Program
{
    static void Main()
    {
        var paymentModule = new PaymentModule();
        var authModule = new AuthModule();

        paymentModule.ProcessPayment();
        authModule.Login("john.doe");

        Console.WriteLine();

        bool isSameInstance = object.ReferenceEquals(Logger.GetInstance(), Logger.GetInstance());
        Console.WriteLine("Один і той самий екземпляр Logger: " + isSameInstance);

        Console.WriteLine();
        Logger.GetInstance().PrintAllLogs();
    }
}

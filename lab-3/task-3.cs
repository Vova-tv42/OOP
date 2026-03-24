/*
    Завдання 3. Деталі фільму (Proxy Pattern)
    Рішення: Замісник (Proxy) завантажує важкі дані лише тоді, коли користувач вперше 
    намагається їх переглянути. Також він контролює доступ.
*/

using System;

public interface IVideo
{
    string GetTitle();
    void ShowDetails(bool isPremiumUser);
}

public class RealVideo : IVideo
{
    private string _title;
    private string _heavyDetails;

    public RealVideo(string title)
    {
        _title = title;
        LoadHeavyData();
    }

    private void LoadHeavyData()
    {
        Console.WriteLine($"[RealVideo] Завантаження даних для '{_title}' з БД");
        _heavyDetails = "Великий опис";
    }

    public string GetTitle()
    {
        return _title;
    }

    public void ShowDetails(bool isPremiumUser)
    {
        Console.WriteLine($"[RealVideo] Деталі '{_title}': {_heavyDetails}");
    }
}

public class VideoProxy : IVideo
{
    private RealVideo _realVideo;
    private string _title;

    public VideoProxy(string title)
    {
        _title = title;
    }

    public string GetTitle()
    {
        return _title;
    }

    public void ShowDetails(bool isPremiumUser)
    {
        if (!isPremiumUser)
        {
            Console.WriteLine($"[Proxy] Відмовлено! '{_title}' лише для Premium");
            return;
        }

        if (_realVideo == null)
        {
            Console.WriteLine($"[Proxy] Дані ще не завантажені. Створюємо об'єкт");
            _realVideo = new RealVideo(_title);
        }
        else
        {
            Console.WriteLine($"[Proxy] Використовуємо кеш для '{_title}'");
        }

        _realVideo.ShowDetails(isPremiumUser);
    }
}

class Program
{
    static void Main()
    {
        IVideo movie1 = new VideoProxy("Термінатор");
        
        Console.WriteLine("Спроба звичайного користувача");
        movie1.ShowDetails(false);
        
        Console.WriteLine("\nСпроба premium-користувача");
        movie1.ShowDetails(true);

        Console.WriteLine("\nСпроба premium-користувача ще раз");
        movie1.ShowDetails(true);
    }
}

/*
    Завдання 1. Сповіщення підписників YouTube (Observer Pattern)
    Рішення: Використано патерн "Спостерігач" (Observer).
    Канал зберігає список підписників і автоматично викликає їхній метод Update, коли виходить нове відео. 
    Це дозволяє автору не знати конкретних підписників, але ефективно їх сповіщати.
*/

using System;
using System.Collections.Generic;

public interface ISubscriber
{
    void Update(string videoTitle);
}

public class UserSubscriber : ISubscriber
{
    private string _userName;

    public UserSubscriber(string userName)
    {
        _userName = userName;
    }

    public void Update(string videoTitle)
    {
        Console.WriteLine($"[Сповіщення для {_userName}]: На каналі вийшло нове відео - '{videoTitle}'");
    }
}

public class YouTubeChannel
{
    private List<ISubscriber> _subscribers = new List<ISubscriber>();
    private string _channelName;

    public YouTubeChannel(string channelName)
    {
        _channelName = channelName;
    }

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.Add(subscriber);
        Console.WriteLine("Новий підписник доданий.");
    }

    public void Unsubscribe(ISubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
        Console.WriteLine("Підписник видалений.");
    }

    public void UploadVideo(string videoTitle)
    {
        Console.WriteLine($"\nКанал '{_channelName}' завантажив нове відео: '{videoTitle}'");
        NotifySubscribers(videoTitle);
    }

    private void NotifySubscribers(string videoTitle)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Update(videoTitle);
        }
    }
}

class Program
{
    static void Main()
    {
        YouTubeChannel channel = new YouTubeChannel("Nikita UA");

        UserSubscriber user1 = new UserSubscriber("Фанат дюни");
        UserSubscriber user2 = new UserSubscriber("Фанат матриці");
        UserSubscriber user3 = new UserSubscriber("Блок Петра Порошенка");

        channel.Subscribe(user1);
        channel.Subscribe(user2);
        channel.UploadVideo("Відео 1");
        
        channel.Unsubscribe(user2);
        channel.Subscribe(user3);
        channel.UploadVideo("Відео 2");
    }
}

/*
    Завдання 6. Загальний алгоритм проходження курсу (Template Method)
    Рішення: Застосовано патерн "Шаблонний метод". В абстрактному класі Course визначено загальний
    алгоритм методами. Деякі кроки мають базову реалізацію, а необхідні специфічні кроки
    визначені як абстрактні і реалізуються в похідних класах (ProgrammingCourse, DesignCourse).
*/

using System;

public abstract class Course
{
    public void CompleteCourse()
    {
        WatchLectures();
        DoPractice();
        DoSpecificTask();
        PassTest();
        ProtectProject();
    }

    private void WatchLectures()
    {
        Console.WriteLine("Студент переглядає відеолекції");
    }

    private void DoPractice()
    {
        Console.WriteLine("Студент виконує практичні завдання");
    }

    private void PassTest()
    {
        Console.WriteLine("Студент проходить фінальне тестування");
    }

    private void ProtectProject()
    {
        Console.WriteLine("Студент захищає фінальний проєкт");
    }

    protected abstract void DoSpecificTask();
}

public class ProgrammingCourse : Course
{
    protected override void DoSpecificTask()
    {
        Console.WriteLine("Специфіка: Студент проходить автоматичну перевірку коду на сервері");
    }
}

public class DesignCourse : Course
{
    protected override void DoSpecificTask()
    {
        Console.WriteLine("Специфіка: Студент презентує свій макет перед викладачем онлайн");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("--- Курс програмування ---");
        Course progCourse = new ProgrammingCourse();
        progCourse.CompleteCourse();

        Console.WriteLine("\n--- Курс дизайну ---");
        Course designCourse = new DesignCourse();
        designCourse.CompleteCourse();
    }
}

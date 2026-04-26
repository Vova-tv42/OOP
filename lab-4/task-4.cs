/*
    Завдання 4. Текстовий редактор та відміна дій Ctrl+Z (Memento Pattern)
    Рішення: Використано патерн "Знімок". Редактор може створювати знімок свого стану та відновлюватися з нього. 
    Об'єкт знімка містить стан тексту, а HistoryTracker зберігає його та дозволяє робити крок назад.
*/

using System;
using System.Collections.Generic;

public class TextMemento
{
    public string Text { get; private set; }
    public string Color { get; private set; }

    public TextMemento(string text, string color)
    {
        Text = text;
        Color = color;
    }
}

public class TextEditor
{
    public string Text { get; set; }
    public string Color { get; set; }

    public TextEditor()
    {
        Text = "";
        Color = "Чорний";
    }

    public void TypeText(string words)
    {
        Text += words;
        Console.WriteLine($"Текст змінено: {Text} (Колір: {Color})");
    }

    public void ChangeColor(string newColor)
    {
        Color = newColor;
        Console.WriteLine($"Колір змінено на: {Color}");
    }

    public TextMemento Save()
    {
        return new TextMemento(Text, Color);
    }

    public void Restore(TextMemento memento)
    {
        if (memento != null)
        {
            Text = memento.Text;
            Color = memento.Color;
            Console.WriteLine($"Стан відновлено: {Text} (Колір: {Color})");
        }
    }
}

public class HistoryTracker
{
    private Stack<TextMemento> _history = new Stack<TextMemento>();

    public void Backup(TextEditor editor)
    {
        _history.Push(editor.Save());
    }

    public void Undo(TextEditor editor)
    {
        if (_history.Count > 0)
        {
            var memento = _history.Pop();
            editor.Restore(memento);
        }
        else
        {
            Console.WriteLine("Вже найстаріша версія");
        }
    }
}

class Program
{
    static void Main()
    {
        TextEditor editor = new TextEditor();
        HistoryTracker history = new HistoryTracker();

        history.Backup(editor);
        editor.TypeText("Дайте, будь ласка, ");

        history.Backup(editor);
        editor.ChangeColor("Червоний");
        editor.TypeText("7 балів");

        Console.WriteLine("\n[Ctrl + Z]");
        history.Undo(editor);

        Console.WriteLine("\n[Ctrl + Z]");
        history.Undo(editor);
    }
}

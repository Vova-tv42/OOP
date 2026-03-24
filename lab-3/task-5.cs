/*
    Завдання 5. Обробка документів SaaS (Decorator Pattern)
    Рішення: Клас DocumentDecorator дозволяє динамічно "обгортати" документ 
    додатковими функціями (стиснення, шифрування, водяний знак).
    Вони додають свою поведінку перед або після виклику методу базового об'єкта,дозволяючи комбінувати їх.
*/

using System;

public interface IDocument
{
    string ReadContent();
    void WriteContent(string text);
}

public class BasicDocument : IDocument
{
    private string _content = "";

    public string ReadContent() => _content;

    public void WriteContent(string text)
    {
        _content = text;
        Console.WriteLine($"[BasicDocument] Записано базовий текст.");
    }
}

public class DocumentDecorator : IDocument
{
    protected IDocument _wrapperObject;

    public DocumentDecorator(IDocument wrapper)
    {
        _wrapperObject = wrapper;
    }

    public virtual string ReadContent() => _wrapperObject.ReadContent();

    public virtual void WriteContent(string text) => _wrapperObject.WriteContent(text);
}

public class CompressionDecorator : DocumentDecorator
{
    public CompressionDecorator(IDocument wrapper) : base(wrapper) { }

    public override void WriteContent(string text)
    {
        Console.WriteLine("[Compression] Стиснення");
        base.WriteContent("[Стиснуто] " + text);
    }

    public override string ReadContent()
    {
        Console.WriteLine("[Compression] Розпакування");
        return base.ReadContent().Replace("[Стиснуто] ", "");
    }
}

public class EncryptionDecorator : DocumentDecorator
{
    public EncryptionDecorator(IDocument wrapper) : base(wrapper) { }

    public override void WriteContent(string text)
    {
        Console.WriteLine("[Encryption] Шифрування");
        base.WriteContent("[Зашифровано] " + text);
    }

    public override string ReadContent()
    {
        Console.WriteLine("[Encryption] Розшифрування");
        return base.ReadContent().Replace("[Зашифровано] ", "");
    }
}

public class WatermarkDecorator : DocumentDecorator
{
    public WatermarkDecorator(IDocument wrapper) : base(wrapper) { }

    public override string ReadContent()
    {
        Console.WriteLine("[Watermark] Додавання водяного знаку");
        return base.ReadContent() + " (Водяний знак)";
    }
}

class Program
{
    static void Main()
    {
        IDocument doc = new BasicDocument();
        doc = new CompressionDecorator(doc);
        doc = new EncryptionDecorator(doc);
        doc = new WatermarkDecorator(doc);

        doc.WriteContent("Секретний звіт.");
        Console.WriteLine();
        string result = doc.ReadContent();
        Console.WriteLine($"\nРезультат:\n{result}");
    }
}

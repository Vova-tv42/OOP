/*
    Завдання 7. Контрольовані дії з документами (Command Pattern)
    Рішення: Патерн "Команда". Кожна операція інкапсулюється в окремий клас, що має методи Execute та Undo.
    CommandManager збирає ці команди в історію і може швидко виконати скасування.
*/

using System;
using System.Collections.Generic;

public class DocumentSystem
{
    public void CreateDocument(string docName)
    {
        Console.WriteLine($"Документ '{docName}' був створений.");
    }

    public void DeleteDocument(string docName)
    {
        Console.WriteLine($"Документ '{docName}' був видалений.");
    }
}

public interface ICommand
{
    void Execute();
    void Undo();
}

public class CreateCommand : ICommand
{
    private DocumentSystem _docSystem;
    private string _docName;

    public CreateCommand(DocumentSystem docSystem, string docName)
    {
        _docSystem = docSystem;
        _docName = docName;
    }

    public void Execute()
    {
        _docSystem.CreateDocument(_docName);
    }

    public void Undo()
    {
        _docSystem.DeleteDocument(_docName);
    }
}

public class DeleteCommand : ICommand
{
    private DocumentSystem _docSystem;
    private string _docName;

    public DeleteCommand(DocumentSystem docSystem, string docName)
    {
        _docSystem = docSystem;
        _docName = docName;
    }

    public void Execute()
    {
        _docSystem.DeleteDocument(_docName);
    }

    public void Undo()
    {
        _docSystem.CreateDocument(_docName);
    }
}

public class CommandManager
{
    private Stack<ICommand> _history = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            Console.WriteLine("Відміна останньої дії");
            command.Undo();
        }
        else
        {
            Console.WriteLine("Немає дій для відміни");
        }
    }
}

class Program
{
    static void Main()
    {
        DocumentSystem system = new DocumentSystem();
        CommandManager manager = new CommandManager();

        manager.ExecuteCommand(new CreateCommand(system, "Річний звіт.pdf"));
        manager.ExecuteCommand(new DeleteCommand(system, "Старий план.docx"));

        Console.WriteLine();
        manager.UndoLastCommand();
    }
}

/*
    Завдання 10. Аналіз типів фінансових документів (Visitor Pattern)
    Рішення: Патерн "Відвідувач" дозволяє винести операції у нові класи без зміни самих класів документів.
    Кожен документ має метод Accept, який приймає Відвідувача, після чого Відвідувач застосовує відповідний алгоритм.
*/

using System;
using System.Collections.Generic;

public interface IDocumentVisitor
{
    void Visit(Invoice invoice);
    void Visit(Contract contract);
}

public interface IFinancialDocument
{
    void Accept(IDocumentVisitor visitor);
}

public class Invoice : IFinancialDocument
{
    public double TotalAmount { get; set; }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Contract : IFinancialDocument
{
    public string PartnerName { get; set; }

    public void Accept(IDocumentVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class TaxCheckVisitor : IDocumentVisitor
{
    public void Visit(Invoice invoice)
    {
        Console.WriteLine($"Перевірка податків для Рахунку (Сума: {invoice.TotalAmount})");
    }

    public void Visit(Contract contract)
    {
        Console.WriteLine($"Юридична перевірка контракту з партнером {contract.PartnerName}");
    }
}

class Program
{
    static void Main()
    {
        List<IFinancialDocument> documents = new List<IFinancialDocument>
        {
            new Invoice { TotalAmount = 25000.0 },
            new Contract { PartnerName = "ТОВ НікітаДев" }
        };

        IDocumentVisitor taxChecker = new TaxCheckVisitor();

        Console.WriteLine("--- Початок перевірки документів ---");
        foreach (var doc in documents)
        {
            doc.Accept(taxChecker);
        }
    }
}

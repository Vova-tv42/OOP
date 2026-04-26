/*
    Завдання 8. Обхід працівників у різних структурах (Iterator Pattern)
    Рішення: Застосовано патерн "Ітератор". Ми сховали складність різних структур під стандартизований інтерфейс IEmployeeIterator.
    Тепер клієнтський код послідовно через HasNext та GetNext обходить всіх працівників, не знаючи, як саме вони зберігаються в колекції.
*/

using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }
    public string Role { get; set; }
}

public interface IEmployeeIterator
{
    bool HasNext();
    Employee GetNext();
}

public interface IEmployeeCollection
{
    IEmployeeIterator CreateIterator();
}

public class DepartmentCollection : IEmployeeCollection
{
    private List<Employee> _employees = new List<Employee>();

    public void AddEmployee(Employee emp)
    {
        _employees.Add(emp);
    }

    public IEmployeeIterator CreateIterator()
    {
        return new DepartmentIterator(_employees);
    }
}

public class DepartmentIterator : IEmployeeIterator
{
    private List<Employee> _employeeList;
    private int _position = 0;

    public DepartmentIterator(List<Employee> employeeList)
    {
        _employeeList = employeeList;
    }

    public bool HasNext()
    {
        return _position < _employeeList.Count;
    }

    public Employee GetNext()
    {
        if (!HasNext()) return null;

        Employee employee = _employeeList[_position];
        _position++;
        return employee;
    }
}

class Program
{
    static void Main()
    {
        DepartmentCollection itDepartment = new DepartmentCollection();
        itDepartment.AddEmployee(new Employee { Name = "Нікіта", Role = "Розробник" });
        itDepartment.AddEmployee(new Employee { Name = "Владислав", Role = "Тестувальник" });
        itDepartment.AddEmployee(new Employee { Name = "Михайло", Role = "Менеджер" });

        IEmployeeIterator iterator = itDepartment.CreateIterator();

        Console.WriteLine("Список працівників відділу:");
        while (iterator.HasNext())
        {
            Employee emp = iterator.GetNext();
            Console.WriteLine($"- {emp.Name} ({emp.Role})");
        }
    }
}

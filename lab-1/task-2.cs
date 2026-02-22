/*
    Порушено принцип Dependency Inversion.
    ClientService залежить від конкретних реалізацій ClientRepository та EmailHelper.
    Рішення:
    1. Введено інтерфейси IClientRepository та IEmailService.
    2. ClientService отримує залежності через конструктор (Dependency Injection).
*/

using System;

public class Client
{
    // Model structure
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(this.Name)) return false;
        if (!EmailHelper.IsValid(this.Email)) return false;
        if (DateOfBirth > DateTime.Now) return false;
        
        return true;
    }
}

public interface IClientRepository
{
    void Add(Client client);
}

public class ClientRepository : IClientRepository
{
    public void Add(Client client)
    {
        // Persist Data
        using var cn = new SqlConnection("cnString");
        var cmd = new SqlCommand("INSERT INTO clients(Name, Email, DateOfBirth) VALUES(@Name, @Email, @DateOfBirth)", cn);
        cmd.Parameters.AddWithValue("Name", client.Name);
        cmd.Parameters.AddWithValue("Email", client.Email);
        cmd.Parameters.AddWithValue("DateOfBirth", client.DateOfBirth);
        
        cmd.ExecuteNonQuery();
  }
}

public interface IEmailService
{
    void Send(string to, string subject, string body, string from = "no-reply@system.net");
}

public class EmailHelper : IEmailService
{
    public static bool IsValid(string email)
    {
        return email.Contains("@");
    }

    public void Send(string to, string subject, string body, string from = "no-reply@system.net")
    {
        // Send e-mail
        var mail = new MailMessage(from, to);
        var smtpClient = new SmtpClient
        {
            Port = 25,
            Host = "smtp.system.net"
        };

        mail.Subject = subject;
        mail.Body = body;

        smtpClient.Send(mail);
        
    }
}

public class ClientService
{
    private readonly IClientRepository _repository;
    private readonly IEmailService _emailService;

    public ClientService(IClientRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public (bool, string) AddClient(Client client)
    {
        if (!client.IsValid()) return (false, "Client data is not valid");

        _repository.Add(client);
        _emailService.Send(client.Email, "Welcome", "Congrats!");

        return (true, string.Empty);
    }
}

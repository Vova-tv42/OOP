/*
    Порушено принцип Single Responsibility.
    Клас Client відповідав за модель даних, валідацію, збереження в БД та відправку пошти.
    Рішення:
    1. Client - залишено тільки як модель даних.
    2. ClientRepository - винесено логіку роботи з БД.
    3. EmailService - винесено логіку відправки листів.
    4. ClientValidator - винесено логіку валідації.
    5. ClientService - об'єднує всі дії.
*/
 
public class Client
{
    // Model structure
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class ClientValidator
{
    public (bool status, string errorMessage) Validate(Client client)
    {
        if (string.IsNullOrEmpty(client.Name)) return (false, "Name is invalid");
        if (!client.Email.Contains("@")) return (false, "Email is invalid");
        if (client.DateOfBirth > DateTime.Now) return (false, "Date of Birth is invalid");
        return (true, string.Empty);
    }
}

public class ClientRepository
{
    public void Save(Client client)
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

public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
        // Send e-mail
        var mail = new MailMessage("no-reply@system.net", email);
        var smtpClient = new SmtpClient
        {
            Port = 25,
            Host = "smtp.system.net"
        };

        mail.Subject = "[System.NET] Welcome";
        mail.Body = "Congrats!";
        smtpClient.Send(mail);
    }
}

public class ClientService
{
    private readonly ClientValidator _validator;
    private readonly ClientRepository _repository;
    private readonly EmailService _emailService;

    public ClientService()
    {
        _validator = new ClientValidator();
        _repository = new ClientRepository();
        _emailService = new EmailService();
    }

    public (bool status, string errorMessage) Register(Client client)
    {
        var validation = _validator.Validate(client);
        if (!validation.status) return validation;

        _repository.Save(client);
        _emailService.SendWelcomeEmail(client.Email);

        return (true, string.Empty);
    }
}

/*
    Порушено принцип Interface Segregation.
    ProductEnroll змушений реалізовувати непотрібні методи (SendEmail, SendSMS).
    Рішення: Інтерфейс IEnroll розділено на більш вузькі інтерфейси IOrder, IEmail, ISms.
*/

public interface IOrder
{
    void Validate();
    void Persist();
}

public interface IEmailSender
{
    void SendEmail();
}

public interface ISmsSender
{
    void SendSMS();
}

class ProductEnroll : IOrder
{
    public void Validate()
    {
        // Check data
    }

    public void Persist()
    {
        // Persist to database
    }
}

class ContactEnroll : IOrder, IEmailSender, ISmsSender
{
    public void Validate()
    {
        // Check data
    }

    public void Persist()
    {
        // Persist to database
    }

    public void SendEmail()
    {
        // Send email
    }

    public void SendSMS()
    {
        // Send SMS
    }
}

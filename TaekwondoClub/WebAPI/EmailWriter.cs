using DB.Entities;
using System.Reflection.Metadata.Ecma335;

namespace WebAPI;

public static class EmailWriter
{
    public static EmailDTO UnpaidPaymentNotificationEmail(Payment payment)
    {
        EmailDTO email = new()
        {
            Recipent = payment.Customer.Email,
            Subject = $"Payment due for month {payment.Type}",
            Body = $"Dear {payment.Customer.Name} {payment.Customer.Surname}," +
            $"\n\nWe are reaching to you regarding your due payment for membership in our Teakwondo club (Month: {payment.Type}), the outstanding amount is: {payment.Price} PLN." +
            $"\nWe kindly remind you that the deadline for your payment is {payment.DueDate}." +
            $"\n\nYours sincerely, \nTaekwondo Club Management"
        };
        return email;
    }

    public static EmailDTO EventParticipationNotificationEmail(Event e, Customer customer)
    {
        EmailDTO email = new()
        {
            Recipent = customer.Email,
            Subject = $"Event participation reminder",
            Body = $"Dear {customer.Name} {customer.Surname}," +
            $"\nWe kindly remind you that you are enrolled as a participant of event {e.Name}, that is taking place on {e.Date}" +
            $"\n\nYours sincerely, \nTaekwondo Club Management"
        };
        return email;
    }

}

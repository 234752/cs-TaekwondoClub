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
            Subject = $"Payment due for month {payment.MonthYear}",
            Body = $"Dear {payment.Customer.Name} {payment.Customer.Surname}," +
            $"\n\nWe are reaching to you regarding your due payment for membership in our Teakwondo club (Month: {payment.MonthYear})." +
            $"\nWe kindly remind you that the deadline for your payment is {payment.DueDate}." +
            $"\n\nYours sincerely, \nTaekwondo Club Management"
        };
        return email;
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Entities;

public class Payment
{
    public Payment() { }
    public Payment(Payment payment) 
    {
        Name = payment.Name;
        Amount = payment.Amount;
        Price = payment.Price;
        DueDate = payment.DueDate;
        Paid = payment.Paid;
        Type = payment.Type;
        CustomerId = payment.CustomerId;
    }
    public void ReplaceProperties(Payment payment)
    {
        Name = payment.Name;
        Amount = payment.Amount;
        Price = payment.Price;
        DueDate = payment.DueDate;
        Paid = payment.Paid;
        Type = payment.Type;
        CustomerId = payment.CustomerId;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public DateTime DueDate { get; set; }
    public string Paid { get; set; }
    public string Type {  get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}

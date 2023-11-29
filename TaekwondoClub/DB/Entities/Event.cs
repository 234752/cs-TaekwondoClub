using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace DB.Entities;

public class Event
{
    public Event() { }
    public Event(Event ev) 
    {
        Name = ev.Name;
        Date = ev.Date;
        Type = ev.Type;
    }
    public void ReplaceProperties(Event ev)
    {
        Name = ev.Name;
        Date = ev.Date;
        Type = ev.Type;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public List<Customer> Customers { get; set; }
    public override string ToString()
    {
        if (Customers is null) return "no participants";
        return Customers.Count.ToString();
    }
}

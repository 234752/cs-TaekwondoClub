using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Entities;

public class Customer
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email {  get; set; }
    public string Address { get; set; }
    public string AccountNumber { get; set; }

    [JsonIgnore]
    public List<Event> Events { get; set; }
}

using System;
using System.Collections.Generic;

namespace QuoteRegister.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}

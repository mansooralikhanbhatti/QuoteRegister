using System;
using System.Collections.Generic;

namespace QuoteRegister.Entities;

public partial class SalesStaff
{
    public int StaffId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}

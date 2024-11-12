using System;
using System.Collections.Generic;

namespace QuoteRegister.Entities;

public partial class Quote
{
    public int QuoteId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? RecievedByStaffId { get; set; }

    public int? RequestedByCustomerId { get; set; }

    public DateTime RecievedDate { get; set; }

    public virtual ICollection<QuoteItem> QuoteItems { get; set; } = new List<QuoteItem>();

    public virtual SalesStaff? RecievedByStaff { get; set; }

    public virtual Customer? RequestedByCustomer { get; set; }
}

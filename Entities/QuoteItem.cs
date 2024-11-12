using System;
using System.Collections.Generic;

namespace QuoteRegister.Entities;

public partial class QuoteItem
{
    public int QuoteItemId { get; set; }

    public int? QuoteId { get; set; }

    public string? ItemCode { get; set; }

    public string? ItemName { get; set; }

    public int? QuantityRequested { get; set; }

    public virtual Quote? Quote { get; set; }
}

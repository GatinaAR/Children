using System;
using System.Collections.Generic;

namespace MyDiplom.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int? SenderId { get; set; }

    public int? RecipientId { get; set; }

    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? Timestamp { get; set; }

    public virtual User? Recipient { get; set; }

    public virtual User? Sender { get; set; }
}

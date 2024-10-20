using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int ChatIdFk { get; set; }

    public string UserIdFk { get; set; } = null!;

    public int FileIdFk { get; set; }

    public byte[] SentAt { get; set; } = null!;

    public DateTime EditedAt { get; set; }

    public string Content { get; set; } = null!;

    public string MessageType { get; set; } = null!;

    public bool IsRead { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsForwarded { get; set; }

    public virtual Chat ChatIdFkNavigation { get; set; } = null!;

    public virtual ChatFile FileIdFkNavigation { get; set; } = null!;

    public virtual ICollection<ForwardMessage> ForwardMessages { get; set; } = new List<ForwardMessage>();

    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    public virtual AspNetUser UserIdFkNavigation { get; set; } = null!;
}

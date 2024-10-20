using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public string? Name { get; set; }

    public byte[]? Icon { get; set; }

    public string? Description { get; set; }

    public bool IsGroup { get; set; }

    public bool IsChannel { get; set; }

    public virtual ICollection<ChatFile> ChatFiles { get; set; } = new List<ChatFile>();

    public virtual ICollection<ForwardMessage> ForwardMessages { get; set; } = new List<ForwardMessage>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}

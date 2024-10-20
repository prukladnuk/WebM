using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ChatFile
{
    public int FileId { get; set; }

    public int ChatIdFk { get; set; }

    public string UserIdFk { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public byte[] UploatedAt { get; set; } = null!;

    public virtual Chat ChatIdFkNavigation { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual AspNetUser UserIdFkNavigation { get; set; } = null!;
}

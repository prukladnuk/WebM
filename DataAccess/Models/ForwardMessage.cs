using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ForwardMessage
{
    public int ForwardId { get; set; }

    public int OriginalMessageIdFk { get; set; }

    public int NewChatIdFk { get; set; }

    public byte[] ForwardAt { get; set; } = null!;

    public bool OriginalIsDeleted { get; set; }

    public virtual Chat NewChatIdFkNavigation { get; set; } = null!;

    public virtual Message OriginalMessageIdFkNavigation { get; set; } = null!;
}

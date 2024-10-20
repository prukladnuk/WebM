using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Reaction
{
    public int ReactionId { get; set; }

    public int MessageIdFk { get; set; }

    public string UserIdFk { get; set; } = null!;

    public string ReactionType { get; set; } = null!;

    public byte[] ReactedAt { get; set; } = null!;

    public virtual Message MessageIdFkNavigation { get; set; } = null!;

    public virtual AspNetUser UserIdFkNavigation { get; set; } = null!;
}

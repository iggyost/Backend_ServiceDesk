using System;
using System.Collections.Generic;

namespace Backend_ServiceDesk.ApplicationData;

public partial class Progress
{
    public int ProgressId { get; set; }

    public int AdminId { get; set; }

    public string Title { get; set; } = null!;

    public int AdminRequestId { get; set; }

    public virtual AdminsRequest AdminRequest { get; set; } = null!;
}

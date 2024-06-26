﻿using System;
using System.Collections.Generic;

namespace Backend_ServiceDesk.ApplicationData;

public partial class AdminsRequest
{
    public int AdminRequestId { get; set; }

    public int AdminId { get; set; }

    public int RequestId { get; set; }

    public DateTime AcceptedDate { get; set; }

    public TimeSpan AcceptedTime { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public TimeSpan? LastChangeTime { get; set; }

    public bool? IsReady { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

    public virtual Request Request { get; set; } = null!;
}

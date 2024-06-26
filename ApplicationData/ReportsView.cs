﻿using System;
using System.Collections.Generic;

namespace Backend_ServiceDesk.ApplicationData;

public partial class ReportsView
{
    public int RequestId { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int StatusId { get; set; }

    public string Category { get; set; } = null!;

    public int CategoryId { get; set; }

    public string UserEmail { get; set; } = null!;

    public int UserId { get; set; }

    public string AdminEmail { get; set; } = null!;

    public int AdminId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime AcceptedDate { get; set; }

    public TimeSpan AcceptedTime { get; set; }

    public DateTime? LastChangeDate { get; set; }

    public TimeSpan? LastChangeTime { get; set; }

    public bool? IsReady { get; set; }
}

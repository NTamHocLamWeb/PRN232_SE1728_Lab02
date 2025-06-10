﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class SystemAccount
{
    public int AccountId { get; set; }

    public string AccountPassword { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string AccountNote { get; set; } = null!;

    public int? Role { get; set; }
}

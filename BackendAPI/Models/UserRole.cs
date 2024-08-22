﻿using System;
using System.Collections.Generic;

namespace BackendAPI.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Role { get; set; }

    public virtual User? User { get; set; }
}

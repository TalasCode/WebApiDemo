using System;
using System.Collections.Generic;

namespace BackendAPI.Models;

public partial class Lookup
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}

using System;
using System.Collections.Generic;

namespace Project_for_DB;

public partial class Adress
{
    public int Id { get; set; }

    public string Street { get; set; } = null!;

    public int Number { get; set; }

    public int? Building { get; set; }

    public virtual ICollection<Lock> Locks { get; set; } = new List<Lock>();
}

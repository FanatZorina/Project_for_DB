using System;
using System.Collections.Generic;

namespace Project_for_DB;

public partial class Lock
{
    public int Id { get; set; }

    public int IdStreet { get; set; }

    public int Level { get; set; }

    public bool Closed { get; set; }

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();

    public virtual Adress IdStreetNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Project_for_DB;

public partial class Audit
{
    public int Number { get; set; }

    public DateTime Date { get; set; }

    public int IdDoor { get; set; }

    public int IdStreet { get; set; }

    public string Login { get; set; } = null!;

    public bool Closed { get; set; }

    public virtual Lock Id { get; set; } = null!;

    public virtual User LoginNavigation { get; set; } = null!;
}

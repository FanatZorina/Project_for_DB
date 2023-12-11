using System;
using System.Collections.Generic;

namespace Project_for_DB;

public partial class User
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public int Level { get; set; }

    public int DepartamentId { get; set; }

    public virtual ICollection<Audit> Audits { get; set; } = new List<Audit>();

    public virtual Departament? Departament { get; set; }
}

using System;
using System.Collections.Generic;

namespace Project_for_DB;

public partial class Departament
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Number { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

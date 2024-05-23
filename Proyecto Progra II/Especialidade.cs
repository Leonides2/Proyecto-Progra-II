using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II;

public partial class Especialidade
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}

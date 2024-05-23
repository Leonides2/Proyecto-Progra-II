using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II;

public partial class EstadosCita
{
    public int Id { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}

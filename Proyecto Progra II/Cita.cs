using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II;

public partial class Cita
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string Lugar { get; set; } = null!;

    public int IdEstado { get; set; }

    public int IdPaciente { get; set; }

    public int IdEspecialidad { get; set; }

    public virtual Especialidade IdEspecialidadNavigation { get; set; } = null!;

    public virtual EstadosCita IdEstadoNavigation { get; set; } = null!;

    public virtual Usuario IdPacienteNavigation { get; set; } = null!;
}

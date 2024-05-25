using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II.Models;

public partial class Cita
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public string Lugar { get; set; }

    public int IdEstado { get; set; }

    public int IdPaciente { get; set; }

    public int IdEspecialidad { get; set; }

    public int IdSucursal { get; set; }

}

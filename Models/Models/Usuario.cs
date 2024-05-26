using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public int IdRol { get; set; }

}

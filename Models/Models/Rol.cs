using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Proyecto_Progra_II.Models;

public partial class Rol 
{
    public int Id { get; set; }

    public string NombreRol { get; set; } = string.Empty;

}



namespace Proyecto_Progra_II.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Name { get; set; } 

    public string? Password { get; set; } 

    public string? Email { get; set; } 

    public string? Telefono { get; set; } 

    public int IdRol { get; set; }

}

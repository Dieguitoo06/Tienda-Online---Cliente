namespace Api.Funcionalidaades.Clientes;

public class UsuariosQueryDto
{
    public int Dni { get; set; }
    public required string Usuario { get; set; }

}

public class UsuariosCommandDto
{
    public required int Dni { get; set; }

    public required string Nombre { get; set;}

    public required string Apellido { get; set; }    

    public required string Usuario { get; set; } 

    public required string Contraseña { get; set; }
}
